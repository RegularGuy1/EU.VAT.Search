using System;
using System.Collections.Generic;
using System.Text;
using BRCL_EU_VAT.Models;

namespace BRCL_EU_VAT.Interfaces
{
    public interface IVatResolver
    {
        IEnumerable<VatCountryRate> GetLowestRate(int count, DateTime effectiveDate, string rateType);
        IEnumerable<VatCountryRate> GetHighestRate(int count, DateTime effectiveDate, string rateType);
    }
}
