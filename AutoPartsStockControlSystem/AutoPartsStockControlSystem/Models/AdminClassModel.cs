using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AutoPartsStockControlSystem.Models
{
    public class AdminClassModel
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


        // Users


        public int UserID { get; set; }

        [Display(Name = "ID Card Number")]
        public string IDCard { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Contact")]
        public string Contact { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "User Type")]
        public string UserType { get; set; }


    }
}