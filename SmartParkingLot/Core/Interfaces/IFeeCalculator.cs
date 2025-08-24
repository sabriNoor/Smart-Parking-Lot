using SmartParkingLot.Domain.Models;

namespace SmartParkingLot.Core.Interfaces
{
    public interface IFeeCalculator
    {
        decimal CalculateFees(Vehicle vehicle, DateTime? exitTime = null);
    }
}