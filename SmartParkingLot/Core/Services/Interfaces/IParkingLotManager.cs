using SmartParkingLot.Domain.Enums;

namespace SmartParkingLot.Core.Services.Interfaces
{
    public interface IParkingLotManager
    {
        bool CheckIn(string licensePlate, VehicleType vehicleType);
        (bool success, decimal? fees) CheckOut(string licensePlate);
        
    }
    
}
