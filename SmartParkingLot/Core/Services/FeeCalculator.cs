using SmartParkingLot.Core.Interfaces;
using SmartParkingLot.Domain.Models;

namespace SmartParkingLot.Core.Services
{
    /// <summary>
    /// Calculates parking fees based on vehicle type and duration of stay.
    /// Implements IFeeCalculator interface.
    /// </summary>
    class FeeCalculator : IFeeCalculator
    {
        private readonly IRateProvider rateProvider;
        public FeeCalculator(IRateProvider rateProvider)
        {
            this.rateProvider = rateProvider;
        }
        public decimal CalculateFees(Vehicle vehicle, DateTime? exitTime = null)
        {
            TimeSpan duration = (exitTime ?? DateTime.Now) - vehicle.EntryTime;
            decimal hourlyRate = rateProvider.GetHourlyRate(vehicle.Type);
            decimal fees = hourlyRate * duration.Hours;
            return fees;
        }


    }
}
