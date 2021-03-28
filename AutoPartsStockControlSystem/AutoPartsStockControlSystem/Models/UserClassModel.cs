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
        [Required]
        public int ItemID { get; set; }
        [Required]
        public string ItemPart { get; set; }
        [RegularExpression("^[-_, A-Za-z]+$", ErrorMessage = "Please enter only Alphabetical Characters!")]
        public string ItemDescription { get; set; }
        public string ItemCategory { get; set; }
        [Required]
        public Nullable<int> ItemQuantity { get; set; }
        public Nullable<decimal> ItemPrice { get; set; }
        [RegularExpression("^[- A-Za-z]+$", ErrorMessage = "Please enter only Alphabetical Characters!")]
        public string VehicleMake { get; set; }
        [RegularExpression("^[- A-Za-z]+$", ErrorMessage = "Please enter only Alphabetical Characters!")]
        public string VehicleModel { get; set; }
        [RegularExpression("^[-_,0-9]*$", ErrorMessage = "Please enter only year Range! example:(2020-2021)")]
        public string VehicleModelYearRange { get; set; }
        public string PartCompatability { get; set; }



        //Stock out

        public int StockOutID { get; set; }
        [RegularExpression("^[-, A-Za-z]+$", ErrorMessage = "Please enter only Alphabetical Characters!")]
        public string ClientName { get; set; }
        [RegularExpression("^[-, A-Za-z]+$", ErrorMessage = "Please enter only Alphabetical Characters!")]
        public string ClientSurname { get; set; }
        public string ClientIDCard { get; set; }
        public string ClientContact { get; set; }
        public string StockOutPart { get; set; }
        public string StockOutDescription { get; set; }
        public Nullable<int> StockOutQuantity { get; set; }

        public string StockOutDate { get; set; }


    }

}