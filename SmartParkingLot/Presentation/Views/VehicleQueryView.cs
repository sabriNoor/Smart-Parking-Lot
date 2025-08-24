using Microsoft.Extensions.Logging;
using SmartParkingLot.Domain.Enums;
using SmartParkingLot.Core.Interfaces;
using SmartParkingLot.Core.Validations;

namespace SmartParkingLot.Presentation.Views
{
    public class VehicleQueryView : IVehicleQueryView
    {
        ILogger<VehicleQueryView> _logger;
        private readonly IParkingLotManager _parkingLotManager;

        public VehicleQueryView(IParkingLotManager parkingLotManager,ILogger<VehicleQueryView> logger)
        {
            _parkingLotManager = parkingLotManager;
            _logger = logger;
            _logger.LogInformation("VehicleQueryView class initialized.");
        }

        public void RenderVehiclesByType()
        {
            var vehicleTypeResult = InputValidator.ReadVehicleType("Enter vehicle type (Car, Motorcycle, Truck): ");
            if (!ValidationResult<VehicleType>.CheckValidation(vehicleTypeResult, out var type))
            {
                Console.WriteLine(vehicleTypeResult.ErrorMessage);
                return;
            }
            var success = _parkingLotManager.FilterAndDisplayVehicles(VehicleQueryOption.ByType, type);
            if(!success)
            {
                Console.WriteLine($"No vehicles found of type {type}.");
            }
            else
            {
                Console.WriteLine($"Vehicles of type {type} displayed successfully.");
            }
           
        }

        public void RenderVehiclesByLicensePlate()
        {
            var licensePlateResult = InputValidator.ReadString("Enter vehicle license plate: ");
            if (!ValidationResult<string>.CheckValidation(licensePlateResult, out var licensePlate))
            {
                Console.WriteLine(licensePlateResult.ErrorMessage);
                return;
            }
            var success = _parkingLotManager.FilterAndDisplayVehicles(VehicleQueryOption.ByLicensePlate, licensePlate ?? string.Empty);
            if (!success)
            {
                Console.WriteLine($"No vehicles found with license plate {licensePlate}.");
            }
            else
            {
                Console.WriteLine($"Vehicles with license plate {licensePlate} displayed successfully.");
            }

        }

        public void RenderVehiclesByEntryTime()
        {
            var success = _parkingLotManager.FilterAndDisplayVehicles<object>(VehicleQueryOption.ByEntryTime);
            if (!success)
            {
                Console.WriteLine("No vehicles found to display sorted by entry time.");
            }
            else
            {
                Console.WriteLine("Vehicles sorted by entry time displayed successfully.");
            }

        }


        public void RenderAllVehicles()
        {
            var success = _parkingLotManager.FilterAndDisplayVehicles<object>(VehicleQueryOption.All);
            if (!success)
            {
                Console.WriteLine("No vehicles found to display.");
            }
            else
            {
                Console.WriteLine("All vehicles displayed successfully.");
            }

        }
    }
}
    