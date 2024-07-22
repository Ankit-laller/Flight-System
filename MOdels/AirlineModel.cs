namespace Flight_System.MOdels
{
    public class AirlineModal
    {
        public string flightId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public string AirlineName { get; set; }
        public string FlightName { get; set; }
        public int Price { get; set; }
        public int CargoWeightLimit { get; set; }
        public string CreatedBy { get; set; }
        public string Date {  get; set; }
    }

    public class AirlineModalDto
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public string AirlineName { get; set; }
        public string FlightName { get; set; }
        public int Price { get; set; }
        public int CargoWeightLimit { get; set; }
        public string CreatedBy { get; set; }
        public string Date { get; set; }
    }
}