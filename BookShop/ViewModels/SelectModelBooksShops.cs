using BookShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.ViewModels
{
    public class SelectModelBooksShops
    {
        public IEnumerable<Book> BooksList { get; set; }
        public IEnumerable<Shops> ShopsList { get; set; }
        public IEnumerable<Sales> SalesList { get; set; }
    }
}
