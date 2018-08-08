using ConsoleAppSWAPI.Model.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSWAPI.Model
{
    public class StarShipWrap
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }

        [JsonProperty("results")]
        public List<StarShip> Starships { get; set; }
    }
}
