using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CalculatorService;
using CalculatorService.Facilities;
using CalculatorService.Models;
using Newtonsoft.Json;
using NLog;

namespace CalculatorService.Controllers
{
    public class CalculatorController : Controller
    {
        #region getCalculator

        public ActionResult Index()
        {
            return View();
        }

        private static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Add
        [HttpPost]
        [ActionName("add")]
        public string Add(AddRequest petition)
        {
            int[] nums = petition.Added;
            AddResponse result = new AddResponse();
            string operationLine = "";

            logger.Trace("------- Add Method -------");

            try
            {
                result.Result = 0;
                for (int i = 0; i < nums.Length; i++)
                {
                    if (petition == null || petition.Added == null)
                    {
                        return Error400().ErrorMessage.ToString();
                    }

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

                logger.Trace($"{operationLine} = {result.Result}");

                if (Request.Headers.GetValues("X_Evi_Tracking_Id") != null && Request.Headers.GetValues("X_Evi_Tracking_Id").Any())
                {
                    string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First();
                    JournalService.StoreOperation(CreateOperation("Sum", $"{operationLine} = {result.Result}", DateTime.Now, key));
                }

                var hasonServer = JsonConvert.SerializeObject(result);
                return hasonServer;
            }catch(Exception)
            {
                return Error500().ErrorMessage.ToString();
            }

        }
        #endregion

        #region Subtract
        [HttpPost]
        [ActionName("sub")]
        public string Subtract(SubtractRequest petition)
        {
            int[] nums = petition.Numbers;
            SubtractResponse result = new SubtractResponse();
            string operationLine = "";

            logger.Trace("------- Subtract Method -------");

            try
            {
                if (petition == null || petition.Numbers == null)
                {
                    return Error400().ErrorMessage.ToString();
                }

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

                logger.Trace($"{operationLine} = {result.Result}");

                if (Request.Headers.GetValues("X_Evi_Tracking_Id") != null && Request.Headers.GetValues("X_Evi_Tracking_Id").Any())
                {
                    string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First();
                    JournalService.StoreOperation(CreateOperation("Subtraction", $"{operationLine} = {result.Result}", DateTime.Now, key));
                }

                var hasonServer = JsonConvert.SerializeObject(result);
                return hasonServer;
            }catch(Exception)
            {
                return Error500().ErrorMessage.ToString();
            }
        }
        #endregion

        #region Multiply
        [HttpPost]
        [ActionName("mult")]
        public string Multiply(MultRequest petition)
        {
            int[] nums = petition.Multipliers;
            MultResponse result = new MultResponse();
            string operationLine = "";

            logger.Trace("------- Multiply Method -------");

            try
            {
                if (petition == null || petition.Multipliers == null)
                {
                    return Error400().ErrorMessage.ToString();
                }

                result.Result = 1;
                for (int i = 0; i < nums.Length; i++)
                {
                    result.Result *= nums[i];
                    if (i != nums.Length - 1)
                    {
                        operationLine += $"{nums[i]} * ";
                    }
                    else
                    {
                        operationLine += $"{nums[i]}";
                    }
                }

                logger.Trace($"{operationLine} = {result.Result}");

                if (Request.Headers.GetValues("X_Evi_Tracking_Id") != null && Request.Headers.GetValues("X_Evi_Tracking_Id").Any())
                {
                    string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First();
                    JournalService.StoreOperation(CreateOperation("Multiplication", $"{operationLine} = {result.Result}", DateTime.Now, key));
                }

                var hasonServer = JsonConvert.SerializeObject(result);
                return hasonServer;
            }catch (Exception){
                return Error500().ErrorMessage.ToString();
            }
        }
        #endregion

        #region Divide
        [HttpPost]
        [ActionName("div")]
        public string Divide(DivRequest petition)
        {
            int?[] nums = new int?[2];
            string operationLine = "";

            logger.Trace("------- Division Method -------");

            try
            {
                if (petition == null || !(petition.Dividend.HasValue || petition.Diviser.HasValue))
                {
                    return Error400().ErrorMessage.ToString();
                }

                nums[0] = petition.Dividend;
                nums[1] = petition.Diviser;

                DivResponse result = new DivResponse();

                result.Quotient = (int)nums[0] / (int)nums[1];
                result.Remainder = (int)nums[0] % (int)nums[1];

                operationLine = $"{nums[0]} / {nums[1]}";

                logger.Trace($"{operationLine} = {result.Quotient} | Remainder = {result.Remainder}");

                if (Request.Headers.GetValues("X_Evi_Tracking_Id") != null && Request.Headers.GetValues("X_Evi_Tracking_Id").Any())
                {
                    string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First();
                    JournalService.StoreOperation(CreateOperation("Division", $"{operationLine} = {result.Quotient} | Remainder = {result.Remainder}", DateTime.Now, key));
                }

                var hasonServer = JsonConvert.SerializeObject(result);
                return hasonServer;
            }
            catch (Exception)
            {
                return Error500().ErrorMessage.ToString();
            }
        }
        #endregion

        #region SquareRoot
        [HttpPost]
        [ActionName("sqr")]
        public string Square(SquareRootRequest petition)
        {
            double? num = petition.Number;
            string operationLine = "";

            logger.Trace("------- SquareRoot Method -------");

            try
            {
                if (petition == null || !(petition.Number.HasValue))
                {
                    return Error400().ErrorMessage.ToString();
                }

                SquareRootResponse result = new SquareRootResponse();

                result.Result = Math.Sqrt((double)num);

                operationLine = $"v-- {num}";

                logger.Trace($"{operationLine} = {result.Result}");

                if (Request.Headers.GetValues("X_Evi_Tracking_Id") != null && Request.Headers.GetValues("X_Evi_Tracking_Id").Any())
                {
                    string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First();
                    JournalService.StoreOperation(CreateOperation("SquareRoot", $"{operationLine} = {result.Result}", DateTime.Now, key));
                }

                var hasonServer = JsonConvert.SerializeObject(result);
                return hasonServer;
            }
            catch (Exception)
            {
                return Error500().ErrorMessage.ToString();
            }
        }
        #endregion

        #region CreateOperation
        public Operations CreateOperation(string operation, string calculation, DateTime date, string key)
        {
            Operations ope = new Operations();

            ope.Operation = operation;
            ope.Calculation = calculation;
            ope.Date = date;
            ope.Key = key;

            return ope;
        }
        #endregion

        #region Journal
        [HttpGet]
        [ActionName("history")]
        public string History()
        {
            string history = "";
            try
            {
                string key = Request.Headers.GetValues("X_Evi_Tracking_Id").FirstOrDefault();
                history = JournalService.GetJournal();
                return history;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return e.Message;
            }
        }
        #endregion

        #region Clearing
        [HttpGet]
        [ActionName("historyC")]
        public string ClearHistory()
        {
            string history = "";
            try
            {
                history = JournalService.ClearJournal();
                return history;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return e.Message;
            }
        }
        #endregion

        #region Errors
        public static CommonError Error400()
        {
            CommonError error = new CommonError();

            error.ErrorCode = "BadRequest";
            error.ErrorStatus = 400;
            error.ErrorMessage = "Unable to process request: the arguments or the request are null";

            logger.Error($"{error.ErrorCode} - {error.ErrorStatus} / {error.ErrorMessage}");

            return error;
        }

        public static CommonError Error500()
        {
            CommonError error = new CommonError();

            error.ErrorCode = "InternalError";
            error.ErrorStatus = 500;
            error.ErrorMessage = "An unexpected error condition was triggered which made impossible to fulfill the request. Please try again";

            logger.Error($"{error.ErrorCode} - {error.ErrorStatus} / {error.ErrorMessage}");

            return error;
        }
        #endregion
    }
}