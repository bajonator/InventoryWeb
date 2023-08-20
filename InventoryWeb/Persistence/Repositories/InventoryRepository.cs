using InventoryWeb.Core;
using InventoryWeb.Core.Models.Domains;
using InventoryWeb.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InventoryWeb.Persistence.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private IApplicationDbContext _context;

        public InventoryRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Inventory> Get(string userId)
        {
            return _context.Inventories.Include(x => x.Products).ToList();
        }

        public List<Product> GetAllProducts(int id)
        {
            return _context.Products.Include(x => x.Units).Where(x => x.InventoryId == id).ToList();
        }
        public IEnumerable<Product> GetProduct(int id)
        {
            return _context.Products.Where(x => x.InventoryId == id).ToList();
        }

        public Inventory GetInventory(string userId, int id = 0)
        {
            return _context.Inventories.Include(x => x.Products).FirstOrDefault(x => x.UserId == userId && x.Id == id);
        }

        public void AddInventory(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
        }

        public Inventory GetPreview(int id)
        {
            return _context.Inventories.Find(id);
        }

        public void Delete(int id, string userId)
        {
            var inventory = _context.Inventories.Single(x => x.Id == id && x.UserId == userId);
            _context.Inventories.Remove(inventory);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Include(x => x.Units).FirstOrDefault(x => x.Id == id);
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Single(x => x.Id == id);
            _context.Products.Remove(product);
        }

        public void Update(Product model)
        {
            _context.Products.Update(model);
        }
    }
}
