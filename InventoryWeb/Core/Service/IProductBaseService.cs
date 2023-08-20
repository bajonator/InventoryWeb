using InventoryWeb.Core.Models.Domains;
using InventoryWeb.Persistence;
using System.Collections.Generic;

namespace InventoryWeb.Core.Service
{
    public interface IProductBaseService
    {
        ProductsBase GetProductBaseById(int id);
        void AddProductBase(ProductsBase product);
        ProductsBase GetProductBase(string kodinput);
        IEnumerable<ProductsBase> GetProductsInBase();
        void DeleteProductBase(int id);
        void UpdateProductBase(ProductsBase updatedProduct);
    }
}
