using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using BRCL_EU_VAT.Models;
using BRCL_EU_VAT.Repositories;
using BRCL_EU_VAT.Services;
using System.Linq;

namespace BRCL_EU_VAT
{


    class Program
    {
        static void Main(string[] args)
        {
            //TODO: read input from screen ?
            DateTime effectiveDate = DateTime.Now;
            string rateType = "standard";
            int resultCount = 5;
            //TODO: get file loc from appsettings.json ?
            string fileName = "http://jsonvat.com/";

            try
            {
                string webData = Utilities.GetWebDataAsync(fileName).Result;
                VatRepository vatRepository = new VatRepository(webData);
                VatResolver vatResolver = new VatResolver(vatRepository);

                if (ValidateRateType(vatRepository.RatesValid, rateType) && vatRepository.TotalCountries > resultCount && vatRepository.TotalCountries > 0)
                {
                    OutputResults("Highest", rateType, resultCount, vatResolver.GetHighestRate(resultCount, effectiveDate, rateType));
                    OutputResults("Lowest", rateType, resultCount, vatResolver.GetLowestRate(resultCount, effectiveDate, rateType).Reverse());
                }
                else
                {
                    Console.WriteLine($"Input not valid");
                }
            }
            catch (Exception exc)
            {

                Console.WriteLine($"Exception: {exc.ToString()}");
            }
        }

        /// <summary>
        /// Prints results to the console
        /// </summary>
        /// <param name="resultType"> rate Highest/Lowest.</param>
        /// <param name="rateType">Rate type (ex. standard,parking, etc...).</param>
        /// <param name="resultCount">Number of results.</param>
        /// <param name="resultCollection">Collection of results to display.</param>
        private static void OutputResults(string resultType, string rateType,int resultCount, IEnumerable<VatCountryRate> resultCollection)
        {
            Console.WriteLine($"{resultType} ({resultCount}) {rateType} rates:");
            foreach (var item in resultCollection)
            {
                Console.WriteLine($"Country: {item.Name}  Rate: {item.RateValue}");
            }
            Console.WriteLine(Environment.NewLine);
        }

        /// <summary>
        /// Validates rate type input
        /// </summary>
        /// <param name="validRateTypes">Collection of valid rate types.</param>
        /// <param name="rateType">Input rate type.</param>
        /// <returns>True if input rateType is valid and is in collection of valid rate types.</returns>
        private static bool ValidateRateType(List<string> validRateTypes, string rateType)
        {
            if (!validRateTypes.Contains(rateType))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
