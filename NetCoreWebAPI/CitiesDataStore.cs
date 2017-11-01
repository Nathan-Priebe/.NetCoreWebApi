using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreWebAPI.Models;

namespace NetCoreWebAPI
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<City> Cities { get; set; }

        public CitiesDataStore()
        {
            // init dummy data
            Cities = new List<City>()
            {
                new City()
                {
                     Id = 1,
                     Name = "New York City",
                     Desc = "The one with that big park.",
                     PointsOfInterest = new List<PointsOfInterest>()
                     {
                         new PointsOfInterest() {
                             Id = 1,
                             Name = "Central Park",
                             Desc = "The most visited urban park in the United States." },
                          new PointsOfInterest() {
                             Id = 2,
                             Name = "Empire State Building",
                              Desc = "A 102-story skyscraper located in Midtown Manhattan." },
                     }
                },
                new City()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Desc = "The one with the cathedral that was never really finished.",
                    PointsOfInterest = new List<PointsOfInterest>()
                     {
                         new PointsOfInterest() {
                             Id = 3,
                             Name = "Cathedral of Our Lady",
                             Desc = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans." },
                          new PointsOfInterest() {
                             Id = 4,
                             Name = "Antwerp Central Station",
                              Desc = "The the finest example of railway architecture in Belgium." },
                     }
                },
                new City()
                {
                    Id= 3,
                    Name = "Paris",
                    Desc = "The one with that big tower.",
                    PointsOfInterest = new List<PointsOfInterest>()
                     {
                         new PointsOfInterest() {
                             Id = 5,
                             Name = "Eiffel Tower",
                             Desc = "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel." },
                          new PointsOfInterest() {
                             Id = 6,
                             Name = "The Louvre",
                             Desc = "The world's largest museum." },
                     }
                }
            };

        }
    }
}
