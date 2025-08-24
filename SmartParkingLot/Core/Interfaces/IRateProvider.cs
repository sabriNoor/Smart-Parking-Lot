using SmartParkingLot.Domain.Enums;

namespace SmartParkingLot.Core.Interfaces
{
    interface IRateProvider
    {
        decimal GetHourlyRate(VehicleType vehicleType);
    }
}