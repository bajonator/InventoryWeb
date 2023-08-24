using InventoryWeb.Persistence.Repositories;
using InventoryWeb.Persistence;
using Microsoft.AspNetCore.Mvc;
using InventoryWeb.Persistence.Extensions;
using InventoryWeb.Core.Models.Domains;
using InventoryWeb.Core.ViewModels;
using System;
using Microsoft.EntityFrameworkCore;
using InventoryWeb.Persistence.Services;
using InventoryWeb.Core.Service;
using InventoryWeb.Core.Repositories;
using Newtonsoft.Json;

namespace InventoryWeb.Controllers
{
    public class UnitController : Controller
    {
        private IUnitService _unitService;
        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }
        public IActionResult Units()
        {
            var units = _unitService.GetUnits();
            var vm = new UnitViewModel();
            vm.Units = units;

            return View(units);
        }
        public IActionResult Unit()
        {
            var vm = new UnitViewModel
            {
                Units = _unitService.GetUnits()
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _unitService.Delete(id);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUnit(Unit unit)
        {
            unit = new Unit
            {
                UnitName = unit.UnitName,
            };
            ModelState.Remove("unit.Id");

            if (!ModelState.IsValid)
            {
                return View(unit);
            }
            _unitService.Add(unit);
            return RedirectToAction("UnitsTablePartial");
        }
        public IActionResult UnitsTablePartial()
        {
            var units = new UnitViewModel
            {
                Units = _unitService.GetUnits(),
            };
            return PartialView("_UnitsTable", units);
        }
    }
}
