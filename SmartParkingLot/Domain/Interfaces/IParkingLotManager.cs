using SmartParkingLot.Domain.Enums;
using SmartParkingLot.Domain.Models;

namespace SmartParkingLot.Domain.Interfaces
{
    public interface IParkingLotManager
    {
        bool CheckIn(string licensePlate, VehicleType vehicleType);
        bool CheckOut(string licensePlate);
        
    }
    
}
