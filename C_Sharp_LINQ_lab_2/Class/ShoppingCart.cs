using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_LINQ_lab_2.Class
{
    internal class ShoppingCart
    {
        internal int Article;
        internal int СonsumerId;
        internal string Sname;

        internal ShoppingCart(int article, int consumerId, string sname)
        {
            Article = article;
            СonsumerId = consumerId;
            Sname = sname;
        }
    }
}
