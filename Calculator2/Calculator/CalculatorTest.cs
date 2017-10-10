using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Net;
using System.IO;
using Calculator.Models;
using Newtonsoft.Json;

namespace Calculator
{
    public class CalculatorTest
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Add
        public void Add(int[] numbers, string url)
        {
            string jsonRequest = "";
            Console.WriteLine($"Operacion: {string.Join("+", numbers)}");
            logger.Info(url);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            req.Headers.Add("X_Evi_Tracking_Id", "Test");

            using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
            {
                AddRequest add = new AddRequest();
                add.Added = numbers;
                jsonRequest = JsonConvert.SerializeObject(add);
                sw.WriteLine(jsonRequest);
                sw.Close();
            }

            string resp;
            AddResponse response = new AddResponse();
            HttpWebResponse Response = (HttpWebResponse)req.GetResponse();

            using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
            {
                resp = sr.ReadToEnd();
                sr.Close();
                Response.Close();
            }

            Console.WriteLine("The server responds: ");
            Console.WriteLine(resp);

            logger.Info($"The server responds: {resp}");
            logger.Info("-------------------------------------------------------------");
        }
        #endregion

        public void getHistory(string url)
        {
            logger.Info(url);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("X_Evi_Tracking_Id", "Test");

            string history;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                history = sr.ReadToEnd();
                sr.Close();
                response.Close();
            }
            Console.WriteLine(history);
        }
    }
}
