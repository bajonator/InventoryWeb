using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryWeb.Core.Models.Domains
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Pole jest wymagane")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Jednostka")]
        public int UnitId { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ProductValue { get; set; }
        public int Quantity { get; set; }
        public int InventoryId { get; set; }
        public Unit Units { get; set; }
        public Inventory Inventory { get; set;}
        public ApplicationUser User { get; set; }
    }
}
