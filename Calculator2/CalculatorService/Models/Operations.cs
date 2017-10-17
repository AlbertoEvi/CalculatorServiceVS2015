using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculatorService.Models
{
    public class Operations
    {
        #region Properties
        public string Operation { get; private set; }

        public string Calculation { get; private set; }

        public DateTime Date { get; private set; }

        public string Key { get; private set; }
        #endregion

        #region .Ctor
        public static Operations CreateOperation(string operation, string calculation, DateTime date, string key)
        {
            Operations ope = new Operations();

            ope.Operation = operation;
            ope.Calculation = calculation;
            ope.Date = date;
            ope.Key = key;

            return ope;
        }
        #endregion

        #region Add
        public static string Add(AddRequest petition, AddResponse result)
        {
            int[] nums = petition.Added;
            string operationLine = "";
            result.Result = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                result.Result += nums[i];
                if (i != nums.Length - 1)
                {
                    operationLine += $"{nums[i]} + ";
                }
                else
                {
                    operationLine += $"{nums[i]}";
                }
            }

            return $"{operationLine} = {result.Result}";
        }
        #endregion

        #region Subtract
        public static string Subt(SubtractRequest petition, SubtractResponse result)
        {
            int[] nums = petition.Numbers;
            string operationLine = "";
            result.Result = nums[0];

            for (int i = 0; i < nums.Length; i++)
            {
                if (i != 0)
                    result.Result -= nums[i];
                if (i != nums.Length - 1)
                {
                    operationLine += $"{nums[i]} - ";
                }
                else
                {
                    operationLine += $"{nums[i]}";
                }
            }

            return $"{operationLine} = {result.Result}";
        }
        #endregion

        #region Multiply
        public static string Mult(MultRequest petition, MultResponse result)
        {
            int[] nums = petition.Multipliers;
            string operationLine = "";
            result.Result = 1;

            for (int i = 0; i < nums.Length; i++)
            {
                result.Result *= nums[i];

                if (i != nums.Length - 1)
                {
                    operationLine += $"{nums[i]} - ";
                }
                else
                {
                    operationLine += $"{nums[i]}";
                }
            }

            return $"{operationLine} = {result.Result}";
        }
        #endregion

        #region Division
        public static string Div(DivRequest petition, DivResponse result)
        {
            int[] nums = petition.Numbers;
            string operationLine = "";

            result.Quotient = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                result.Quotient /= nums[i];

                if (i != nums.Length - 1)
                {
                    operationLine += $"{nums[i]} / ";
                }
                else
                {
                    result.Remainder = result.Quotient % nums[nums.Length - 1];
                    operationLine += $"{nums[i]}";
                }
            }

            return $"{operationLine} = {result.Quotient} | Remainder = {result.Remainder}";
        }
        #endregion

        #region SquareRoot
        public static string Sqr(SquareRootRequest petition, SquareRootResponse result)
        {
            double? num = petition.Number;
            string operationLine = "";

            result.Result = Math.Sqrt((double)num);

            operationLine = $"v-- {num}";

            return $"{operationLine} = {result.Result}";
        }
        #endregion
    }
}