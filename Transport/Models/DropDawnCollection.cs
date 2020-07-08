using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Transport.Models
{
    public class DropDawnCollection
    {
    
        public SelectList fromNameDDl { get; set; }
      
        public SelectList toNameDDl { get; set; }
        public SelectList travellerNameDDl { get; set; }
        public SelectList travellerIdentifyDDl { get; set; }
        public SelectList carNameDDl { get; set; }



    }
}