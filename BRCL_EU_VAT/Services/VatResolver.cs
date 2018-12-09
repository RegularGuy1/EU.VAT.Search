using System;
using System.Collections.Generic;
using System.Text;
using BRCL_EU_VAT.Models;
using BRCL_EU_VAT.Repositories;
using BRCL_EU_VAT.Interfaces;
using System.Linq;

namespace BRCL_EU_VAT.Services
{
    /// <summary>
    /// Contains methods for dealing with VatRepository Data.
    /// </summary>
    public class VatResolver: IVatResolver
    {
        private List<VatCountryData> vatCountryDataCollection;
        private VatRepository vatRepository;
        private List<VatCountryRate> effectiveCountryRates = new List<VatCountryRate>();

        public VatResolver(VatRepository vatRepository)
        {
            this.vatRepository = vatRepository;
            this.vatCountryDataCollection = vatRepository.GetVatCountryCollection();
        }

        /// <summary>
        /// Retrieves the N number of lowest countries/rates
        /// </summary>
        /// <param name="count">Result count</param>
        /// <param name="inputDate">Input date of rate search</param>
        /// <param name="rateType">Rate type (ex.standard, parking, etc...)</param>
        /// <returns>Collection of results <see cref="VatCountryRate"/></returns>
        public IEnumerable<VatCountryRate> GetLowestRate(int count, DateTime inputDate, string rateType)
        {
                CreateRateTypeList(inputDate, rateType);
                return effectiveCountryRates.TakeLast(count);
        }

        /// <summary>
        /// Retrieves the N number of highest countries/rates
        /// </summary>
        /// <param name="count">Result count</param>
        /// <param name="inputDate">Input date of rate search</param>
        /// <param name="rateType">Rate type (ex.standard, parking, etc...)</param>
        /// <returns>Collection of results <see cref="VatCountryRate"/></returns>
        public IEnumerable<VatCountryRate> GetHighestRate(int count, DateTime inputDate, string rateType)
        {
                CreateRateTypeList(inputDate, rateType);
                return effectiveCountryRates.Take(count);
        }

        /// <summary>
        /// Creates a list of countries/rates for the rateType filtered by lookupDate date
        /// Sorts the result collection by rate value using custom sorter <see cref="RateSorter"/>.
        /// </summary>
        /// <param name="inputDate">Datetime of look-up-date.Must be above the period's effective date</param>
        /// <param name="rateType">Rate type (ex.standard, parking, etc...)</param>
        private void CreateRateTypeList(DateTime inputDate, string rateType)
        {
            try
            {
                bool rateFound = false;

                if (effectiveCountryRates.Count < 1)
                {
                    foreach (var country in vatCountryDataCollection)
                    {
                        rateFound = false;
                        foreach (var period in country.Periods)
                        {
                            if (period.EffectiveDate <= inputDate)
                            {
                                foreach (var rate in period.Rates)
                                {
                                    if (rate.Key.Equals(rateType))
                                    {
                                        this.effectiveCountryRates.Add(new VatCountryRate() { EffectiveDate = period.EffectiveDate, RateType = rate.Key, RateValue = rate.Value, Code = country.Code, CountryCode = country.CountryCode, Name = country.Name });
                                        rateFound = true;
                                        break;
                                    }
                                }
                            }

                            if (rateFound)
                            { break; }
                        }
                    }

                    this.effectiveCountryRates.Sort(new RateSorter());
                }
            }
            catch (Exception exc)
            {
                throw new Exception($"VatResolver Error [{exc.ToString()}]");
            }
        }
    }
}
