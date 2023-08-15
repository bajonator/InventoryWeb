using InventoryWeb.Persistence.Repositories;
using InventoryWeb.Persistence;
using Microsoft.AspNetCore.Mvc;
using InventoryWeb.Persistence.Extensions;
using InventoryWeb.Core.Models.Domains;
using InventoryWeb.Core.ViewModels;
using System;
using Microsoft.EntityFrameworkCore;

namespace InventoryWeb.Controllers
{
    public class UnitController : Controller
    {
        private UnitRepository _unitRepository;
        public UnitController(ApplicationDbContext context)
        {
            _unitRepository = new UnitRepository(context);
        }
        public IActionResult Units()
        {
            var units = _unitRepository.GetUnits();
            var vm = new UnitViewModel();
            vm.Units = units;

            return View(units);
        }
        public IActionResult Unit()
        {
            var vm = new UnitViewModel
            {
                Units = _unitRepository.GetUnits()
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _unitRepository.Delete(id);
            }
            catch (Exception ex)
            {
                //logowanie
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult AddUnit(Unit unit)
        {
            ModelState.Remove("unit.Id");
            if (ModelState.IsValid)
            {
                unit = new Unit
                {
                    UnitName = unit.UnitName,
                };
                _unitRepository.Add(unit);
                return RedirectToAction("Unit");
            }
            return RedirectToAction("Unit", unit);
        }
    }
}
