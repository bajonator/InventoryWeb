using InventoryWeb.Core;
using InventoryWeb.Core.Models.Domains;
using InventoryWeb.Core.Service;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace InventoryWeb.Persistence.Services
{
    public class ProductBaseService : IProductBaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductBaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ProductsBase GetProductBaseById(int id)
        {
            return _unitOfWork.ProductBase.GetProductBaseById(id);
        }
        public void AddProductBase(ProductsBase product)
        {
            _unitOfWork.ProductBase.AddProductBase(product);
            _unitOfWork.Complete();
        }

        public ProductsBase GetProductBase(string kodinput)
        {
            return _unitOfWork.ProductBase.GetProductBase(kodinput);
        }

        public IEnumerable<ProductsBase> GetProductsInBase()
        {
            return _unitOfWork.ProductBase.GetProductsInBase();
        }

        public void DeleteProductBase(int id)
        {
            _unitOfWork.ProductBase.DeleteProductBase(id);
            _unitOfWork.Complete();
        }

        public void UpdateProductBase(ProductsBase updatedProduct)
        {
            _unitOfWork.ProductBase.UpdateProductBase(updatedProduct);
            _unitOfWork.Complete();
        }
    }
}
