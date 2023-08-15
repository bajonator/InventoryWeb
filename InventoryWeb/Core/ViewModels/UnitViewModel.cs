using InventoryWeb.Core.Models.Domains;
using System.Collections;
using System.Collections.Generic;

namespace InventoryWeb.Core.ViewModels
{
    public class UnitViewModel
    {
        public string Heading { get; set; }
        public Unit Unit { get; set; }
        public IEnumerable<Unit> Units { get; set; }
    }
}
