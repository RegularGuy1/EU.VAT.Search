using System;
using System.Collections.Generic;
using System.Text;

namespace BRCL_EU_VAT.Models
{
    public class VatPeriod
    {
        public DateTime EffectiveDate { get; set; }

        public Dictionary<string, decimal> Rates { get; set; } = new Dictionary<string, decimal>();
    }
}
