using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace InventoryWeb.Core.Models.Domains
{
    public class Unit
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa jednostki")]
        public string UnitName { get; set; }
    }
}
