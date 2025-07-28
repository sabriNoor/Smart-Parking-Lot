using SmartParkingLot.Domain.Models;

namespace SmartParkingLot.Core.Services.Interfaces
{
    public interface IFeeCalculator
    {
        decimal CalculateFees(Vehicle vehicle, DateTime? exitTime = null);
    }
}