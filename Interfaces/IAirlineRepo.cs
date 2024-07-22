using Flight_System.MOdels;
using Flight_System.Utils;

namespace Flight_System.Interfaces
{
    public interface IAirlineRepo
    {
        ResponseClass AddAirline(AirlineModal airline);
        ResponseClass UpdateAirline(int id, AirlineModalDto airline);
        ResponseClass DeleteAirline(int id);
        List<AirlineModal> GetAllAirlines();
        AirlineModal GetAirlineById(int id);
    }
}
