using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace InventoryWeb.Core.Models.Domains
{
    public class Unit
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Pole Nazwa jednostki jest wymagane.")]
        [Display(Name = "Nazwa jednostki")]
        public string UnitName { get; set; }
    }
}
