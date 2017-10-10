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

            logger.Trace("----- Method Add -----");


            result.Result = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                result.Result += nums[i];
            }

            var hasonServer = JsonConvert.SerializeObject(result);
            return hasonServer;

        }
        #endregion

        #region Subtract
        [HttpPost]
        [ActionName("sub")]
        public string Subtract(SubtractRequest petition)
        {
            int[] nums = petition.Numbers;
            SubtractResponse result = new SubtractResponse();

            result.Result = nums[0];
            for (int i = 0; i < nums.Length; i++)
            {
                if (i != 0)
                    result.Result -= nums[i];
            }
            var hasonServer = JsonConvert.SerializeObject(result);
            return hasonServer;
        }
        #endregion

        #region Multiply
        [HttpPost]
        [ActionName("mult")]
        public string Multiply(MultRequest petition)
        {
            int[] nums = petition.Multipliers;
            MultResponse result = new MultResponse();
            result.Result = 1;
            for (int i = 0; i < nums.Length; i++)
            {
                result.Result *= nums[i];
            }
            var hasonServer = JsonConvert.SerializeObject(result);
            return hasonServer;
        }
        #endregion

        #region Divide
        [HttpPost]
        [ActionName("div")]
        public string Divide(DivRequest petition)
        {
            int[] nums = new int[2];

            nums[0] = petition.Dividend;
            nums[1] = petition.Diviser;

            DivResponse result = new DivResponse();

            result.Quotient = nums[0] / nums[1];
            result.Remainder = nums[0] % nums[1];

            var hasonServer = JsonConvert.SerializeObject(result);
            return hasonServer;
        }
        #endregion

        #region SquareRoot
        [HttpPost]
        [ActionName("sqr")]
        public string Square(SquareRootRequest petition)
        {
            double num = petition.Number;

            SquareRootResponse result = new SquareRootResponse();

            result.Result = Math.Sqrt(num);

            var hasonServer = JsonConvert.SerializeObject(result);
            return hasonServer;
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
    }
}