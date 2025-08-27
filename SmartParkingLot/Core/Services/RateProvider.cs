using SmartParkingLot.Domain.Enums;
using SmartParkingLot.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace SmartParkingLot.Core.Services
{
    /// <summary>
    /// Provides rate information for different vehicle types.
    /// Implements the IRateProvider interface.
    /// </summary>
    class RateProvider : IRateProvider
    {
        private readonly ILogger<RateProvider> _logger;
        private static readonly Dictionary<VehicleType, decimal> rates = new()
        {
            {VehicleType.Car,5m},
            {VehicleType.Motorcycle,3m},
            {VehicleType.Truck,8m}
        };
        public RateProvider(ILogger<RateProvider> logger)
        {
            _logger = logger;
            _logger.LogInformation("RateProvider initialized with predefined rates.");

        }
        public decimal GetHourlyRate(VehicleType vehicleType)
        {
            if (!rates.ContainsKey(vehicleType))
            {
                _logger.LogWarning($"No rate defined for vehicle type: {vehicleType}");
                throw new ArgumentException($"No rate defined for vehicle type: {vehicleType}");
            }
            return rates[vehicleType];

        }
    }
}