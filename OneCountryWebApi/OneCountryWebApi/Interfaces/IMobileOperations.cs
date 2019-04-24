using OneCountry.Data.MobileOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCountryWebApi.Interfaces
{
    public interface IMobileOperations
    {
        List<MyReportListItem> GetAllMyReports(string userId);
        MyReports GetMyReportDetail(int Id, string userId);
        Task<bool> DeleteReport(int Id, string userId);
        Task<MyReportUpload> CreateReport(MyReportUpload report, string userId, string path);
    }
}
