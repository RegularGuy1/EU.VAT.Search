using System;
using System.Collections.Generic;
using BRCL_EU_VAT.Models;
using BRCL_EU_VAT.Interfaces;
using Newtonsoft.Json.Linq;

namespace BRCL_EU_VAT.Repositories
{
    /// <summary>
    /// EU Vat Country/Rate Repository
    /// </summary>
    public class VatRepository: IVatRepository
    {
        private string inputString = string.Empty;

        public List<string> RatesValid { get; } = new List<string>();
        public int TotalCountries { get; set; } = 0;

        public VatRepository(string inputString)
        {
            this.inputString = inputString;
        }

        /// <summary>
        /// Parses JSON input string into <see cref="VatCountryData"/> collection.
        /// </summary>
        /// <returns>VatCountryData <see cref="VatCountryData"/> collection</returns>
        public List<VatCountryData> GetVatCountryCollection()
        {
            List<VatCountryData> vatCountryDataCollection = new List<VatCountryData>();
            try
            {
                JObject vatObj = JObject.Parse(this.inputString);
                JArray ratesCollection = JArray.Parse(vatObj["rates"].ToString());
                this.TotalCountries = ratesCollection.Count;

                foreach (JToken rate in ratesCollection)
                {
                    VatCountryData vatCountryData = new VatCountryData();
                    vatCountryData.Name = rate["name"].ToString();
                    vatCountryData.Code = rate["code"].ToString();
                    vatCountryData.CountryCode = rate["country_code"].ToString();
                    foreach (JToken period in JArray.Parse(rate["periods"].ToString()))
                    {
                        VatPeriod vatPeriod = new VatPeriod();
                        DateTime dateTime;
                        DateTime.TryParse(period["effective_from"].ToString(), out dateTime);
                        vatPeriod.EffectiveDate = dateTime;
                        JToken rates = period["rates"];
                        foreach (var c in rates.Children())
                        {
                            //TODO: has to be a better way of referencing property name?
                            string rateLabel = c.ToString().Replace("\"", string.Empty).Split(':')[0].ToString();
                            AddValidRateType(rateLabel);
                            vatPeriod.Rates.Add(rateLabel, Convert.ToDecimal(c.First));
                        }
                        vatCountryData.Periods.Add(vatPeriod);
                    }
                    vatCountryDataCollection.Add(vatCountryData);

                }
            }
            catch(Exception exc)
            {
                throw new Exception($"VatRepository Error [{exc.ToString()}]");
            }
            return vatCountryDataCollection;
        }

        /// <summary>
        /// Adds unique rate labels to RatesValid collection
        /// </summary>
        /// <param name="rateType">Rate type (ex.standard, parking, etc...)</param>
        private void AddValidRateType(string rateType)
        {
            if (!RatesValid.Contains(rateType))
            {
                RatesValid.Add(rateType);
            }
        }

    }
}
