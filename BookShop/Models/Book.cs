using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Runtime.Versioning;
using BookShop.Properties;

namespace BookShop.Models
{
    public class Book
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resources))]
        [MaxLength(100, ErrorMessageResourceName = "NameLength", ErrorMessageResourceType = typeof(Resources.Resources))]
        [Display(Name = "Name Book", ShortName = "Name", Order = 0)]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessageResourceName = "NameLength", ErrorMessageResourceType = typeof(Resources.Resources))]
        [Display(Name = "Short Description", ShortName = "ShortDesc", Order = 1)]
        public string ShortDesc { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resources))]
        [RegularExpression("[0-9]*", ErrorMessageResourceName = "Page", ErrorMessageResourceType = typeof(Resources.Resources))]
        public int Pages { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resources))]
        [RegularExpression(@"^[0-9]*\.?[0-9]{0,2}$", ErrorMessageResourceName ="Price", ErrorMessageResourceType =typeof(Resources.Resources))]
        public ushort Price { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Publication", ShortName = "PublishDate", Order = 2)]
        public DateTime PublishDate { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resources))]
        [Display(Name = "Author", ShortName = "AuthorId", Order = 3)]
        public int AuthorId { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resources))]
        [Display(Name = "Theme", ShortName = "ThemeId", Order = 4)]
        public int ThemeId { get; set; }
        public HashSet<Author> Authors { get; set; }
    }
}
