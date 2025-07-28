using SmartParkingLot.Domain.Enums;

namespace SmartParkingLot.Domain.Interfaces
{
    interface IRateProvider
    {
        decimal GetHourlyRate(VehicleType vehicleType);
    }
}