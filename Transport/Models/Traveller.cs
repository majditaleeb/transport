//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Transport.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Traveller
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Traveller()
        {
            this.Informaions = new HashSet<Informaion>();
        }
    
        public int travellerId { get; set; }
        public string travellerIdentifiy { get; set; }
        public string travellerName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Informaion> Informaions { get; set; }
    }
}