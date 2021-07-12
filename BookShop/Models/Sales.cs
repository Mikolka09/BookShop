using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public ushort Price { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
        public int BookId { get; set; }
        public int ShopId { get; set; }
    }
}
