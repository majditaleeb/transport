using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Transport.Models
{
    public class HomeIndexMV
    {
        public int id { get; set; }
        [Required(ErrorMessage ="خانة إجبارية")]
        [DisplayName("كاش")]
        public Nullable<bool> CashOrReserve { get; set; }
        [Required(ErrorMessage = "خانة إجبارية")]
        [DisplayName("قيمة الكاش")]

        //[RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "أدخل رقم")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "أدخل رقم")]
        //[Range(0, 9999999999999999.99, ErrorMessage = "القيمة القصوى  9999999999999999.99")]
        //[DisplayFormat(DataFormatString = "{0:#.##}")]
        public Nullable<decimal> CashValue { get; set; }
        public Nullable<int> fromId { get; set; }
        public Nullable<int> toId { get; set; }
        public Nullable<int> travellerId { get; set; }
        public Nullable<int> carId { get; set; }
        public Nullable<int> userid { get; set; }

        public virtual Car Car { get; set; }
        public virtual From From { get; set; }
        public virtual To To { get; set; }
        public virtual Traveller Traveller { get; set; }
        public virtual User User { get; set; }

        
        [DisplayName("رقم المركبة")]
        public string carName { get; set; }
        [DisplayName("أضف مركبة")]
        [RegularExpression("([0-9]+)", ErrorMessage = "أدخل رقم")]
        public string anotherCarName { get; set; }


        [DisplayName("بداية الطلب")]
        public string fromName { get; set; }
        [DisplayName("أضف اسم المنطقة")]
        public string anotherFromName { get; set; }

       
        [DisplayName("نهاية الطلب")]
        public string toName { get; set; }
        [DisplayName("أضف اسم المنطقة")]
        public string anotherToName { get; set; }

      
        [DisplayName("اسم السائق")]
        public string travellerName { get; set; }
        [DisplayName(" أضف سائق جديد")]
        public string anotherTravellerName { get; set; }


     
        [DisplayName("رقم هوية السائق")]
        public string travellerIdentifiy { get; set; }
        [DisplayName("  اضف رقم هوية للسائق الجديد")]
        [RegularExpression("([0-9]+)", ErrorMessage = "أدخل رقم")]
        public string anotherTravellerIdentifiy { get; set; }

        public string userFname { get; set; }
        public string userLname { get; set; }
        [DisplayName("التاريخ")]
        public Nullable<System.DateTime> dateTravel { get; set; }
    }
}