using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryWeb.Core.Models.Domains
{
    public class Inventory
    {
        public Inventory()
        {
            Products = new Collection<Product>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        [Required]
        public string Company { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Value { get; set; }
        public ICollection<Product> Products { get; set; }
        public ApplicationUser User { get; set; }
    }
}