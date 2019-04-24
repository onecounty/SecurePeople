using log4net;
using OneCountry.Data.MobileOnly;
using OneCountryWebApi.Converters;
using OneCountryWebApi.Interfaces;
using OneCountryWebApi.Models;
using OneCountryWebApi.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}