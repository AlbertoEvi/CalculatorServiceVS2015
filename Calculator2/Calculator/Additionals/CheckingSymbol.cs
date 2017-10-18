using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Additionals
{
    class CheckingSymbol
    {
        public static string HaveOperationSymbol(char symb, string str)
        {
            while (!str.Contains(symb) || !ParsingComprobation.CanBeParsed(symb,str))
            {
                Console.WriteLine("Please write the operation again, one or more of the operation symbols is not valid. ");
                str = Console.ReadLine();
            }
            return str;
        }
    }
}
