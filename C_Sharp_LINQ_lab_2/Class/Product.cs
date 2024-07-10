using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_LINQ_lab_2.Class
{
    internal class Product
    {
        internal string Category;
        internal int Article;
        internal string Country;
        internal Product( string category, int article, string country)
        {
            Category = category;
            Article = article;
            Country = country;
        }
    }
}
