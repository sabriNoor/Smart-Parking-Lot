using SmartParkingLot.Domain.Enums;
using SmartParkingLot.Domain.Events;

namespace SmartParkingLot.Core.Services.Interfaces
{
    public interface IParkingLotManager
    {
        bool CheckIn(string licensePlate, VehicleType vehicleType);
        (bool success, decimal? fees) CheckOut(string licensePlate);

        bool FilterAndDisplayVehicles<T>(VehicleQueryOption vehicleQueryOption, T? value = default);
        
        public event LotFullHandler<LotFullEventArgs>? LotFull;
        
    }
    
}
