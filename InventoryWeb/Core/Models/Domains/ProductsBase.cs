using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryWeb.Core.Models.Domains
{
    public class ProductsBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display (Name = "Kod")]
        public string Code { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Nazwa Produktu")]
        public string NameProductDb { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Cena za jednostkę")]
        public decimal EachPrice { get; set; }
    }
}
