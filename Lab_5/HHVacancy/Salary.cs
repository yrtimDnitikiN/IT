using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace HHVacancy
{
    class Salary
    {
        private string currency;
        [JsonProperty("to")]
        private string to;
        [JsonProperty("from")]
        private string from;
        [JsonProperty("currency")]
        public string Currency
        {
            get { return currency; }
            set { currency = (value != null) ? value : string.Empty; }    
        }
       
        public Salary(string to, string from, string currency)
        {
            this.to = to; this.from = from;
            Currency = currency;
        }

        public decimal GetValue()
        {
            if (to != null)
                return decimal.Parse(to);
            else if (from != null)
                return decimal.Parse(from);
            else
                return (decimal.Parse(to) + decimal.Parse(from)) / 2;
        }

        public override string ToString()
        {
            return (to == null && from == null) ? null : string.Format("{0} {1}", GetValue().ToString(), Currency);
        }
    }
}
