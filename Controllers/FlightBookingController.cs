using Flight_System.Interfaces;
using Flight_System.MOdels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Flight_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightBookingController : ControllerBase
    {
        private readonly IFlightBooking flightBooking;
        public FlightBookingController(IFlightBooking flightBooking)
        {
            this.flightBooking = flightBooking;
        }
        // GET: api/<FlightBookingController>
        [HttpPost("SearchFlight")]
        public async Task<ActionResult> Post(SearchFlightDto value)
        {
           var data =  await flightBooking.searchFlight(value);
            return Ok(data);
        }

        [HttpPost("bookflight")]
        public async Task<ActionResult> bookflight(BookFlightDto value)
        {
            var data = await flightBooking.bookFlight(value);
            return Ok(data);
        }

        // GET api/<FlightBookingController>/5
        [HttpGet("getBooking")]
        public async Task<ActionResult> Get()
        {
            var data = await flightBooking.getBookings();
            return Ok(data);
        }

        // POST api/<FlightBookingController>
        

        // PUT api/<FlightBookingController>/5
        [HttpPut("updateBooking/{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] UpdateFlightBookingDto value)
        {
            var data = await flightBooking.updateBooking(id, value);
            if (data.success)
            {
                return Ok(data);
            }else
            {
                return BadRequest(data);
            }
           
        }

        // DELETE api/<FlightBookingController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> delete(string id)
        {
            var data = await flightBooking.cancelBooking(id);
            if(data.success)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest(data);
            }
        }
    }
}
