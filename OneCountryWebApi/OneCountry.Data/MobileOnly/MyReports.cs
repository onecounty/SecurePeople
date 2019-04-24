using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCountry.Data.MobileOnly
{
    public class MyReports
    {
        public string CaseId { get; set; }
        public string Description { get; set; }
        public double UploadedLat { get; set; }
        public double UploadedLong { get; set; }
        public double LocationLat { get; set; }
        public double LocationLong { get; set; }       
        public string MobileNumber { get; set; }
    }

    public class MyReportListItem
    {
        public string CaseId { get; set; }
        public string Description { get; set; }
        public double LocationLat { get; set; }
        public double LocationLong { get; set; }
        public string Action { get; set; }

    }
}
