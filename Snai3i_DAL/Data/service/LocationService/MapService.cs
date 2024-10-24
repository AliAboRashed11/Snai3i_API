using Newtonsoft.Json.Linq;
using Snai3i_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Snai3i_DAL.Data.service.MapService
{
    public class MapService : IMapService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MapService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CustomMap> GetLocationFromAddressAsync(string location)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("MyApplication/1.0"); // Use a meaningful User-Agent

            var encodedLocation = Uri.EscapeDataString(location);
            var url = $"https://nominatim.openstreetmap.org/search?q={encodedLocation}&format=json&limit=1";

            //// Log the URL
            //Console.WriteLine($"Requesting location from URL: {url}");

            try
            {
                var response = await client.GetStringAsync(url);
                var json = JArray.Parse(response);

                // Check if the response contains results
                if (json.Count > 0)
                {
                    var lat = json[0]["lat"]?.ToString(); // Use null conditional operator
                    var lon = json[0]["lon"]?.ToString(); // Use null conditional operator

                    if (double.TryParse(lat, out double latitude) && double.TryParse(lon, out double longitude))
                    {
                        return new CustomMap
                        {
                            latitude = latitude,
                            longitude = longitude
                        };
                    }
                    else
                    {
                        throw new Exception("Latitude or Longitude could not be parsed.");
                    }
                }
                else
                {
                    throw new Exception("Location not found.");
                }
            }
            catch (Exception ex) {

                throw new Exception(ex.Message); 
            }
        }



    }
}
