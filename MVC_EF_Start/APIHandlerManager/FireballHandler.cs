using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using MVC_EF_Start.Models;
using Newtonsoft.Json;

namespace MVC_EF_Start.APIHandlerManager
{
    public class FireballHandler
    {
        static string BASE_URL = "https://ssd-api.jpl.nasa.gov/scout.api";
        static string API_KEY = "2DbgzjUw3YVFzbsMRW4FYXpknpTYPrSzMH1Rf8Hz"; //Add your API key here inside ""
        HttpClient httpClient;

        public FireballHandler()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public FireballObject GetFireballObjects()
        {
            string FIREBALL_API_PATH = BASE_URL;
            string fireballRaw = "";
            FireballObject fireballData = null;
            httpClient.BaseAddress = new Uri(FIREBALL_API_PATH);

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(FIREBALL_API_PATH).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    fireballRaw = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!fireballRaw.Equals(""))
                {
                    fireballData = JsonConvert.DeserializeObject<FireballObject>(fireballRaw);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return fireballData;
        }
    }
}
    

