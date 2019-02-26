using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WorldBankGDPReport.CommonException;
using WorldBankGDPReport.Model;

namespace WorldBankGDPReport.Tests
{
    [TestClass()]
    public class CountriesListWithGDPTests
    {
        [TestMethod()]
        public void ParseResponseRecievedfromAPITestHappyFlow()
        {
            CountriesListWithGDP countriesListWithGDP = new CountriesListWithGDP();
            List<WorldBankAPIResponse> sortedListOfCountriesWithGDPValue = new List<WorldBankAPIResponse>();           
            string Json = System.IO.File.ReadAllText(Path.Combine(GetResourcePath(), "Resources", "data_2017.json"));
            sortedListOfCountriesWithGDPValue = countriesListWithGDP.ParseResponseRecievedfromAPI(sortedListOfCountriesWithGDPValue, Json);
            sortedListOfCountriesWithGDPValue.Count();
            string TopCountry = sortedListOfCountriesWithGDPValue[0].Country;
            decimal TopGDPValue = sortedListOfCountriesWithGDPValue[0].GDPValue;
            Assert.IsTrue(sortedListOfCountriesWithGDPValue.Count() > 0);
        }

        [TestMethod()]       
        public void ParseResponseRecievedfromAPITestNegativeFLow()
        {            
            try
            {
                CountriesListWithGDP countriesListWithGDP = new CountriesListWithGDP();
                List<WorldBankAPIResponse> sortedListOfCountriesWithGDPValue = new List<WorldBankAPIResponse>();
                string Json = System.IO.File.ReadAllText(Path.Combine(GetResourcePath(), "Resources", "no_data_2019.json"));
                sortedListOfCountriesWithGDPValue = countriesListWithGDP.ParseResponseRecievedfromAPI(sortedListOfCountriesWithGDPValue, Json);
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is WorldBankAPIException);
                Assert.IsTrue(ex.Message == "No Data Avaialble");
            }
        }

        [TestMethod()]       
        public void ParseResponseRecievedfromAPITestNegativeFLowErrorScenario()
        {
            try
            {
                CountriesListWithGDP countriesListWithGDP = new CountriesListWithGDP();
                List<WorldBankAPIResponse> sortedListOfCountriesWithGDPValue = new List<WorldBankAPIResponse>();
                string Json = System.IO.File.ReadAllText(Path.Combine(GetResourcePath(), "Resources", "data_error.json"));                
                sortedListOfCountriesWithGDPValue = countriesListWithGDP.ParseResponseRecievedfromAPI(sortedListOfCountriesWithGDPValue, Json);
                Assert.Fail("no exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is WorldBankAPIException);
                Assert.IsTrue(ex.Message == "The provided parameter value is not valid");
            }
        }

        private String GetResourcePath()
        {
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            return basePath.Substring(0, basePath.Length - 10);
        }
    }
}