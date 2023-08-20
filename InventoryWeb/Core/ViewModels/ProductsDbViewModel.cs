using InventoryWeb.Core.Models.Domains;
using System.Collections;
using System.Collections.Generic;

namespace InventoryWeb.Core.ViewModels
{
    public class ProductsDbViewModel
    {
        public ProductsBase ProductBase {get; set;}
        public IEnumerable<ProductsBase> ProductsBase { get; set;}
    }
}
