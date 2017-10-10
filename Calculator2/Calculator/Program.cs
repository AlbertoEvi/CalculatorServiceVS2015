using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator
{
    public class Program
    {
        static Mediator med = new Mediator();
        public static CalculatorTest test = new CalculatorTest();

        #region Storing
        public static string StoringId()
        {
            string idOperation = "";
            Console.WriteLine("Do you want to make a saving of the operation you are going to carry out?(Yes(Y)/No(N))");
            string command = Console.ReadLine();
            switch (command.ToLowerInvariant().Trim())
            {
                case "yes":
                case "y":
                    do
                    {
                        Console.WriteLine("Type the id you want to use to make the saving: ");
                        idOperation = Console.ReadLine();
                    } while (String.IsNullOrEmpty(idOperation) && String.IsNullOrWhiteSpace(idOperation));
                    break;
                case "no":
                case "n":
                    Console.WriteLine("The operation won't be stored");
                    break;
                default:
                    Console.WriteLine("Command introduced isn't listed. Default response: \"no\".");
                    break;
            }
            return idOperation;
        }
        #endregion

        #region Menu
        public static string CalcMenu(string trackingId)
        {
            Console.WriteLine("Welcome to our Calculator's Menu. Type an option to start working or \"Exit\" to finish and go out.");
            Console.WriteLine(" 1. Addition");
            Console.WriteLine(" 2. Subtraction");
            Console.WriteLine(" 3. Multiply");
            Console.WriteLine(" 4. Division");
            Console.WriteLine(" 5. Square root");
            Console.WriteLine(" 6. History");
            Console.WriteLine(" Exit ");

            string opt = Console.ReadLine().ToLowerInvariant().Trim();
            switch (opt)
            {
                case "1":
                case "addition":
                    med.Add(trackingId);
                    break;
                case "2":
                case "subtraction":
                    med.Subt(trackingId);
                    break;
                case "3":
                case "multiply":
                    med.Mult(trackingId);
                    break;
                case "4":
                case "division":
                    med.Div(trackingId);
                    break;
                case "5":
                case "square root":
                case "squareroot":
                case "square-root":
                    med.Square(trackingId);
                    break;
                case "6":
                case "history":
                    History(trackingId);
                    break;
                case "exit":
                    Environment.Exit(255);
                    break;
                default:
                    Console.WriteLine("The command introduced is invalid. The options you can type are: addition(1), subtraction(2), multiply(3), division(4) or exit(5)");
                    CalcMenu(trackingId);
                    break;
            }
            return opt;
        }
        #endregion

        #region HistoryJournal
        public static void History(string trackingId)
        {
            if (trackingId != "")
            {
                med.Journal(trackingId);
            }
            else
            {
                test.getHistory("http://localhost:51419/Calculator/history");
            }

        }
        #endregion

        #region ProgramCalls
        static void Main(string[] args)
        {
            string id = StoringId();
            do { } while (CalcMenu(id) != "exit");
            test.Test(); 
        }
        #endregion
    }
}