using InventoryWeb.Persistence;
using InventoryWeb.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using InventoryWeb.Persistence.Extensions;
using System.Drawing;
using InventoryWeb.Core.Models.Domains;

using InventoryWeb.Core.ViewModels;
using DinkToPdf.Contracts;
using DinkToPdf;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Routing;

namespace InventoryWeb.Controllers
{
    public class PrintController : Controller
    {
        private InventoryRepository _inventoryRepository;
        private IConverter _converter;
        private IRazorViewEngine _viewEngine;
        private ITempDataProvider _tempDataProvider;
        private IServiceProvider _serviceProvider;

        public PrintController(ApplicationDbContext context, IConverter converter, IRazorViewEngine viewEngine,
                         ITempDataProvider tempDataProvider, IServiceProvider serviceProvider)
        {
            _inventoryRepository = new InventoryRepository(context);
            _converter = converter;
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }
        public async Task<IActionResult> InventoryToPdf(Inventory inventory)
        {
            var inventoryViewModel = new InventoryViewModel
            {
                Inventory = inventory,
                Products = _inventoryRepository.GetProduct(inventory.Id)
            };

            var htmlContent = await RenderViewToStringAsync("Print/InventoryPrintTemplate", inventoryViewModel);

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait,
            },
                Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = htmlContent,
                    }
                }
            };

            var pdfBytes = _converter.Convert(doc);
            return File(pdfBytes, "application/pdf", $"Inventaryzacja_{inventory.Id}.pdf");
        }
        private async Task<string> RenderViewToStringAsync(string viewName, object model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ControllerActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = _viewEngine.FindView(actionContext, viewName, false);
                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }

        public ActionResult DownloadInventoryPdf(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] == null)
                throw new Exception("Błąd przy próbie eksportu faktury do PDF.");

            var data = TempData[fileGuid] as byte[];
            return File(data, "application/pdf", fileName);
        }

        public ActionResult PrintInventory(int id)
        {
            var userId = User.GetUserId();
            var inventory = new InventoryViewModel
            {
                Inventory = _inventoryRepository.GetInventory(userId, id),
                Products = _inventoryRepository.GetProduct(id),
            };
            inventory.Inventory.Value = GetValueAll(inventory);
            return View("InventoryPrintTemplate", inventory);
        }
        private decimal GetValueAll(InventoryViewModel products)
        {
            decimal val = 0;
            foreach (var item in products.Products)
            {
                val += item.ProductValue;
            };
            return val;
        }
    }
}
