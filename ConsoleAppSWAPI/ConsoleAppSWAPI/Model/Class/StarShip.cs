using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSWAPI.Model.Class
{
    public class StarShip
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string MGLT { get; set; }
        public string Consumables { get; set; }
        public string ResupplyFrequency { get; set; }
        public string Url { get; set; }
    }
}
