using InventoryWeb.Core.Models.Domains;
using InventoryWeb.Core.Repositories;
using InventoryWeb.Core.Service;
using InventoryWeb.Core.ViewModels;
using InventoryWeb.Persistence;
using InventoryWeb.Persistence.Extensions;
using InventoryWeb.Persistence.Repositories;
using InventoryWeb.Persistence.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InventoryWeb.Controllers
{
    public class ProductsBaseController : Controller
    {
        private IProductBaseService _productBaseRepository;
        public ProductsBaseController(IProductBaseService productBaseService)
        {
            _productBaseRepository = productBaseService;
        }
        public IActionResult ProductsDb()
        {
            var vm = new ProductsDbViewModel
            {
                ProductsBase = _productBaseRepository.GetProductsInBase(),
                ProductBase = new ProductsBase()
            };

            return View("ProductsBase", vm);
        }
        [HttpPost]
        public IActionResult AddProductDbs(InventoryViewModel model)
        {
            var productDb = new ProductsBase
            {
                NameProductDb = model.Product.ProductName,
                Code = model.Product.Code,
                EachPrice = model.Product.Price,
            };
            ModelState.Remove("Product.UnitId");
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, productDb });
            }
            _productBaseRepository.AddProductBase(productDb);
            return Json(new { success = true, productDb });
        }
        [HttpPost]
        public IActionResult AddProductDb(ProductsBase model)
        {
            var productDb = new ProductsBase
            {
                NameProductDb = model.NameProductDb,
                Code = model.Code,
                EachPrice = model.EachPrice,
            };

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, productDb });
            }
            _productBaseRepository.AddProductBase(productDb);
            return Json(new {success = true, productDb});
        }

        [HttpPost]
        public IActionResult DeleteProductDb(int id)
        {
            try
            {
                _productBaseRepository.DeleteProductBase(id);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult EditProductDb(ProductsBase prodToUpdate)
        {
            var updatedProduct = _productBaseRepository.GetProductBaseById(prodToUpdate.Id);

            updatedProduct.Code = prodToUpdate.Code;
            updatedProduct.NameProductDb = prodToUpdate.NameProductDb;
            updatedProduct.EachPrice = prodToUpdate.EachPrice;
            if (ModelState.IsValid)
            {
                _productBaseRepository.UpdateProductBase(updatedProduct);

                return Json(new { success = true});
            }
            return Json(new { success = false, updatedProduct });

        }
        public IActionResult GetProductForEdit(int id)
        {
            var product = _productBaseRepository.GetProductBaseById(id);
            return Json(new { success = true, product });
        }
    }
}
