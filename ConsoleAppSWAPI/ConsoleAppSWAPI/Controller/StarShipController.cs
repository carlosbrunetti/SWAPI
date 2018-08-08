using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConsoleAppSWAPI.Model;
using ConsoleAppSWAPI.Model.Class;
using ConsoleAppSWAPI.Model.DTO;
using Newtonsoft.Json;

namespace ConsoleAppSWAPI.Controller
{
    public class StarShipController
    {
        private const string ADDRESS = "http://swapi.co/api/starships/";


        public void result(IList<StarShip> result)
        {
            if (result != null && result.Count > 0)
            {
                Console.WriteLine(" ");
                Console.WriteLine($"{"Name".PadRight(30)} | \tNecessary stops");
                Console.WriteLine(" ");
                foreach (var starship in result)
                {
                    Console.WriteLine($"{starship.Name.PadRight(30)} | { starship.ResupplyFrequency}");
                }
            }

            Console.WriteLine(" ");
            Console.WriteLine("Press any key to use the application again or 'ESC' to exit.");
        }

        //TODO fix asynchronous function because exists more than one page and it's getting only the first 10 registers
        public async Task<List<StarShip>> getStarship(Action<StarShipWrap> callBack = null)
        {
            List<StarShip> list = new List<StarShip>();
            bool isValidInput = false;
            try
            {
                MgltDTO mgltDTO = new MgltDTO();
                mgltDTO.Mglt = Console.ReadLine();

                do
                {

                    if (!InputValidate.mgltTypeValidate(mgltDTO.Mglt))
                    {
                        Console.Write("\nAn invalid entry has inputted, please input a valid positive number: ");
                        mgltDTO.Mglt = Console.ReadLine();
                    }
                    else
                    {
                        isValidInput = true;
                    }

                } while (!isValidInput);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ADDRESS);
                    var nextURL = client.BaseAddress.ToString();

                    do
                    {
                        using (var request = await client.GetAsync(nextURL))
                        {
                            request.EnsureSuccessStatusCode();
                            string asyncResponse = await request.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<StarShipWrap>(asyncResponse);

                            if (result != null && result.Starships.Any())
                            {
                                //get all the resupply frequency acordding to starship
                                foreach (StarShip starship in result.Starships)
                                {
                                    starship.ResupplyFrequency = calculateSupply(starship, mgltDTO.Mglt);
                                    list.Add(starship);
                                }
                            }

                            //Is there next page? If it exists, get the next url 
                            nextURL = result.Next ?? string.Empty;

                        }
                    } while (!string.IsNullOrEmpty(nextURL));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
            }

            return list;
        }

        public string calculateSupply(StarShip starship, string mglt)
        {
            if (starship.MGLT.ToLower().Equals("unknown"))
            {
                return starship.MGLT;
            }

            DateTime startTime = DateTime.Now;
            DateTime EndTime = DateTime.Now;

            string[] measurement = starship.Consumables.Split(' ');

            //Verify how long it takes to arrive at the final destiny in hours
            if (measurement[1].ToLower().Equals("day") || measurement[1].ToLower().Equals("days"))
            {
                EndTime = EndTime.AddDays(Convert.ToDouble(measurement[0]));
            }
            else if (measurement[1].ToLower().Equals("week") || measurement[1].ToLower().Equals("weeks"))
            {
                EndTime = EndTime.AddDays(Convert.ToDouble(measurement[0]) * 7);
            }
            else if (measurement[1].ToLower().Equals("month") || measurement[1].ToLower().Equals("months"))
            {
                EndTime = EndTime.AddMonths(Convert.ToInt32(measurement[0]));
            }
            else if (measurement[1].ToLower().Equals("year") || measurement[1].ToLower().Equals("years"))
            {
                EndTime = EndTime.AddYears(Convert.ToInt32(measurement[0]));
            }

            int numHours = (int)(EndTime - startTime).TotalHours;
            //Distance(MGLT) / (starship MGLT * starshps consumables per hour)
            return (Convert.ToInt64(mglt) / (numHours * Convert.ToInt64(starship.MGLT))).ToString();
        }

    }
}
