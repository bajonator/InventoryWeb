using InventoryWeb.Core.Models.Domains;

namespace InventoryWeb.Core.ViewModels
{
    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public ProductsBase Product { get; set; }

    }
}
