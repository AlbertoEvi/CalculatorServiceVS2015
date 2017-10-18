using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Models;
using Calculator.Additionals;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using NLog;

namespace Calculator
{
    public class Mediator
    {
        public static string url = "http://localhost:51419/Calculator/";
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region Add
        public void Add(string trackingId)
        {
            #region AddStuff
            Console.WriteLine("------Addition Operation------");
            Console.WriteLine("Type the integer sum you want to do(ex:13 + 12 + 5): ");

            char symb = '+';
            string sum = Console.ReadLine();

            sum = CheckingSymbol.HaveOperationSymbol(symb, sum);

            int[] numbers;
            if (ParsingComprobation.CanBeParsed(symb, sum)) {
                numbers = ParsingComprobation.Parse(ParsingComprobation.SeparateString(symb, sum));
            } else {
                return;
            }
            #endregion

            #region Connection
            AddRequest addition = new AddRequest();
            AddResponse result = new AddResponse();

            addition.Added = numbers;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}Add");
            request.Method = "POST";
            request.ContentType = "application/json";

            if (trackingId != "")
            {
                request.Headers.Add("X_Evi_Tracking_Id", trackingId);
            }

            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                var jason = JsonConvert.SerializeObject(addition);
                dataStream.Write(jason);
                dataStream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader stRead = new StreamReader(response.GetResponseStream()))
            {
                Console.WriteLine("The server operation's result is:");
                result = JsonConvert.DeserializeObject<AddResponse>(stRead.ReadToEnd());
                Console.WriteLine(result.Result);
                stRead.Close();
            }
            #endregion
        }
        #endregion

        #region Subtraction
        public void Subt(string trackingId)
        {
            #region SubtractionStuff
            Console.WriteLine("------Subtraction Operation------");
            Console.WriteLine("Type the integer subtraction you want to do(ex:13 - 12 - 5): ");

            char symb = '-';
            string subt = Console.ReadLine();
            while (!subt.Contains('-') || subt.Contains('+') || subt.Contains('/') || subt.Contains('*'))
            {
                Console.WriteLine("Error at the reading of the string, please write it again(the operation symbol is not valid to the subtraction): ");
                subt = Console.ReadLine();
            }
            int[] numbers;
            if (ParsingComprobation.CanBeParsed(symb, subt))
            {
                numbers = ParsingComprobation.Parse(ParsingComprobation.SeparateString(symb, subt));
            }
            else
            {
                return;
            }
            #endregion

            #region Connection
            SubtractRequest subtract = new SubtractRequest();
            SubtractResponse result = new SubtractResponse();

            subtract.Numbers = numbers;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}sub");
            request.Method = "POST";
            request.ContentType = "application/json";

            if (trackingId != "")
            {
                request.Headers.Add("X_Evi_Tracking_Id", trackingId);
            }

            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                var jason = JsonConvert.SerializeObject(subtract);
                dataStream.Write(jason);
                dataStream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader stRead = new StreamReader(response.GetResponseStream()))
            {
                Console.WriteLine("The server operation's result is:");
                result = JsonConvert.DeserializeObject<SubtractResponse>(stRead.ReadToEnd());
                Console.WriteLine(result.Result);
                stRead.Close();
            }
            #endregion
        }
        #endregion

        #region Multiply
        public void Mult(string trackingId)
        {
            #region MultiplicationStuff
            Console.WriteLine("------Multplication Operation------");
            Console.WriteLine("Type the integer multiplication you want to do, multipliers must have 10 or less digits(ex:13 * 12 * 5): ");

            char symb = '*';
            string mult = Console.ReadLine();
            while (!mult.Contains('*') || mult.Contains('+') || mult.Contains('/') || mult.Contains('-'))
            {
                Console.WriteLine("Error at the reading of the string, please write it again(the operation symbol is not valid to the multiplication): ");
                mult = Console.ReadLine();
            }
            
            int[] numbers;
            if (ParsingComprobation.CanBeParsed(symb, mult, true))
            {
                numbers = ParsingComprobation.Parse(ParsingComprobation.SeparateString(symb, mult));
            }
            else
            {
                return;
            }
            #endregion

            #region Connection
            MultRequest multiply = new MultRequest();
            MultResponse result = new MultResponse();

            multiply.Multipliers = numbers;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}mult");
            request.Method = "POST";
            request.ContentType = "application/json";

            if (trackingId != "")
            {
                request.Headers.Add("X_Evi_Tracking_Id", trackingId);
            }

            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                var jason = JsonConvert.SerializeObject(multiply);
                dataStream.Write(jason);
                dataStream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader stRead = new StreamReader(response.GetResponseStream()))
            {
                Console.WriteLine("The server operation's result is:");
                result = JsonConvert.DeserializeObject<MultResponse>(stRead.ReadToEnd());
                Console.WriteLine(result.Result);
                stRead.Close();
            }
            #endregion
        }
        #endregion

        #region Division
        public void Div(string trackingId)
        {
            #region DivisionStuff
            Console.WriteLine("-------Division Operation------");
            Console.WriteLine("Type the integer division you want to do(ex:1243 / 12 / 2): ");

            char symb = '/';
            string div = Console.ReadLine();
            while (!div.Contains('/') || div.Contains('+') || div.Contains('*') || div.Contains('-'))
            {
                Console.WriteLine("Error at the reading of the string, please write it again(the operation symbol is not valid to the division): ");
                div = Console.ReadLine();
            }

            int[] numbers;
            if (ParsingComprobation.CanBeParsed(symb, div))
            {
                numbers = ParsingComprobation.Parse(ParsingComprobation.SeparateString(symb, div));
            }
            else
            {
                return;
            }
            #endregion

            #region Connection
            DivRequest division = new DivRequest();
            DivResponse result = new DivResponse();

            division.Numbers = numbers;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}div");
            request.Method = "POST";
            request.ContentType = "application/json";

            if (trackingId != "")
            {
                request.Headers.Add("X_Evi_Tracking_Id", trackingId);
            }

            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                var jason = JsonConvert.SerializeObject(division);
                dataStream.Write(jason);
                dataStream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader stRead = new StreamReader(response.GetResponseStream()))
            {
                Console.WriteLine("The server operation's result is:");
                result = JsonConvert.DeserializeObject<DivResponse>(stRead.ReadToEnd());
                Console.WriteLine(result.Quotient);
                Console.WriteLine("The remainder of the last division is:");
                Console.WriteLine(result.Remainder);
                stRead.Close();
            }
            #endregion
        }
        #endregion

        #region SquareRoot
        public void Square(string trackingId)
        {
            #region SquareRootStuff
            Console.WriteLine("-------Square Root Operation------");
            Console.WriteLine("Type the number to make the square root of it(ex: 12): ");

            string sqr = Console.ReadLine();

            double sr;
            bool isNumeric;

            if (isNumeric = double.TryParse(sqr, out sr))
            {
                sr = double.Parse(sqr);
                Console.WriteLine(sr);
            }
            else
            {
                Console.WriteLine("The value introduced isn't valid, try it again.");
                return;
            }
            #endregion

            #region Connection
            SquareRootRequest squareRoot = new SquareRootRequest();
            SquareRootResponse result = new SquareRootResponse();

            squareRoot.Number = sr;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}sqr");
            request.Method = "POST";
            request.ContentType = "application/json";

            if (trackingId != "")
            {
                request.Headers.Add("X_Evi_Tracking_Id", trackingId);
            }

            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                var jason = JsonConvert.SerializeObject(squareRoot);
                dataStream.Write(jason);
                dataStream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader stRead = new StreamReader(response.GetResponseStream()))
            {
                Console.WriteLine("The server operation's result is:");
                result = JsonConvert.DeserializeObject<SquareRootResponse>(stRead.ReadToEnd());
                Console.WriteLine(result.Result);
                stRead.Close();
            }
            #endregion
        }
        #endregion
        
        #region Journal
        public void Journal(string trackingId)
        {
            logger.Info(url);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{url}history");
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("X_Evi_Tracking_Id", trackingId);

            string history = "", line = "";
            char[] sep = new char[] { '|' };
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                line = sr.ReadLine();
                if (trackingId != "")
                {
                    while (line != null)
                    {
                        line = sr.ReadLine();
                        if (line != "------ History of Operations that you carried out in the app's launch this time ------" && line != null)
                        {
                            string id = line.Split(sep)[2].Trim();
                            if (id == trackingId)
                            {
                                history += line + "\n";
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No Tracking Id");
                }
                sr.Close();
                response.Close();
            }
            Console.WriteLine("------ History of Operations that you carry out in this app's launch ------");
            Console.WriteLine(history);
        }
        #endregion
    }
}

