using BookShop.Properties;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models
{
    public class Sales
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resources))]
        [RegularExpression(@"^[0-9]*\.?[0-9]{0,2}$", ErrorMessageResourceName = "Price", ErrorMessageResourceType = typeof(Resources.Resources))]
        public ushort Price { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resources))]
        [RegularExpression("[0-9]*", ErrorMessageResourceName = "Page", ErrorMessageResourceType = typeof(Resources.Resources))]
        public int Quantity { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resources))]
        [DataType(DataType.Date)]
        [Display(Name = "Sale Date", ShortName = "SaleDate", Order = 0)]
        public DateTime SaleDate { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resources))]
        [Display(Name = "Book", ShortName = "BookId", Order = 0)]
        public int BookId { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resources))]
        [Display(Name = "Shop", ShortName = "ShopId", Order = 1)]
        public int ShopId { get; set; }
    }
}
