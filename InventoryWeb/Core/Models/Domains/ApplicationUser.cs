using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace InventoryWeb.Core.Models.Domains
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Products = new Collection<Product>();
            Inventories = new Collection<Inventory>();
        }

        public ICollection<Product> Products { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
    }
}
