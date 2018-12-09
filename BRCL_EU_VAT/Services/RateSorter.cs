using System;
using System.Collections.Generic;
using System.Text;
using BRCL_EU_VAT.Models;

namespace BRCL_EU_VAT.Services
{
    public class RateSorter : IComparer<VatCountryRate>
    {
        public int Compare(VatCountryRate countyRate1, VatCountryRate countryRate2)
        {
            return countryRate2.RateValue.CompareTo(countyRate1.RateValue);
        }
    }
}
