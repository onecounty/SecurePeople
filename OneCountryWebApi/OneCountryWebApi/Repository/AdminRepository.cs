using log4net;
using OneCountryWebApi.Interfaces;
using OneCountryWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneCountryWebApi.Repository
{
    public class AdminRepository:IAdmin
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        private ILog _logger = LogManager.GetLogger(typeof(AdminRepository));

        public List<Report> GetReports(string location,int action,string description,int page,int itemPerPage)
        {
            if (string.IsNullOrEmpty(location)&&action==0&& string.IsNullOrEmpty(description))
            {
                return _db.Reports.Skip(itemPerPage * page).Take(itemPerPage).ToList();
            }
            else
            {
                return _db.Reports.Where(x => x.ApproximateArea == location || x.ActionId == action || x.Description.Contains(description)).Skip(itemPerPage * page).Take(itemPerPage).ToList();
            }
           
        }
    }
}