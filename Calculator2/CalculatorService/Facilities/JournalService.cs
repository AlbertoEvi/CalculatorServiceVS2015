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

        public static void StoreOperation(Models.Operations op)
        {
            string journal = GetJournal();
            using (StreamWriter sw = new StreamWriter(STORE_PATH))
            {
                sw.WriteLine(journal);
                sw.WriteLine($"{ op.operation} => {op.Calculation} || {op.Key} || { op.Date}");
                sw.Close();
            }
        }
        public static string GetJournal()
        {
            string journal = "";

            if (!(File.Exists(STORE_PATH)))
            {
                using (StreamWriter sw = File.CreateText(STORE_PATH))
                {
                    sw.WriteLine("* * * * History of Operations * * * *");
                    sw.Close();
                }
            }

            using (StreamReader sr = new StreamReader(STORE_PATH))
            {
                journal = sr.ReadToEnd();
            }
            return journal.TrimEnd();
        }
    }
}