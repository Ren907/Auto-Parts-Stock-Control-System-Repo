//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoPartsStockControlSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            this.Sales = new HashSet<Sale>();
            this.Vehicles = new HashSet<Vehicle>();
        }
    
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string ItemCategory { get; set; }
        public string ItemCompatability { get; set; }
        public Nullable<decimal> ItemPrice { get; set; }
        public Nullable<int> ItemQuantity { get; set; }
        public int ItemSupplierFK { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
