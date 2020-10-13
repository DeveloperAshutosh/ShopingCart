using AshZoneModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AshZoneModels.ViewModels
{
    public class OrderDetailsCart
    {
        public List<ShopingCart> ListCart { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
