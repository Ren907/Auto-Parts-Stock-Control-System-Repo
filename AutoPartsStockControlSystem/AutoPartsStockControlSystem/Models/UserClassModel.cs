using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AutoPartsStockControlSystem.Models
{
    public class UserClassModel
    {
        // Suppliers
        public int SupplierID { get; set; }
      
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
       
        [Display(Name = "Products Type")]
        public string SupplierProductsType { get; set; }
   
        [Display(Name = "Contact")]
        public string SupplierContact { get; set; }
       
        [Display(Name = "Email")]
        public string SupplierEmail { get; set; }
       
        [Display(Name = "Supplier Experience")]
        public string SupplierExperience { get; set; }

        //Items
        //public IEnumerable<SelectListItem> PartsList { get; set; }
        public int ItemID { get; set; }
        [Required]
        public string ItemPart { get; set; }
        public string ItemDescription { get; set; }
        public string ItemCategory { get; set; }
        public string ItemCompatability { get; set; }
        public Nullable<decimal> ItemPrice { get; set; }
        [Required]
        public Nullable<int> ItemQuantity { get; set; }
        public Nullable<int> ItemSupplierFK { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
       

    }

}