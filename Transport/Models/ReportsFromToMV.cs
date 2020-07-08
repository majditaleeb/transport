using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Transport.Models
{
    public class ReportsFromToMV
    {
        [Required(ErrorMessage ="خانة ضرورية")]
        [DataType(DataType.DateTime)]
        public DateTime From { get; set; }
        [Required(ErrorMessage = "خانة ضرورية")]
        [DataType(DataType.DateTime)]
        public DateTime To { get; set; }
    }
}