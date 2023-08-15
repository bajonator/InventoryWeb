using InventoryWeb.Core.Models.Domains;
using InventoryWeb.Core.ViewModels;
using InventoryWeb.Persistence;
using InventoryWeb.Persistence.Extensions;
using InventoryWeb.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryWeb.Controllers
{
    public class InventoryController : Controller
    {
        private InventoryRepository _inventoryRepository;
        public InventoryController(ApplicationDbContext context)
        {
            _inventoryRepository = new InventoryRepository(context);
        }
        public IActionResult Inventory()
        {
            var userId = User.GetUserId();

            var vm = new InventoriesViewModel
            {
                Inventory = _inventoryRepository.GetInventory(userId, 0),
                Inventories = _inventoryRepository.Get(userId),
                Product = new Product()
            };
            return View(vm);
        }
        public IActionResult InventoryPreview(int id)
        {
            var inv = _inventoryRepository.GetPreview(id);
            var products = _inventoryRepository.GetAllProducts(id);

            var vm = new InventoryViewModel
            {
                Inventory = inv,
                Products = _inventoryRepository.GetProduct(inv.Id),
                Units = _inventoryRepository.GetUnit(),
                Product = new Product()
            };
            vm.Inventory.Value = GetValueAll(id);

            return View("InventoryPreview", vm);
        }
        public IActionResult ProductsDb()
        {
            var products = _inventoryRepository.GetProductsInBase();

            return View(products);
        }
        public IActionResult GetInventoryProducts(int inventoryId)
        {
            var products = _inventoryRepository.GetProduct(inventoryId);
            var viewModel = new InventoryViewModel
            {
                Products = products
            };
            return RedirectToAction("InventoryPreview", viewModel);
        }
        public IActionResult FindProducts(string kodinput)
        {
            var product = _inventoryRepository.GetProductInBase(kodinput);
            if (product != null)
            {
                var productName = product.NameProductDb;

                return Json(new { success = true, productName });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult AddInventory(Inventory inventory)
        {
            ModelState.Remove("Inventory.Id");
            if (ModelState.IsValid)
            {
                inventory = new Inventory
                {
                    Name = inventory.Name,
                    Company = inventory.Company,
                    Date = DateTime.Now,
                    UserId = User.GetUserId(),
                };
                _inventoryRepository.AddInventory(inventory);
                return RedirectToAction("Inventory");
            }
            else
                return View(inventory);

        }
        [HttpPost]
        public IActionResult AddProduct(InventoryViewModel model)
        {
            var product = new Product
            {
                ProductName = model.Product.ProductName,
                Code = model.Product.Code,
                InventoryId = model.Inventory.Id,
                Price = model.Product.Price,
                Quantity = model.Product.Quantity,
                UnitId = model.Product.UnitId,
                ProductValue = model.Product.Price * model.Product.Quantity,
            };

            ModelState.Remove("Inventory.Name");
            ModelState.Remove("Inventory.Company");
            if (!ModelState.IsValid)
            {
                return PartialView("", product);
            }
            _inventoryRepository.AddProduct(product);

            return RedirectToAction("ProductTablePartial", new {id = model.Inventory.Id});
        }
        public IActionResult ProductTablePartial(int id)
        {
            var products = new InventoryViewModel
            {
                Inventory = new Inventory { Id = id },
                Products = _inventoryRepository.GetAllProducts(id)
            };
            products.Inventory.Products = _inventoryRepository.GetAllProducts(id);
            return PartialView("_ProductsTable", products);
        }
        [HttpGet]
        public IActionResult CalculateInventoryValue(int inventoryId)
        {
            var inventoryValue = GetValueAll(inventoryId);

            return Json(new { inventoryValue });
        }
        private decimal GetValueAll(int id)
        {
            decimal val = 0;
            var products = _inventoryRepository.GetAllProducts(id);
            foreach (var item in products)
            {
                val += item.ProductValue;
            };
            return val;
        }

        [HttpPost]
        public IActionResult AddProductDb(InventoryViewModel model, int inventoryId)
        {
            ModelState.Remove("Product.UnitId");
            ModelState.Remove("Inventory.Name");
            ModelState.Remove("Inventory.Company");
            if (ModelState.IsValid)
            {
                var productDb = new ProductsBase
                {
                    NameProductDb = model.Product.ProductName,
                    Code = model.Product.Code,
                };

                _inventoryRepository.AddProductDb(productDb);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _inventoryRepository.Delete(id, userId);
                
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                _inventoryRepository.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult EditProduct(Product prodToUpdate)
        {
            var updatedProduct = _inventoryRepository.GetProductById(prodToUpdate.Id);

            updatedProduct.Code = prodToUpdate.Code;
            updatedProduct.ProductName = prodToUpdate.ProductName;
            updatedProduct.Price = prodToUpdate.Price;
            updatedProduct.UnitId = prodToUpdate.UnitId;
            updatedProduct.Quantity = prodToUpdate.Quantity;
            updatedProduct.ProductValue = prodToUpdate.Price * prodToUpdate.Quantity;

            _inventoryRepository.Update(updatedProduct);

            return Json(new { success = true, updatedProduct });
        }
    }
}
