using log4net;
using OneCountry.Data.MobileOnly;
using OneCountryWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneCountryWebApi.Converters
{
    public class ReportMobileConverter
    {
        private static ILog _log = LogManager.GetLogger(typeof(ReportMobileConverter));

        internal static MyReportListItem ConvertReportsToMyReportListItem(Report item)
        {
            try
            {
                return new MyReportListItem(item.ReportId, item.CaseId, item.Description, item.LocationLat, item.LocationLong, item.LastAction.ActionName,item.LastAction.ActionName);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }

        internal static MyReports ConvertReportsToMyReports(Report item)
        {
            try
            {
                return new MyReports(item.ReportId, item.CaseId, item.Description, item.LocationLat, item.LocationLong, item.LastAction.ActionName, item.LastAction.ActionName,item.PhotoUrl,item.MobileNumber);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }
        }
    }
}