using OneCountry.Data.MobileOnly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCountry.Data
{
    public class OneReportDetail: MyReports
    {
        public OneReportDetail(int id, string caseId, string description, double lat, double longt, string action, string status, string image, string mobile) 
            : base(id, caseId, description, lat, longt, action, status, image, mobile)
        {
        }

        public double UploadedLat { get; set; }
        public double UploadedLong { get; set; }
        public string LastActionDescription { get; set; }
        public string LastActionTakenBy { get; set; }
        public string ApproximateArea { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastActionTakenDate { get; set; }
        public UserDetails UserDetails { get; set; }
    }
}
