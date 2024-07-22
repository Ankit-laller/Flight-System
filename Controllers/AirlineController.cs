using Flight_System.Interfaces;
using Flight_System.MOdels;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Flight_System.Controllers
{

[Route("api/[controller]")]
[ApiController]

    public class AirlineController : ControllerBase

    {

        private readonly IAirlineRepo _airlineRepo;

        public AirlineController(IAirlineRepo airlineRepo)

        {

            _airlineRepo = airlineRepo;

        }

        // GET: api/<AirlineController>

        [HttpGet]

        public ActionResult Get2()

        {

            //return new string[] { "value1", "value2" };

            var data = _airlineRepo.GetAllAirlines();

            return Ok(data);

        }

        // GET api/<AirlineController>/5

        [HttpGet("{id}")]

        public ActionResult Get(int id)

        {

            var data = _airlineRepo.GetAirlineById(id);

            return Ok(data);

        }

        // POST api/<AirlineController>

        [HttpPost]

        public ActionResult Post([FromBody] AirlineModal value)

        {

            var data = _airlineRepo.AddAirline(value);

            return Ok();

        }

        // PUT api/<AirlineController>/5

        [HttpPut("{id}")]

        public ActionResult Put(int id, [FromBody] AirlineModalDto value)

        {

            var data = _airlineRepo.UpdateAirline(id,value);

            return Ok();

        }

        // DELETE api/<AirlineController>/5

        [HttpDelete("{id}")]

        public ActionResult Delete(int id)

        {

            var data = _airlineRepo.DeleteAirline(id);

            return Ok();

        }

    }

}
