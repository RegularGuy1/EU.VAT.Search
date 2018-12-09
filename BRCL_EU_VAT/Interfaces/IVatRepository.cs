using System;
using System.Collections.Generic;
using BRCL_EU_VAT.Models;

namespace BRCL_EU_VAT.Interfaces
{
    public interface IVatRepository
    {
        List<VatCountryData> GetVatCountryCollection();
    }
}
