using InventoryWeb.Core.Models.Domains;
using System.Collections;
using System.Collections.Generic;

namespace InventoryWeb.Core.ViewModels
{
    public class InventoriesViewModel
    {
        public Inventory Inventory { get; set; }
        public Product Product { get; set; }
        public IEnumerable<Inventory> Inventories { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
