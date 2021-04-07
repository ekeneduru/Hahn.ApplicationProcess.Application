using Hahn.ApplicationProcess.February2021.Data.DTOs;
using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.Services
{
    public class ExternalService: IExternalService
    {


        public async Task<List<CountryDTO>> GetCountryAsync(string countryName)
        {
            string endpoint = $"/rest/v2/name/{countryName}?fullText=true";
            string baseurl = "https://restcountries.eu";
            var resp = await GetAsync<List<CountryDTO>>(baseurl, endpoint);
            return resp;
        }

        private async Task<T> GetAsync<T>(string baseurl, string endpoint)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new HttpResponseMessage();
               
               
                response = client.GetAsync(endpoint).Result;


                if (response.IsSuccessStatusCode)
                {
                    string stringResp;
                    using (Stream responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        stringResp = new StreamReader(responseStream).ReadToEnd();
                    }
                    T resp = JsonConvert.DeserializeObject<T>(stringResp);
                    return resp;

                }
                else
                {
                    
                    return default(T);
                }

            }
        }
    }
}
