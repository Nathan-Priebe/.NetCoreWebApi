using Microsoft.AspNetCore.Mvc;
using NetCoreWebAPI.Entities;

namespace NetCoreWebAPI.Controllers
{
    public class DummyController : Controller
    {
        private CityInfoContext _ctx;

        public DummyController(CityInfoContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Creates a new instance of the test database using the provided database connection string
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/testdatabase")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}