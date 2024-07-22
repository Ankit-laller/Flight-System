using Dapper;
using Flight_System.Interfaces;
using Flight_System.MOdels;
using Flight_System.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;

namespace Flight_System.Repo
{
    public class FlightBookingRepo : IFlightBooking
    {
        private readonly IConfiguration _configuration;
        public FlightBookingRepo( IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public async Task<ResponseClass> bookFlight(BookFlightDto value)
        {
            var query = @"
            INSERT INTO FlightBookingTable 
            (flightId,flightName, airlineName, arrivalTime, DepartureTime, origin, destination, 
             price, cargoweight,cargoWeightLimit, date, cargo, createdBy, bookedBy, timestamp, booked) 
            VALUES 
            (@FlightId, @FlightName, @AirlineName, @ArrivalTime, @DepartureTime, @Origin, @Destination, 
             @Price, @CargoWeight,@CargoWeightLimit, @Date,@Cargo, @CreatedBy, @BookedBy, @TimeStamp, @Booked);";
           
            string myuuidAsString = Guid.NewGuid().ToString();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("ForwarderConnectionString")))
            {
                try
                {
                    await connection.OpenAsync();
                    var row = await connection.ExecuteAsync(query, new
                    {
                        FlightId = myuuidAsString,
                        FlightName = value.flightName,
                        AirlineName = value.airlineName,
                        ArrivalTime = value.arrivalTime,
                        DepartureTime = value.DepartureTime,
                        Origin = value.origin,
                        Destination = value.destination,
                        Price = value.price,
                        CargoWeight = value.cargoWeight,
                        CargoWeightLimit = value.cargoWeightLimit,
                        CreatedBy = value.createdBy,
                        Date = value.date,
                        Cargo = value.cargo,
                        BookedBy = value.bookedBy,
                        TimeStamp= DateTime.Now,
                        Booked=true
                    });
                    if (row == 1)
                    {
                        return new ResponseClass(success: true, message: "Flight has been booked");
                    }
                }
                catch (Exception ex)
                {
                    return new ResponseClass(success: false, message: ex.ToString());
                }
            }
            return new ResponseClass(success: false, message: "Flight is not booked");

        }

        public async Task<ResponseClass> cancelBooking(string id)
        {
            var query = "update flightbookingtable set booked = 0 where flightId = @Id ";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("ForwarderConnectionString")))
            {
                var row = await connection.ExecuteAsync(query, new {Id =id});
                if(row == 1)
                {
                    return new ResponseClass(success: true, message: "Booking canceled");
                }
            }
            return new ResponseClass(success: false, message: "Booking is not canceled");
        }

        public async Task<IEnumerable<FlightBooking>> getBookings()
        {
            var query = "select * from FlightBookingTable where booked = 1 order by timestamp desc";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("ForwarderConnectionString")))
            {
                var data = await connection.QueryAsync<FlightBooking>(query);
                if (data != null)
                {
                    return data;
                }
            }
            return new List<FlightBooking>();
        }

        public async Task<IEnumerable<FlightBooking>> searchFlight(SearchFlightDto value)
        {
            var query = "select * from airlinetable where origin= @Origin and destination=@Destination and date= @Date";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("ForwarderConnectionString")))
            {
                var data = await connection.QueryAsync<FlightBooking>(query, new
                {
                    Origin= value.origin,
                    Destination= value.destination,
                    Date = value.date,
                });
                if (data != null)
                {
                    return data;
                }
            }
            return new List<FlightBooking>();
        }

        public async Task<ResponseClass> updateBooking(string id, UpdateFlightBookingDto value)
        {
            var query = @"
            UPDATE FlightBookingTable 
            SET 
            
            cargoWeight = @CargoWeight,
            cargo = @Cargo,
            timestamp=@TimeStamp
            WHERE flightId = @FlightId;";
            var selectQuery = @"select * from FlightBookingTable where flightId =@Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("ForwarderConnectionString")))
            {
                try
                {
                    var data = await connection.QueryFirstOrDefaultAsync<FlightBooking>(selectQuery, new { Id = id });
                    if (data == null)
                    {
                        return new ResponseClass(success: false, message: "flight not found", FlightBookingData: new FlightBooking());
                    }
                    var row = await connection.ExecuteAsync(query, new
                    {
                        FlightId = id,
                        CargoWeight = value.cargoWeight,
                        Cargo = value.cargo,
                        TimeStamp= DateTime.Now,
                    }

                    );
                    if (row == 1)
                    {
                        var flightdata = await connection.QueryFirstOrDefaultAsync<FlightBooking>(selectQuery, new { Id = id });
                        if (flightdata != null)
                        {
                            return new ResponseClass(success: true, message: "Booking updated!", FlightBookingData: flightdata);

                        }
                    }
                }
                catch(Exception ex)
                {
                    return new ResponseClass(success: false, message: ex.ToString(), FlightBookingData: null);

                }
            }
            return new ResponseClass(success: false, message: "flight not found", FlightBookingData: null);

        }
    }
}
