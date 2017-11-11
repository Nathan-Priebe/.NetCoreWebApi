using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreWebAPI.Entities;
using NetCoreWebAPI.Models;
using City = NetCoreWebAPI.Models.City;

namespace NetCoreWebAPI
{
    public static class CityInforContextExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {
            if (context.Cities.Any())
            {
                return;
            }

            // init dummy data
            var cities = new List<Entities.City>()
                {
                new Entities.City()
                {
                     Name = "New York City",
                     Desc = "The one with that big park.",
                     PointsOfInterest = new List<PointOfInterest>()
                     {
                         new PointOfInterest() {
                             Name = "Central Park",
                             Desc = "The most visited urban park in the United States." },
                          new PointOfInterest() {
                             Name = "Empire State Building",
                             Desc = "A 102-story skyscraper located in Midtown Manhattan." },
                     }
                },
                new Entities.City()
                {
                    Name = "Antwerp",
                    Desc = "The one with the cathedral that was never really finished.",
                    PointsOfInterest = new List<PointOfInterest>()
                     {
                         new PointOfInterest() {
                             Name = "Cathedral of Our Lady",
                             Desc = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans." },
                          new PointOfInterest() {
                             Name = "Antwerp Central Station",
                             Desc = "The the finest example of railway architecture in Belgium." },
                     }
                },
                new Entities.City()
                {
                    Name = "Paris",
                    Desc = "The one with that big tower.",
                    PointsOfInterest = new List<PointOfInterest>()
                     {
                         new PointOfInterest() {
                             Name = "Eiffel Tower",
                             Desc = "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel." },
                          new PointOfInterest() {
                             Name = "The Louvre",
                             Desc = "The world's largest museum." },
                     }
                }
            };

            context.Cities.AddRange(cities);
            context.SaveChanges();
        }
    }
}
