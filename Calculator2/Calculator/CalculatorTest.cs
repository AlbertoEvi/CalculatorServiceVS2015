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
        
        #region Testing
        public void Test()
        {
            int[] numbers = new int[] { 40, 22};

            Console.WriteLine("------ Here we test every Calculator's method  ------");
            logger.Info("------ Here we test every Calculator's method ------");

            Add(numbers, "http://localhost:51419/Calculator/add");
            Add(new int[] { }, "http://localhost:51419/Calculator/add");

            Sub(numbers, "http://localhost:51419/Calculator/sub");
            Sub(new int[] { }, "http://localhost:51419/Calculator/sub");

            Mult(numbers, "http://localhost:51419/Calculator/mult");
            Mult(new int[] { }, "http://localhost:51419/Calculator/mult");

            Div(numbers, "http://localhost:51419/Calculator/div");
            Div(new int[] { }, "http://localhost:51419/Calculator/div");

            SquareRoot(25, "http://localhost:51419/Calculator/sqr");
            /*double dbl = double.Parse("");
            SquareRoot(dbl, "http://localhost:51419/Calculator/sqr");*/

            getHistory("http://localhost:51419/Calculator/history");
        }
        #endregion

        #region History
        public void getHistory(string url)
        {
            logger.Info(url);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("X_Evi_Tracking_Id", "This are the operations from the autotesting");

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

        public void clearHistory(string url)
        {
            logger.Info(url);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("X_Evi_Tracking_Id", "This are the operations from the autotesting");

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
        #endregion

        #region Add
        public void Add(int[] numbers, string url)
        {
            string jsonRequest = "";
            Console.WriteLine($"Operation: {string.Join("+", numbers)}");
            
            logger.Info(url);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            req.Headers.Add("X_Evi_Tracking_Id", "This are the operations from the autotesting");

            using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
            {
                AddRequest add = new AddRequest();
                add.Added = numbers;
                jsonRequest = JsonConvert.SerializeObject(add);
                sw.WriteLine(jsonRequest);
                sw.Close();
            }
            
            string resp;
            HttpWebResponse Response = (HttpWebResponse)req.GetResponse();

            using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
            {
                resp = sr.ReadToEnd();
                sr.Close();
                Response.Close();
            }
            
            Console.WriteLine("The server response is a: ");
            Console.WriteLine(resp);
            
            logger.Info($"The server response is: {resp}");
            logger.Info("---------");
        }
        #endregion

        #region Sub
        public void Sub(int[] numbers, string url)
        {
            string jsonRequest = "";
            Console.WriteLine($"Operation: {string.Join("-", numbers)}");

            logger.Info(url);
            
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            req.Headers.Add("X_Evi_Tracking_Id", "This are the operations from the autotesting");

            using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
            {
                SubtractRequest sub = new SubtractRequest();
                sub.Numbers = numbers;
                jsonRequest = JsonConvert.SerializeObject(sub);
                sw.WriteLine(jsonRequest);
                sw.Close();
            }

            string resp;
            HttpWebResponse Response = (HttpWebResponse)req.GetResponse();
            
            using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
            {
                resp = sr.ReadToEnd();
                sr.Close();
                Response.Close();
            }
            
            Console.WriteLine("The server response is: ");
            Console.WriteLine(resp);
            
            logger.Info($"The server response is: {resp}");
            logger.Info("---------");
        }
        #endregion

        #region Mult
        public void Mult(int[] numbers, string url)
        {
            string jsonRequest = "";
            Console.WriteLine($"Operation: {string.Join("*", numbers)}");

            logger.Info(url);
            
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            req.Headers.Add("X_Evi_Tracking_Id", "This are the operations from the autotesting");
            
            using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
            {
                MultRequest mult = new MultRequest();
                mult.Multipliers = numbers;
                jsonRequest = JsonConvert.SerializeObject(mult);
                sw.WriteLine(jsonRequest);
                sw.Close();
            }
            
            string resp;
            HttpWebResponse Response = (HttpWebResponse)req.GetResponse();

            using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
            {
                resp = sr.ReadToEnd();
                sr.Close();
                Response.Close();
            }
            
            Console.WriteLine("The server response is: ");
            Console.WriteLine(resp);
            
            logger.Info($"The server response is: {resp}");
            logger.Info("---------");
        }
        #endregion

        #region Div
        public void Div(int[] numbers, string url)
        {
            string jsonRequest = "";
            Console.WriteLine($"Operation: {string.Join("/", numbers)}");
            
            logger.Info(url);
            
            DivRequest div = new DivRequest();

            if (numbers.Length != 0)
            {
                div.Dividend = numbers[0];
                div.Divisor = numbers[1];
            }

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            req.Headers.Add("X_Evi_Tracking_Id", "This are the operations from the autotesting");

            using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
            {
                jsonRequest = JsonConvert.SerializeObject(div);
                sw.WriteLine(jsonRequest);
                sw.Close();
            }
            
            string resp;
            HttpWebResponse Response = (HttpWebResponse)req.GetResponse();

            using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
            {
                resp = sr.ReadToEnd();
                sr.Close();
                Response.Close();
            }
            
            Console.WriteLine("The server response is: ");
            Console.WriteLine(resp);

            logger.Info($"The server response is: {resp}");
            logger.Info("---------");
        }
        #endregion

        #region SquareRoot
        public void SquareRoot(double number, string url)
        {
            string jsonRequest = "";
            Console.WriteLine($"Operation: v-- {number}");

            logger.Info(url);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            req.Headers.Add("X_Evi_Tracking_Id", "This are the operations from the autotesting");

            using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
            {
                SquareRootRequest sqr = new SquareRootRequest();
                sqr.Number = number;
                jsonRequest = JsonConvert.SerializeObject(sqr);
                sw.WriteLine(jsonRequest);
                sw.Close();
            }

            string resp;
            HttpWebResponse Response = (HttpWebResponse)req.GetResponse();

            using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
            {
                resp = sr.ReadToEnd();
                sr.Close();
                Response.Close();
            }

            Console.WriteLine("The server response is: ");
            Console.WriteLine(resp);

            logger.Info($"The server response is: {resp}");
            logger.Info("---------");
        }
        #endregion
    }
}
