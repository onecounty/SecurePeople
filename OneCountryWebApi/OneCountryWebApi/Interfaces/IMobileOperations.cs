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
    }
}
