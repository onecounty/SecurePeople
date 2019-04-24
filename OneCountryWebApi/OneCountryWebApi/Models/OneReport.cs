using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OneCountryWebApi.Models
{
    [Table("OneReport")]
    public class Report
    {
        [Column(name:"OneReportId")]
        public int ReportId { get; set; }
        [MaxLength(150)]
        public string CaseId { get; set; }
        [MaxLength(500)]
        [Required]
        public string Description { get; set; }
        public double UploadedLat { get; set; }
        public double UploadedLong { get; set; }
        [Required]
        public double LocationLat { get; set; }
        [Required]
        public double LocationLong { get; set; }
        [Required]
        [StringLength(10)]
        [RegularExpression(@"^\(?([0-9]{10})$")]
        public string MobileNumber { get; set; }
        [ForeignKey("LastAction")]
        public int ActionId { get; set; }
        [MaxLength(225)]
        public string PhotoUrl { get; set; }
        [MaxLength(5000)]
        public string LastActionDescription { get; set; }
        public string LastActionTakenBy { get; set; }
        [MaxLength(500)]
        public string ApproximateArea { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastActionTakenDate { get; set; }
        public bool IsDelete { get; set; }

        [ForeignKey("CreatedUser")]
        public string CreatedUserId { get; set; }

        public virtual Action LastAction { get; set; }
        public virtual ApplicationUser CreatedUser { get; set; }

    }
}