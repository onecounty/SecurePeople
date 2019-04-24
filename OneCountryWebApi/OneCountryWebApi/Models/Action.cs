using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OneCountryWebApi.Models
{
    [Table("OneAction")]
    public class Action
    {
        public int ActionId { get; set; }
        [MaxLength(50)]
        public string ActionName { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        
    }
}