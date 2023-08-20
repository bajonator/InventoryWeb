using InventoryWeb.Core;
using InventoryWeb.Core.Models.Domains;
using InventoryWeb.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace InventoryWeb.Persistence.Repositories
{
    public class ProductBaseRepository : IProductBaseRepository
    {
        private IApplicationDbContext _context;

        public ProductBaseRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public ProductsBase GetProductBaseById(int id)
        {
            return _context.ProductsBases.FirstOrDefault(x => x.Id == id);
        }
        public void AddProductBase(ProductsBase product)
        {
            var productFind = _context.ProductsBases.FirstOrDefault(x => x.Code == product.Code);
            if (productFind == null)
            {
                _context.ProductsBases.Add(product);
            }
        }

        public ProductsBase GetProductBase(string kodinput)
        {
            return _context.ProductsBases.FirstOrDefault(x => x.Code == kodinput);
        }

        public IEnumerable<ProductsBase> GetProductsInBase()
        {
            return _context.ProductsBases.ToList();
        }

        public void DeleteProductBase(int id)
        {
            var product = _context.ProductsBases.Single(x => x.Id == id);
            _context.ProductsBases.Remove(product);
        }

        public void UpdateProductBase(ProductsBase updatedProduct)
        {
            _context.ProductsBases.Update(updatedProduct);
        }
    }
}
