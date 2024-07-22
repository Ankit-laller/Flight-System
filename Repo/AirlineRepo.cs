using Flight_System.Interfaces;
using Flight_System.MOdels;
using Flight_System.Utils;
using Microsoft.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Flight_System.Repo
{
  
public class AirlineRepo : IAirlineRepo

    {

        private readonly IConfiguration _configuration;

        public AirlineRepo(IConfiguration configuration)

        {

            _configuration = configuration;

        }

        public ResponseClass AddAirline(AirlineModal airline)

        {

            var query = "insert into airlinetable (Origin, Destination, ArrivalTime, DepartureTime, AirlineName, FlightName, Price, CargoWeightLimit, CreatedBy,date) values (@Origin, @Destination, @ArrivalTime, @DepartureTime, @AirlineName, @FlightName, @Price, @CargoWeightLimit, @CreatedBy, @Date);";

            var connectionString = _configuration.GetConnectionString("DefaultConnectionString");

            using (var connection = new SqlConnection(connectionString))

            {

                connection.Open();

                using (var command = new SqlCommand(query, connection))

                {

                    command.Parameters.AddWithValue("@Origin", airline.Origin);

                    command.Parameters.AddWithValue("@Destination", airline.Destination);

                    command.Parameters.AddWithValue("@ArrivalTime", airline.ArrivalTime);

                    command.Parameters.AddWithValue("@DepartureTime", airline.DepartureTime);

                    command.Parameters.AddWithValue("@AirlineName", airline.AirlineName);

                    command.Parameters.AddWithValue("@FlightName", airline.FlightName);

                    command.Parameters.AddWithValue("@Price", airline.Price);

                    command.Parameters.AddWithValue("@CargoWeightLimit", airline.CargoWeightLimit);

                    command.Parameters.AddWithValue("@CreatedBy", airline.CreatedBy);
                    command.Parameters.AddWithValue("@Date", airline.Date);

                    command.ExecuteNonQuery();

                }

            }

            return new ResponseClass(success: true, message: "airline added");

        }

        public ResponseClass DeleteAirline(int id)

        {

            var query = "DELETE FROM airlinetable WHERE flightId = @Id;";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString")))

            {

                connection.Open();

                using (var command = new SqlCommand(query, connection))

                {

                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)

                    {

                        return new ResponseClass(success: true, message: "Airline with ID deleted successfully.");

                    }

                    else

                    {

                        return new ResponseClass(success: false, message: "No airline found with ID.");

                    }

                }

            }

        }

        public AirlineModal GetAirlineById(int id)

        {

            var query = "SELECT * FROM airlinetable  WHERE flightId = @Id;";

            AirlineModal airline = null;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString")))

            {

                connection.Open();

                using (var command = new SqlCommand(query, connection))

                {

                    command.Parameters.AddWithValue("@Id", id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())

                    {

                        airline = new AirlineModal

                        {

                            Origin = reader["Origin"].ToString(),

                            Destination = reader["Destination"].ToString(),

                            ArrivalTime = reader["ArrivalTime"].ToString(),

                            DepartureTime = reader["DepartureTime"].ToString(),

                            AirlineName = reader["AirlineName"].ToString(),

                            FlightName = reader["FlightName"].ToString(),

                            Price = Convert.ToInt32(reader["Price"]),

                            CargoWeightLimit = Convert.ToInt32(reader["CargoWeight"]),

                            CreatedBy = reader["CreatedBy"].ToString(),
                            flightId = reader["flightId"].ToString()

                        };

                    }

                    reader.Close();

                }

            }

            return airline;

        }

        public List<AirlineModal> GetAllAirlines()

        {

            var airlines = new List<AirlineModal>();

            var query = "SELECT * FROM airlinetable;";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString")))

            {

                connection.Open();

                using (var command = new SqlCommand(query, connection))

                {

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())

                    {

                        var airline = new AirlineModal

                        {

                            Origin = reader["Origin"].ToString(),

                            Destination = reader["Destination"].ToString(),

                            ArrivalTime = reader["ArrivalTime"].ToString(),

                            DepartureTime = reader["DepartureTime"].ToString(),

                            AirlineName = reader["AirlineName"].ToString(),

                            FlightName = reader["FlightName"].ToString(),
                            Date = reader["date"].ToString(),
                            Price = Convert.ToInt32(reader["Price"]),

                            CargoWeightLimit = Convert.ToInt32(reader["CargoWeightLimit"]),
                            flightId = reader["flightId"].ToString(),
                            CreatedBy = reader["CreatedBy"].ToString()

                        };

                        airlines.Add(airline);

                    }

                    reader.Close();

                }

            }

            return airlines;

        }

        public ResponseClass UpdateAirline(int id, AirlineModalDto airline)

        {

            var query = "UPDATE airlinetable SET " +

                        "Origin = @Origin, " +

                        "Destination = @Destination, " +

                        "ArrivalTime = @ArrivalTime, " +

                        "DepartureTime = @DepartureTime, " +

                        "AirlineName = @AirlineName, " +

                        "FlightName = @FlightName, " +

                        "Price = @Price, " +

                        "CargoWeightLimit = @CargoWeight " +

                        "WHERE flightId = @Id;";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnectionString")))

            {

                connection.Open();

                using (var command = new SqlCommand(query, connection))

                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.Parameters.AddWithValue("@Origin", airline.Origin);

                    command.Parameters.AddWithValue("@Destination", airline.Destination);

                    command.Parameters.AddWithValue("@ArrivalTime", airline.ArrivalTime);

                    command.Parameters.AddWithValue("@DepartureTime", airline.DepartureTime);

                    command.Parameters.AddWithValue("@AirlineName", airline.AirlineName);

                    command.Parameters.AddWithValue("@FlightName", airline.FlightName);

                    command.Parameters.AddWithValue("@Price", airline.Price);

                    command.Parameters.AddWithValue("@CargoWeight", airline.CargoWeightLimit);


                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)

                    {

                        return new ResponseClass(success: true, message: "Airline with ID updated successfully.");

                    }

                    else

                    {

                        return new ResponseClass(success: false, message: "No airline found with ID.");

                    }

                }

            }

        }

    }

}
