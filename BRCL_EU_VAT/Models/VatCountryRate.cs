using System;
using System.Collections.Generic;
using System.Text;

namespace BRCL_EU_VAT.Models
{
    public class VatCountryRate :VatCountry
    {
        public string RateType { get; set; }
        public decimal RateValue { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
