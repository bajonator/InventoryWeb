using InventoryWeb.Core;
using InventoryWeb.Core.Models.Domains;
using InventoryWeb.Core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace InventoryWeb.Persistence.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public InventoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Inventory> Get(string userId)
        {
            return _unitOfWork.Inventory.Get(userId);
        }

        public List<Product> GetAllProducts(int id)
        {
            return _unitOfWork.Inventory.GetAllProducts(id);
        }
        public IEnumerable<Product> GetProduct(int id)
        {
            return _unitOfWork.Inventory.GetProduct(id);
        }

        public Inventory GetInventory(string userId, int id = 0)
        {
            return _unitOfWork.Inventory.GetInventory(userId, id);
        }

        public void AddInventory(Inventory inventory)
        {
            _unitOfWork.Inventory.AddInventory(inventory);
            _unitOfWork.Complete();
        }

        public Inventory GetPreview(int id)
        {
            return _unitOfWork.Inventory.GetPreview(id);
        }

        public void Delete(int id, string userId)
        {
            _unitOfWork.Inventory.Delete(id, userId);
            _unitOfWork.Complete();
        }

        public void AddProduct(Product product)
        {
            _unitOfWork.Inventory.AddProduct(product);
            _unitOfWork.Complete();
        }

        public Product GetProductById(int id)
        {
            return _unitOfWork.Inventory.GetProductById(id);
        }

        public void DeleteProduct(int id)
        {
            _unitOfWork.Inventory.DeleteProduct(id);
            _unitOfWork.Complete();
        }

        public void Update(Product model)
        {
            _unitOfWork.Inventory.Update(model);
            _unitOfWork.Complete();
        }

        public decimal GetValueAll(int id)
        {
            decimal val = 0;
            var products = _unitOfWork.Inventory.GetAllProducts(id);
            foreach (var item in products)
            {
                val += item.ProductValue;
            };
            return val;
        }
    }
}
