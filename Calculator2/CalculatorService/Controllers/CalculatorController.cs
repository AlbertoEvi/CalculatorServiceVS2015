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
            AddResponse result = new AddResponse();

            logger.Debug("------- Add Method -------");

            try
            {
                if (petition == null || petition.Added == null)
                {
                    return Error400().ErrorMessage.ToString();
                }

                logger.Debug(Operations.Add(petition, result));

                string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First();
                JournalService.Storing(Operations.Add(petition, result), "Sum", key);

                var hasonServer = JsonConvert.SerializeObject(result);
                return hasonServer;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Error500(ex));
            }

        }
        #endregion

        #region Subtract
        [HttpPost]
        [ActionName("sub")]
        public string Subtract(SubtractRequest petition)
        {
            SubtractResponse result = new SubtractResponse();

            logger.Debug("------- Subtract Method -------");

            try
            {
                if (petition == null || petition.Numbers == null)
                {
                    return Error400().ErrorMessage.ToString();
                }

                logger.Debug(Operations.Subt(petition,result));

                string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First();
                JournalService.Storing(Operations.Subt(petition, result), "Subtraction", key);

                var hasonServer = JsonConvert.SerializeObject(result);
                return hasonServer;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Error500(ex));
            }
        }
        #endregion
        
        #region Multiply
        [HttpPost]
        [ActionName("mult")]
        public string Multiply(MultRequest petition)
        {
            MultResponse result = new MultResponse();

            logger.Debug("------- Multiply Method -------");

            try
            {
                if (petition == null || petition.Multipliers == null)
                {
                    return Error400().ErrorMessage.ToString();
                }

                logger.Debug(Operations.Mult(petition, result));

                string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First();
                JournalService.Storing(Operations.Mult(petition, result), "Multiplication", key);

                var hasonServer = JsonConvert.SerializeObject(result);
                return hasonServer;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Error500(ex));
            }
        }
        #endregion

        #region Divide
        [HttpPost]
        [ActionName("div")]
        public string Divide(DivRequest petition)
        {
            logger.Debug("------- Division Method -------");

            try
            {
                if (petition == null || petition.Numbers == null)
                {
                    return Error400().ErrorMessage.ToString();
                }
                DivResponse result = new DivResponse();

                logger.Debug(Operations.Div(petition, result));

                string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First();
                JournalService.Storing(Operations.Div(petition, result), "Division", key);

                var hasonServer = JsonConvert.SerializeObject(result);
                return hasonServer;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Error500(ex));
            }
        }
        #endregion

        #region SquareRoot
        [HttpPost]
        [ActionName("sqr")]
        public string Square(SquareRootRequest petition)
        {
            logger.Debug("------- SquareRoot Method -------");

            try
            {
                if (petition == null || !(petition.Number.HasValue))
                {
                    return Error400().ErrorMessage.ToString();
                }

                SquareRootResponse result = new SquareRootResponse();

                logger.Debug(Operations.Sqr(petition, result));

                string key = Request.Headers.GetValues("X_Evi_Tracking_Id").First();
                JournalService.Storing(Operations.Sqr(petition, result),"SquareRoot", key);

                var hasonServer = JsonConvert.SerializeObject(result);
                return hasonServer;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(Error500(ex));
            }
        }
        #endregion
        
        #region Journal
        [HttpGet]
        [ActionName("history")]
        public string History()
        {
            try
            {
                return JournalService.GetJournal();
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
            try
            {
               return JournalService.ClearJournal();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return JsonConvert.SerializeObject(Error500(ex));
            }
        }
        #endregion

        #region Errors
        public static ErrorDto Error400()
        {
            ErrorDto error = new ErrorDto("BadRequest", 400, "Unable to process request: the arguments or the request are null.");

            logger.Error($"{error.ErrorCode} - {error.ErrorStatus} / {error.ErrorMessage}");

            return error;
        }

        public static ErrorDto Error500(Exception ex)
        {
            ErrorDto error = new ErrorDto("InternalError", ex.HResult, ex.Message);

            logger.Error(ex);

            return error;
        }
        #endregion
    }
}