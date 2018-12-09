using System;
using Xunit;
using BRCL_EU_VAT.Services;
using BRCL_EU_VAT.Models;
using BRCL_EU_VAT.Repositories;
using System.Linq;
using System.Collections.Generic;

namespace BRCL_EU_VAT_Tests
{
    public class UnitTests
    {
        DateTime effectiveDate = DateTime.Now;
        string rateType = "standard";
        int resultCount = 1;
        VatRepository vatRepository;
        VatResolver vatResolver;
        List<VatCountryData> vatCountryDataCollection;

        public UnitTests()
        {
            vatRepository = new VatRepository(GetMockWebData());
            vatResolver = new VatResolver(vatRepository);
            vatCountryDataCollection = vatRepository.GetVatCountryCollection();
        }

        [Fact]
        public void TestCountryCount()
        {
            Assert.Equal(28, vatCountryDataCollection.Count);
        }

        [Fact]
        public void TestRatesValidCount()
        {
            Assert.Equal(6, vatRepository.RatesValid.Count);
        }

        [Fact]
        public void TestGetHighestStandardRate()
        {
            var result = vatResolver.GetHighestRate(resultCount, effectiveDate, rateType).ToList();
            Assert.Equal("Hungary", result[0].Name);
        }

        [Fact]
        public void TestGetLowestStandartRate()
        {
            var result = vatResolver.GetLowestRate(resultCount, effectiveDate, rateType).ToList();
            Assert.Equal("Luxembourg", result[0].Name);
        }

