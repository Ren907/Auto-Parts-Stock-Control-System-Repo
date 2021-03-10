using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AutoPartsStockControlSystem.Models
{
    public class UserClassModel
    {
        
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



    }
}