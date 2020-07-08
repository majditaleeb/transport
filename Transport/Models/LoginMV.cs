using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Transport.Models
{
    public class LoginMV
    {
        [Required(ErrorMessage ="حقل إجباري")]
        [MaxLength(50,ErrorMessage ="الحد الأقصى 50 رمز فقط")]
        [DataType(DataType.Text)]
        public string userLogin { get; set; }

        [Required(ErrorMessage = "حقل إجباري")]
        [MaxLength(50, ErrorMessage = "الحد الأقصى 50 رمز فقط")]
        [DataType(DataType.Password)]
        public string passwordLogin { get; set; }
    }
    [MetadataType(typeof(LoginMV))]
    public partial class User
    {
    }
}