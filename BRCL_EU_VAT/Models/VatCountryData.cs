using System;
using System.Collections.Generic;
using System.Text;

namespace BRCL_EU_VAT.Models
{
    public class VatCountryData : VatCountry
    {
        public List<VatPeriod> Periods { get; set; } = new List<VatPeriod>();

    }
}
