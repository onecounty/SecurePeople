using log4net;
using OneCountry.Data;
using OneCountry.Data.MobileOnly;
using OneCountryWebApi.Converters;
using OneCountryWebApi.Interfaces;
using OneCountryWebApi.Models;
using OneCountryWebApi.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OneCountryWebApi.Repository
{
    public class MobileOperationsRepository: IMobileOperations
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        private ILog _logger = LogManager.GetLogger(typeof(MobileOperationsRepository));     
        public List<MyReportListItem> GetAllMyReports(string userId)
        {
            return              
                _db.Reports.Where(x => x.CreatedUserId == userId).Select(y=> ReportMobileConverter.ConvertReportsToMyReportListItem(y)).ToList();
        }

        public MyReports GetMyReportDetail(int Id, string userId)
        {
            return
                _db.Reports.Where(x => x.CreatedUserId == userId && x.ReportId==Id).Select(y => ReportMobileConverter.ConvertReportsToMyReports(y)).FirstOrDefault();
        }

        public async Task<bool> DeleteReport(int Id, string userId)
        {
            Report report = _db.Reports.Where(x => x.CreatedUserId == userId && x.ReportId == Id).FirstOrDefault();
          
            if (report == null)
            {
                return false;
            }

            if (report.ActionId != (int)ReportActionEnum.InProgress || report.ActionId != (int)ReportActionEnum.Closed)
            {
                await updateDetailReport(Id,report);
                return true;
            }

            _db.Reports.Remove(report);
            await _db.SaveChangesAsync();
            return true;
        }

        private async Task updateDetailReport(int Id, Report report)
        {
            report.IsDelete = true;
            _db.Entry(report).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}