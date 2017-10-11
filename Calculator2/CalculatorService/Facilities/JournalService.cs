using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace CalculatorService.Facilities
{
    public class JournalService
    {
        private const string STORE_PATH = "C:\\repos\\CalculatorServiceVS2015\\Calculator2\\CalculatorService\\Store.txt";

        #region Storing
        public static void StoreOperation(Models.Operations op)
        {
            string journal = GetJournal();
            using (StreamWriter sw = new StreamWriter(STORE_PATH))
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
            using (StreamWriter sw = new StreamWriter(STORE_PATH,false))//true inserts the line at the end and false overwrites it
            {
                sw.WriteLine("------ Operation's history ------");
                sw.WriteLine("");
                sw.Close();
            }

            using (StreamReader sr = new StreamReader(STORE_PATH))
            {
                journal = sr.ReadToEnd();
            }

            return journal.TrimEnd();
        }
        #endregion

        #region GetJournal
        public static string GetJournal()
        {
            string journal = "";

            if (!(File.Exists(STORE_PATH)))
            {
                using (StreamWriter sw = File.CreateText(STORE_PATH))
                {
                    sw.WriteLine("------ Operation's history ------");
                    sw.Close();
                }
            }

            using (StreamReader sr = new StreamReader(STORE_PATH))
            {
                journal = sr.ReadToEnd();
            }
            return journal.TrimEnd();
        }
        #endregion
    }
}