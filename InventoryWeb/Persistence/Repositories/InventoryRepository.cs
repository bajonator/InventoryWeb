using InventoryWeb.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InventoryWeb.Persistence.Repositories
{
    public class InventoryRepository
    {
        private ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
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
            _context.SaveChanges();
        }

        public Inventory GetPreview(int id)
        {
            return _context.Inventories.Find(id);
        }

        public void Delete(int id, string userId)
        {
            var inventory = _context.Inventories.Single(x => x.Id == id && x.UserId == userId);
            _context.Inventories.Remove(inventory);
            _context.SaveChanges();
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Include(x => x.Units).FirstOrDefault(x => x.Id == id);
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Single(x => x.Id == id);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public void Update(Product model)
        {
            _context.Products.Update(model);
            _context.SaveChanges();
        }

        public IEnumerable<Unit> GetUnit()
        {
            return _context.Units.ToList();
        }

        public void AddProductDb(ProductsBase product)
        {
            var productFind = _context.ProductsBases.FirstOrDefault(x => x.Code == product.Code);
            if (productFind == null) 
            {
                _context.ProductsBases.Add(product);
                _context.SaveChanges();
            }
        }

        public ProductsBase GetProductInBase(string kodinput)
        {
            return _context.ProductsBases.FirstOrDefault(x => x.Code == kodinput);
        }

        public IEnumerable<ProductsBase> GetProductsInBase()
        {
            return _context.ProductsBases.ToList();
        }
    }
}
