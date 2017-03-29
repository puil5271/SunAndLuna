using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SunAndLuna.Models
{
    public class Bill
    {
        [Key]
        public int BillID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Bill Number is required.")]
        [Display(Name = "Bill Number")]
        public int BillNumber { get; set; }

        [Required(ErrorMessage = "Pruduct Name is required.")]
        [Display(Name = "Pruduct Name")]
        public string PruductName { get; set; }

        [Required(ErrorMessage = "Bill Amount is required.")]
        [Display(Name = "Bill Amount")]
        public double BillAmount { get; set; }

        [Required(ErrorMessage = "Manufacturer is required.")]
        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Company Name is required.")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Product Type is required.")]
        [Display(Name = "Product Type")]
        public string ProductType { get; set; }

        [Required(ErrorMessage = "Return Expiry Date is required.")]
        [Display(Name = "Return Expiry Date")]
        [DataType(DataType.DateTime)]
        public DateTime ExpiryDateReturn { get; set; }

        [Required(ErrorMessage = "Warranty Expiry Date is required.")]
        [Display(Name = "Warranty Expiry Date")]
        [DataType(DataType.DateTime)]
        public DateTime ExpiryDateWarranty { get; set; }

        [Required(ErrorMessage = "Purchase Date is required.")]
        [Display(Name = "Purchase Date")]
        [DataType(DataType.DateTime)]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "Bill Comment is required.")]
        [Display(Name = "Bill Comment")]
        public string BillComment { get; set; }

        public bool IsDelete { get; set; }
    }

    public class BillDBContext : DbContext
    {
        public DbSet<Bill> Billinfo { get; set; }
    }
}