using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_LINQ_lab_2.Class
{
    internal class Debtor
    {
        internal double Debt;
        internal string Sname;
        internal int Anumber;
        internal Debtor(double debt, string sname, int anumber)
        {
            Debt = debt;
            Sname = sname;
            Anumber = anumber;
        }
    }
}
