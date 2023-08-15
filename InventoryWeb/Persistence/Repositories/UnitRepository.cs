using InventoryWeb.Core.Models.Domains;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryWeb.Persistence.Repositories
{
    public class UnitRepository
    {
        private ApplicationDbContext _context;
        public UnitRepository(ApplicationDbContext context)
        {
            _context = context;
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
