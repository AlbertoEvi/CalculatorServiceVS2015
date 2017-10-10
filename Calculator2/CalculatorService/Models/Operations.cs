using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculatorService.Models
{
    public class Operations
    {
        public string Operation { get; set; }

        public string Calculation { get; set; }

        public DateTime Date { get; set; }

        public string Key { get; set; }
    }
}