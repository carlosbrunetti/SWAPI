using ConsoleAppSWAPI.Model;
using ConsoleAppSWAPI.Model.Class;
using ConsoleAppSWAPI.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ConsoleAppSWAPI.Model.DTO;
using Newtonsoft.Json.Linq;

namespace ConsoleAppSWAPI
{
    class Program
    {
        //private const string ADDRESS = "http://swapi.co/api/starships/";

        static void Main(string[] args)
        {
            do
            {

                Console.Clear();
                Console.Write("\nPlease input distance in MGLT: ");

                StarShipController starshipController = new StarShipController();

                IList<StarShip> starships = starshipController.getStarship().Result;
                starshipController.result(starships.OrderBy(x => x.Name).ToList());

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
