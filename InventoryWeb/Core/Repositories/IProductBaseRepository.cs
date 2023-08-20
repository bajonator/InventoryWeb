using InventoryWeb.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace InventoryWeb.Core.Repositories
{
    public interface IProductBaseRepository
    {
        ProductsBase GetProductBaseById(int id);
        void AddProductBase(ProductsBase product);
        ProductsBase GetProductBase(string kodinput);
        IEnumerable<ProductsBase> GetProductsInBase();
        void DeleteProductBase(int id);
        void UpdateProductBase(ProductsBase updatedProduct);       
    }
}
