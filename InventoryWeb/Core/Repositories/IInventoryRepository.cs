using InventoryWeb.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace InventoryWeb.Core.Repositories
{
    public interface IInventoryRepository
    {
        IEnumerable<Inventory> Get(string userId);
        List<Product> GetAllProducts(int id);
        IEnumerable<Product> GetProduct(int id);
        Inventory GetInventory(string userId, int id = 0);
        void AddInventory(Inventory inventory);
        Inventory GetPreview(int id);
        void Delete(int id, string userId);
        void AddProduct(Product product);
        Product GetProductById(int id);
        void DeleteProduct(int id);
        void Update(Product model);
    }
}
