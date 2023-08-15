using InventoryWeb.Core.Models.Domains;
using System.Collections.Generic;

namespace InventoryWeb.Core.ViewModels
{
    public class InventoryViewModel
    {
        public Inventory Inventory { get; set; }
        public Product Product { get; set; }
        public Unit Unit { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Unit> Units { get; set; }
    }
}
