using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryWeb.Core.Models.Domains
{
    public class ProductsBase
    {
        public int Id { get; set; }
        [Display (Name = "Kod")]
        [Required(ErrorMessage = " ")]
        public string Code { get; set; }
        [Display(Name = "Nazwa Produktu")]
        [Required(ErrorMessage = " ")]
        public string NameProductDb { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Cena za jednostkę")]
        public decimal EachPrice { get; set; }
    }
}
