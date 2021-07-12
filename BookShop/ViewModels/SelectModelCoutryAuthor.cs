using BookShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.ViewModels
{
    public class SelectModelCoutryAuthor
    {
        public IEnumerable<Countries> CountryList { get; set; }

        public IEnumerable<Author> AuthorList { get; set; }
    }
}
