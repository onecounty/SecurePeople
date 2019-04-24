﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCountry.Data.MobileOnly
{
    public class MyReports: MyReportListItem
    {
        public string ImageUrl { get; set; }
        public string MobileNumber { get; set; }

        public MyReports(int id, string caseId, string description, double lat, double longt, string action, string status,string image, string mobile)
        {
            Id = id;
            CaseId = caseId;
            description = Description;
            LocationLat = lat;
            LocationLong = longt;
            Action = action;
            Status = status;
            ImageUrl = image;
            MobileNumber = mobile;
        }
    }

    public class MyReportListItem
    {
        public int Id { get; set; }
        public string CaseId { get; set; }
        public string Description { get; set; }
        public double LocationLat { get; set; }
        public double LocationLong { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }

        public MyReportListItem()
        {
            //default
        }
        public MyReportListItem(int id, string caseId,string description,double lat,double longt,string action,string status)
        {
            Id = id;
            CaseId = caseId;
            description = Description;
            LocationLat = lat;
            LocationLong = longt;
            Action = action;
            Status = status;
        }

    }

    public class MyReportUpload: MyReportListItem
    {
        public byte[] Image { get; set; }
        public string MobileNumber { get; set; }    }
}
