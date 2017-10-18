using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using CalculatorService.Models;

namespace CalculatorService.Facilities
{
    public static class JournalService
    {
        private const string STORE_PATH = "C:\\repos\\CalculatorServiceVS2015\\Calculator2\\CalculatorService\\";
        private static string name = string.Format("Store-{0:yyyy-MM-dd}.txt", DateTime.Now);

        #region Storing
        public static void StoreOperation(Models.Operations op)
        {
            string journal = GetJournal();
            using (StreamWriter sw = new StreamWriter($"{STORE_PATH}{name}"))
            {
                sw.WriteLine(journal);
                sw.WriteLine($"{ op.Operation} => {op.Calculation} || {op.Key} || { op.Date}");
                sw.Close();
            }
        }
        #endregion

        #region Clearing
        public static string ClearJournal()
        {
            string journal = "";
            using (StreamWriter sw = new StreamWriter($"{STORE_PATH}{name}", false))//true inserts the line at the end and false overwrites it
            {
                sw.WriteLine("------ Operation's history ------");
                sw.WriteLine("");
                sw.Close();
            }

            using (StreamReader sr = new StreamReader($"{STORE_PATH}{name}"))
            {
                journal = sr.ReadToEnd();
            }

            return journal.TrimEnd();
        }
        #endregion

        #region StoringOperation
        public static void Storing(string operation, string operationType, string key)
        {
            if (key != null)
            {
                StoreOperation(Operations.CreateOperation(operationType, operation, DateTime.Now, key));
            }
        }
        #endregion

        #region GetJournal
        public static string GetJournal()
        {
            string journal = "";

            if (!(File.Exists($"{STORE_PATH}{name}")))
            {
                using (StreamWriter sw = File.CreateText($"{STORE_PATH}{name}"))
                {
                    sw.WriteLine("------ Operation's history ------");
                    sw.Close();
                }
            }

            using (StreamReader sr = new StreamReader($"{STORE_PATH}{name}"))
            {
                journal = sr.ReadToEnd();
            }
            return journal.TrimEnd();
        }
        #endregion
    }
}