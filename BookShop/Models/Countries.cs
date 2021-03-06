using BookShop.Properties;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models
{
    public class Countries
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resources))]
        [MaxLength(50, ErrorMessageResourceName = "NameLength", ErrorMessageResourceType = typeof(Resources.Resources))]
        [Display(Name = "Country", ShortName = "Name", Order = 0)]
        public string Name { get; set; }
    }
}
