using Microsoft.Extensions.Logging;
using SmartParkingLot.Core.Services.Interfaces;
using SmartParkingLot.Domain.Enums;
using SmartParkingLot.Presentation.Interfaces;
using SmartParkingLot.Presentation.Validations;

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
            _parkingLotManager.FilterAndDisplayVehicles(VehicleQueryOption.ByType,type);
           
        }

        public void RenderVehiclesByLicensePlate()
        {
            var licensePlateResult = InputValidator.ReadString("Enter vehicle license plate: ");
            if (!ValidationResult<string>.CheckValidation(licensePlateResult, out var licensePlate))
            {
                Console.WriteLine(licensePlateResult.ErrorMessage);
                return;
            }
             _parkingLotManager.FilterAndDisplayVehicles(VehicleQueryOption.ByLicensePlate,licensePlate ?? string.Empty);

        }

        public void RenderVehiclesByEntryTime()
        {
            _parkingLotManager.FilterAndDisplayVehicles<object>(VehicleQueryOption.ByEntryTime);

        }


        public void RenderAllVehicles()
        {
            _parkingLotManager.FilterAndDisplayVehicles<object>(VehicleQueryOption.All);

        }
    }
}
    