using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AshZoneModels.Models
{
    public class ShopingCart
    {
        public ShopingCart()
        {
            Count = 1;
        }

        public int Id { get; set; }

        public string AppUserId { get; set; }

        [NotMapped]
        [ForeignKey("AppUserId")]
        public virtual AppUser ApplicationUser { get; set; }

        public int ProductId { get; set; }

        [NotMapped]
        [ForeignKey("ProductID")]
        public virtual Product Productitem { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Please Select the quantity")]
        public int Count { get; set; }
    }
}
