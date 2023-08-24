using InventoryWeb.Core.Models.Domains;
using InventoryWeb.Core.Service;
using InventoryWeb.Core.ViewModels;
using InventoryWeb.Persistence;
using InventoryWeb.Persistence.Extensions;
using InventoryWeb.Persistence.Repositories;
using InventoryWeb.Persistence.Services;
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
        private IUnitService _unitService;
        private IInventoryService _inventoryService;
        private IProductBaseService _productBaseService;
        public InventoryController(IUnitService unitService, IInventoryService inventoryService, IProductBaseService productBaseService)
        {
            _unitService = unitService;
            _inventoryService = inventoryService;
            _productBaseService = productBaseService;
        }
        public IActionResult Inventory()
        {
            var userId = User.GetUserId();

            var vm = new InventoriesViewModel
            {
                Inventory = _inventoryService.GetInventory(userId, 0),
                Inventories = _inventoryService.Get(userId),
                Product = new Product()
            };
            return View(vm);
        }
        public IActionResult InventoryPreview(int id)
        {
            var inv = _inventoryService.GetPreview(id);
            var products = _inventoryService.GetAllProducts(id);

            var vm = new InventoryViewModel
            {
                Inventory = inv,
                Products = _inventoryService.GetProduct(inv.Id),
                Units = _unitService.GetUnit(),
                Product = new Product()
            };
            vm.Inventory.Value = _inventoryService.GetValueAll(id);

            return View("InventoryPreview", vm);
        }

        public IActionResult GetInventoryProducts(int inventoryId)
        {
            var products = _inventoryService.GetProduct(inventoryId);
            var viewModel = new InventoryViewModel
            {
                Products = products
            };
            return RedirectToAction("InventoryPreview", viewModel);
        }
        public IActionResult FindProducts(string kodinput)
        {
            var product = _productBaseService.GetProductBase(kodinput);
            if (product != null)
            {
                var productName = product.NameProductDb;
                var productPrice = product.EachPrice;
                return Json(new { success = true, productName, productPrice });
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
                _inventoryService.AddInventory(inventory);

                return RedirectToAction("Inventory");
            }
            else
                return View(inventory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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

            if (!ModelState.IsValid)
            {
                return View(product);
            }
            _inventoryService.AddProduct(product);

            return RedirectToAction("ProductTablePartial", new { id = model.Inventory.Id });
        }
        public IActionResult ProductTablePartial(int id)
        {
            var products = new InventoryViewModel
            {
                Inventory = new Inventory { Id = id },
                Products = _inventoryService.GetAllProducts(id)
            };
            products.Inventory.Products = _inventoryService.GetAllProducts(id);
            return PartialView("_ProductsTable", products);
        }
        [HttpGet]
        public IActionResult CalculateInventoryValue(int inventoryId)
        {
            var inventoryValue = _inventoryService.GetValueAll(inventoryId);

            return Json(new { inventoryValue });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _inventoryService.Delete(id, userId);
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
                _inventoryService.DeleteProduct(id);
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
            var updatedProduct = _inventoryService.GetProductById(prodToUpdate.Id);

            updatedProduct.Code = prodToUpdate.Code;
            updatedProduct.ProductName = prodToUpdate.ProductName;
            updatedProduct.Price = prodToUpdate.Price;
            updatedProduct.UnitId = prodToUpdate.UnitId;
            updatedProduct.Quantity = prodToUpdate.Quantity;
            updatedProduct.ProductValue = prodToUpdate.Price * prodToUpdate.Quantity;

            _inventoryService.Update(updatedProduct);

            return Json(new { success = true, updatedProduct });
        }
    }
}
