using Flight_System.MOdels;
using Flight_System.Utils;

namespace Flight_System.Interfaces
{
    public interface IFlightBooking
    {
        Task<ResponseClass> bookFlight(BookFlightDto value);
        Task<BookedFlightResponse> getBookings(int PageNumber, int PageSize);
        Task<ResponseClass> updateBooking(string id, UpdateFlightBookingDto value);
        Task<IEnumerable<FlightBooking>> searchFlight(SearchFlightDto value);
        Task<ResponseClass> cancelBooking(string id);
        Task<int> dataSize();

    }
}
