using BookShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.ViewModels
{
    public class SelectModelAuthorsThemes
    {
        public IEnumerable<Author> AuthorList { get; set; }

        public IEnumerable<Themes> ThemeList { get; set; }
        public IEnumerable<Book> BookList { get; set; }
    }
}
