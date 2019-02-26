using System;
using System.Configuration;

namespace WorldBankGDPReport.Model
{
    public class WorldBankAPIRequest
    {

        public int RecordsPerPage { get; set; }
        public string Endpoint { get; set; }

        public WorldBankAPIRequest(string _Year)
        {
            //string URL = "http://api.worldbank.org/v2/country/all/indicator/NY.GDP.MKTP.CD?date=2017&format=json&per_page=300";
            RecordsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["RecordsPerPage"]);
            Endpoint = ConfigurationManager.AppSettings["APIUrl"] + "?date=" + _Year + "&format=json&per_page=" + RecordsPerPage;
        }
         
    }
}