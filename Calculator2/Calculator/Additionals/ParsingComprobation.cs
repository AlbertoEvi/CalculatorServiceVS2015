using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Additionals
{
    class ParsingComprobation
    {
        public static bool CanBeParsed(char symb, string str, bool cond = false)
        {
            string[] nums  = SeparateString(symb, str);
            bool condition = true;
            for (int i = 0; i < nums.Length; i++)
            {
                int n;
                bool isNumeric;

                if (cond && nums[i].Length > 10)
                {
                    Console.WriteLine("One or more values introduced have more than 10 digits, try it again. ");
                    condition = false;
                    return condition;
                }
                if (!(isNumeric = int.TryParse(nums[i], out n)))
                {
                    Console.WriteLine("One or more of the values introduced aren't integers, try it again.");
                    condition = false;
                }
            }
            return condition;
        }

        public static string[] SeparateString(char symb, string str)
        {
            string[] nums = str.Split(symb);
            return nums;
        }

        public static int[] Parse(string[] nums)
        {
            int[] numbers = new int[nums.Length];

            for (int i = 0; i < nums.Length; i++)
            {
                numbers[i] = int.Parse(nums[i].Trim());
                Console.WriteLine(numbers[i]);
            }

            return numbers;
        }
    }
}
