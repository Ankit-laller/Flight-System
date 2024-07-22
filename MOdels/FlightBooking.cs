namespace Flight_System.MOdels
{
    public class FlightBooking
    {
        public string flightId { get; set; }
        public string flightName { get; set;}
        public string airlineName { get; set; }
        public string arrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public int price { get; set; }
        public int cargoWeight { get; set; }
        public int cargoWeightLimit { get; set; }
        public string createdBy {  get; set; }
        public string cargo { get; set; }

        public string date { get; set; }
        public string  bookedBy { get; set; }
    }

    public class BookFlightDto
    {
        public string flightName { get; set; }
        public string airlineName { get; set; }
        public string arrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public int price { get; set; }
        public string  date { get; set; }
        public int cargoWeight { get; set; }
        public int cargoWeightLimit { get; set; }

        public string  cargo { get; set; }
        public string createdBy { get; set; }
        public string bookedBy { get; set; }


    }
    public class UpdateFlightBookingDto
    {
        public int cargoWeight { get; set; }

        public string cargo { get; set; }
    }


    public class SearchFlightDto
    {
        public string origin { get; set; }
        public string destination { get; set; }
        public string date { get; set; }
    }

    public class ResponseClass
    {
        public bool success { get; set; }
        public string message { get; set; }
        public FlightBooking FlightBookingData { get; set; }
        public ResponseClass(FlightBooking FlightBookingData, bool success, string message)
        {
            this.success = success;
            this.message = message;
            this.FlightBookingData = FlightBookingData;
        }
        public ResponseClass(bool success, string message)
        {
            this.success = success;
            this.message = message;
        }
    }

}
