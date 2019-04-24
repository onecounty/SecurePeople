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