        private string GetMockWebData()
        {
            return "{'details':'http://github.com/adamcooke/vat-rates','version':null,'rates':[{'name':'Spain','code':'ES','country_code':'ES','periods':[{'effective_from':'0000-01-01','rates':{'super_reduced':4.0,'reduced':10.0,'standard':21.0}}]},{'name':'Bulgaria','code':'BG','country_code':'BG','periods':[{'effective_from':'0000-01-01','rates':{'reduced':9.0,'standard':20.0}}]},{'name':'Hungary','code':'HU','country_code':'HU','periods':[{'effective_from':'0000-01-01','rates':{'reduced1':5.0,'reduced2':18.0,'standard':27.0}}]},{'name':'Latvia','code':'LV','country_code':'LV','periods':[{'effective_from':'0000-01-01','rates':{'reduced':12.0,'standard':21.0}}]},{'name':'Poland','code':'PL','country_code':'PL','periods':[{'effective_from':'0000-01-01','rates':{'reduced1':5.0,'reduced2':8.0,'standard':23.0}}]},{'name':'United Kingdom','code':'UK','country_code':'GB','periods':[{'effective_from':'2011-01-04','rates':{'standard':20.0,'reduced':5.0}}]},{'name':'Czech Republic','code':'CZ','country_code':'CZ','periods':[{'effective_from':'0000-01-01','rates':{'reduced':15.0,'standard':21.0}}]},{'name':'Malta','code':'MT','country_code':'MT','periods':[{'effective_from':'0000-01-01','rates':{'reduced1':5.0,'reduced2':7.0,'standard':18.0}}]},{'name':'Italy','code':'IT','country_code':'IT','periods':[{'effective_from':'0000-01-01','rates':{'super_reduced':4.0,'reduced':10.0,'standard':22.0}}]},{'name':'Slovenia','code':'SI','country_code':'SI','periods':[{'effective_from':'0000-01-01','rates':{'reduced':9.5,'standard':22.0}}]},{'name':'Ireland','code':'IE','country_code':'IE','periods':[{'effective_from':'0000-01-01','rates':{'super_reduced':4.8,'reduced1':9.0,'reduced2':13.5,'standard':23.0,'parking':13.5}}]},{'name':'Sweden','code':'SE','country_code':'SE','periods':[{'effective_from':'0000-01-01','rates':{'reduced1':6.0,'reduced2':12.0,'standard':25.0}}]},{'name':'Denmark','code':'DK','country_code':'DK','periods':[{'effective_from':'0000-01-01','rates':{'standard':25.0}}]},{'name':'Finland','code':'FI','country_code':'FI','periods':[{'effective_from':'0000-01-01','rates':{'reduced1':10.0,'reduced2':14.0,'standard':24.0}}]},{'name':'Cyprus','code':'CY','country_code':'CY','periods':[{'effective_from':'0000-01-01','rates':{'reduced1':5.0,'reduced2':9.0,'standard':19.0}}]},{'name':'Luxembourg','code':'LU','country_code':'LU','periods':[{'effective_from':'2016-01-01','rates':{'super_reduced':3.0,'reduced1':8.0,'standard':17.0,'parking':13.0}},{'effective_from':'2015-01-01','rates':{'super_reduced':3.0,'reduced1':8.0,'reduced2':14.0,'standard':17.0,'parking':12.0}},{'effective_from':'0000-01-01','rates':{'super_reduced':3.0,'reduced1':6.0,'reduced2':12.0,'standard':15.0,'parking':12.0}}]},{'name':'Romania','code':'RO','country_code':'RO','periods':[{'effective_from':'2017-01-01','rates':{'reduced1':5.0,'reduced2':9.0,'standard':19.0}},{'effective_from':'2016-01-01','rates':{'reduced1':5.0,'reduced2':9.0,'standard':20.0}},{'effective_from':'0000-01-01','rates':{'reduced1':5.0,'reduced2':9.0,'standard':24.0}}]},{'name':'Estonia','code':'EE','country_code':'EE','periods':[{'effective_from':'0000-01-01','rates':{'reduced':9.0,'standard':20.0}}]},{'name':'Greece','code':'EL','country_code':'GR','periods':[{'effective_from':'2016-06-01','rates':{'reduced1':6.0,'reduced2':13.5,'standard':24.0}},{'effective_from':'2016-01-01','rates':{'reduced1':6.0,'reduced2':13.5,'standard':23.0}},{'effective_from':'0000-01-01','rates':{'reduced1':6.5,'reduced2':13.0,'standard':23.0}}]},{'name':'Lithuania','code':'LT','country_code':'LT','periods':[{'effective_from':'0000-01-01','rates':{'reduced1':5.0,'reduced2':9.0,'standard':21.0}}]},{'name':'France','code':'FR','country_code':'FR','periods':[{'effective_from':'2014-01-01','rates':{'super_reduced':2.1,'reduced1':5.5,'reduced2':10.0,'standard':20.0}},{'effective_from':'2012-01-01','rates':{'super_reduced':2.1,'reduced1':5.5,'reduced2':7.0,'standard':19.6}},{'effective_from':'0000-01-01','rates':{'super_reduced':2.1,'reduced1':5.5,'standard':19.6}}]},{'name':'Croatia','code':'HR','country_code':'HR','periods':[{'effective_from':'0000-01-01','rates':{'reduced1':5.0,'reduced2':13.0,'standard':25.0}}]},{'name':'Belgium','code':'BE','country_code':'BE','periods':[{'effective_from':'0000-01-01','rates':{'reduced1':6.0,'reduced2':12.0,'standard':21.0,'parking':12.0}}]},{'name':'Netherlands','code':'NL','country_code':'NL','periods':[{'effective_from':'2012-10-01','rates':{'reduced':6.0,'standard':21.0}},{'effective_from':'0000-01-01','rates':{'reduced':6.0,'standard':19.0}}]},{'name':'Slovakia','code':'SK','country_code':'SK','periods':[{'effective_from':'0000-01-01','rates':{'reduced':10.0,'standard':20.0}}]},{'name':'Germany','code':'DE','country_code':'DE','periods':[{'effective_from':'0000-01-01','rates':{'reduced':7.0,'standard':19.0}}]},{'name':'Portugal','code':'PT','country_code':'PT','periods':[{'effective_from':'0000-01-01','rates':{'reduced1':6.0,'reduced2':13.0,'standard':23.0,'parking':13.0}}]},{'name':'Austria','code':'AT','country_code':'AT','periods':[{'effective_from':'2016-01-01','rates':{'reduced1':10.0,'reduced2':13.0,'standard':20.0,'parking':13.0}},{'effective_from':'0000-01-01','rates':{'reduced':10.0,'standard':20.0,'parking':12.0}}]}]}";
        }
    }
}
