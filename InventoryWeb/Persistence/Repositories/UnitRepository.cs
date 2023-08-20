using InventoryWeb.Core;
using InventoryWeb.Core.Models.Domains;
using InventoryWeb.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryWeb.Persistence.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private IApplicationDbContext _context;
        public UnitRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Unit> GetUnit()
        {
            return _context.Units.ToList();
        }
        public IEnumerable<Unit> GetUnits()
        {
            return _context.Units.ToList();
        }

        public void Delete(int id)
        {
            var unit = _context.Units.Single(x => x.Id == id);            
            _context.Units.Remove(unit);
            _context.SaveChanges();
        }

        public void Add(Unit unit)
        {
            _context.Units.Add(unit);
            _context.SaveChanges();
        }

        public Unit GetUnit(int id)
        {
            return _context.Units.FirstOrDefault(x => x.Id == id);
        }
    }
}
