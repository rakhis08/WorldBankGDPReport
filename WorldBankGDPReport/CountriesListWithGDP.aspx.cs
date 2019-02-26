using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using WorldBankGDPReport.CommonException;
using WorldBankGDPReport.Model;

namespace WorldBankGDPReport
{
    public partial class CountriesListWithGDP : System.Web.UI.Page
    {
        const string COUNTRY_NAME = "country";
        const string COUNTRY_GDP_VALUE = "value";
        const string COUNTRY_ISO_CODE = "countryiso3code";
        const string SERVER_ERROR = "Internal Server Error";      

        protected void Page_Load(object sender, EventArgs e)
        {            
            try
            {
                ClearMessage();
                if (IsPostBack)
                {

                    if (ddlYear.SelectedValue != string.Empty)
                    {
                        string year = ddlYear.SelectedValue;
                        LogWriter.LogWrite("Selected year: "+ year);
                        WorldBankAPIRequest requestParams = new WorldBankAPIRequest(year);                        
                        LogWriter.LogWrite("Endpoint: " + requestParams.Endpoint);
                        GetFirstTenCountriesByGDP(requestParams.Endpoint);                       
                    }
                }
            }
            catch (WorldBankAPIException ex)
            {                
                LogWriter.LogWrite("Exception Page_Load WorldBankAPIException: " + ex.Message);
                lblMessage.Text = ex.Message;
            }
            catch (Exception ex)
            {
                LogWriter.LogWrite("Exception Page_Load: " + ex.Message);
                lblMessage.Text = SERVER_ERROR;
            }
        }

        /// <summary>
        /// Clearing Message used for displaying User defined messages.
        /// </summary>
        private void ClearMessage()
        {
            lblMessage.Text = string.Empty;
        }

        /// <summary>
        /// Getting List of Top Ten Countries based on GDP value and binding to Grid.
        /// </summary>
        /// <param name="endpoint">Passed World Bank API URL.</param>
        private void GetFirstTenCountriesByGDP(string endpoint)
        {
            List<WorldBankAPIResponse> firstTenCountriesByGDP = new List<WorldBankAPIResponse>();
            try
            {
                ClearMessage();
                gridTopTenList.DataSource = GetListOfCountriesWithGDPValue(endpoint).Take(10).ToList();
                gridTopTenList.DataBind();
            }
            catch (WorldBankAPIException ex)
            {
               LogWriter.LogWrite("Exception GetFirstTenCountriesByGDP WorldBankAPIException: " + ex.Message);
               lblMessage.Text = ex.Message;
            }
            catch (Exception ex)
            {
                LogWriter.LogWrite("Exception GetFirstTenCountriesByGDP: " + ex.Message);
            }
        }

        /// <summary>
        /// Fetching Full List of countries with GDP value from World Bank API URL.
        /// </summary>
        /// <param name="endpoint">Passed World Bank API URL.</param>
        /// <returns>World Bank Details List</returns>
        private List<WorldBankAPIResponse> GetListOfCountriesWithGDPValue(string endpoint)
        {
           
            List<WorldBankAPIResponse> sortedListOfCountriesWithGDPValue = new List<WorldBankAPIResponse>();
            try
            {
                ClearMessage();
                using (var httpClient = new HttpClient())
                {
                    var response = httpClient.GetStringAsync(new Uri(endpoint)).Result;                   
                    sortedListOfCountriesWithGDPValue = ParseResponseRecievedfromAPI(sortedListOfCountriesWithGDPValue, response);
                }
            }
            catch (WorldBankAPIException ex)
            {
                LogWriter.LogWrite("Exception GetListOfCountriesWithGDPValue WorldBankAPIException: " + ex.Message);
                lblMessage.Text = ex.Message;               
            }
            catch (Exception ex)
            {
                LogWriter.LogWrite("Exception GetFirstTenCountriesByGDP: " + ex.Message);
                lblMessage.Text = SERVER_ERROR;
            }
            return sortedListOfCountriesWithGDPValue;
        }

        /// <summary>
        /// Parsed and sorted list based on GDP Value.
        /// </summary>
        /// <param name="sortedListOfCountriesWithGDPValue"></param>
        /// <param name="response">Passing API response string for sorting as per requirement</param>
        /// <returns>Returning sorted list as per GDP.</returns>
        public List<WorldBankAPIResponse> ParseResponseRecievedfromAPI(List<WorldBankAPIResponse> sortedListOfCountriesWithGDPValue, string response)
        {
            List<WorldBankAPIResponse> listOfCountriesWithGDPValue = new List<WorldBankAPIResponse>();
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            object[] objWordBankAPIResponse = jsonSerializer.Deserialize<object[]>(response);

            Dictionary<string, object> pageDetails = new Dictionary<string, object>();
            pageDetails = (Dictionary<string, object>)objWordBankAPIResponse[0];
            if (pageDetails.ContainsKey("message")) // _In case of error received from API
            {
                Object[] messageDetails = (Object[])pageDetails["message"];
                Dictionary<string, object> detail = (Dictionary<string, object>)messageDetails[0];
                foreach (KeyValuePair<string, object> item in detail)
                {
                    if (item.Key == "value")
                    {                        
                        throw new WorldBankAPIException(Convert.ToString(item.Value));
                    }
                }
            }
            else if (pageDetails.ContainsKey("total") && Convert.ToInt32(pageDetails["total"]) == 0) // _In case of no data available from Service
            {
                throw new WorldBankAPIException("No Data Avaialble");
            }
            else
            {
                Object[] countryDetails = (Object[])objWordBankAPIResponse[1];
                for (int j = 0; j < countryDetails.Length; j++)
                {
                    WorldBankAPIResponse objLstDetails = new WorldBankAPIResponse();
                    Dictionary<string, object> detail = new Dictionary<string, object>();
                    detail = (Dictionary<string, object>)countryDetails[j];

                    if (detail.ContainsKey(COUNTRY_ISO_CODE) && !detail[COUNTRY_ISO_CODE].Equals("")) // _Not adding aggregates region                         
                    {                                                                                 //as we need only countries
                        foreach (KeyValuePair<string, object> item in detail)
                        {
                            if (item.Key == COUNTRY_NAME)
                            {
                                foreach (KeyValuePair<string, object> innerItem in (Dictionary<string, object>)item.Value)
                                {
                                    if (innerItem.Key == "value") //_Storing Country name.
                                    {
                                        objLstDetails.Country = Convert.ToString(innerItem.Value);
                                    }
                                }
                            }
                            else if (item.Key == COUNTRY_GDP_VALUE) //_Storing GDP Value.
                            {
                                objLstDetails.GDPValue = Convert.ToDecimal(item.Value);
                            }
                        }
                        listOfCountriesWithGDPValue.Add(objLstDetails);
                    }
                }

                if (listOfCountriesWithGDPValue.Count > 0)
                {                    
                    sortedListOfCountriesWithGDPValue = listOfCountriesWithGDPValue.OrderByDescending(x => x.GDPValue).ToList();
                }
                LogWriter.LogWrite("listOfCountriesWithGDPValue count: " + listOfCountriesWithGDPValue.Count);
            }

            return sortedListOfCountriesWithGDPValue;
        }
    }
}