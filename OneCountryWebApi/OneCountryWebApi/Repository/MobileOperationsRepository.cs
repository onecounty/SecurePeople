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
using System.IO;
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


        public async Task<MyReportUpload> CreateReport(MyReportUpload report, string userId,string path)
        {
            Report tempItem = ReportMobileConverter.ConvertMyReportUploadsToReport(report);
            tempItem.CreatedUserId = userId;
            try
            {
                _db.Reports.Add(tempItem);
                await _db.SaveChangesAsync();

                string fileName= FilesSave(report.Image, path, report.fileExtention, tempItem.ReportId);

                tempItem.PhotoUrl = fileName;
                _db.Entry(report).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return ReportMobileConverter.ConvertReportToMyReportUploads(tempItem);

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return ReportMobileConverter.ConvertReportToMyReportUploads(tempItem);
            }
        }

        private async Task updateDetailReport(int Id, Report report)
        {
            report.IsDelete = true;
            _db.Entry(report).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        private string FilesSave(byte[] file,string path,string fileExtention,int Id)
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); 
            }

            string imageName = Id.ToString() + "."+ fileExtention;

            string imgPath = Path.Combine(path, imageName);

            File.WriteAllBytes(imgPath, file);

            return "Images/"+imageName;
        }
    }
}