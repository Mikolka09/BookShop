using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public HashSet<Book> Books { get; set; }
    }
}
