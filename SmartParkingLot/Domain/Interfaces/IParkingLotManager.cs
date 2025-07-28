using SmartParkingLot.Domain.Enums;
using SmartParkingLot.Domain.Models;

namespace SmartParkingLot.Domain.Interfaces
{
    public interface IParkingLotManager
    {
        public bool CheckIn(string licensePlate,VehicleType vehicleType);
        
    }
    
}
