using OneCountryWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCountryWebApi.Interfaces
{
    public interface IAdmin
    {
        List<Report> GetReports(string location, int action, string description, int page, int itemPerPage);
    }
}
