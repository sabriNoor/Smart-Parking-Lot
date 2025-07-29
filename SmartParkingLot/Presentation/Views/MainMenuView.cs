using Microsoft.Extensions.Logging;
using SmartParkingLot.Core.Services.Interfaces;
using SmartParkingLot.Domain.Enums;
using SmartParkingLot.Presentation.Interfaces;
using SmartParkingLot.Presentation.Validations;

namespace SmartParkingLot.Presentation.Views
{
    public class MainMenuView : IMainMenuView
    {
        private readonly IParkingLotManager _parkingLotManager;
        private ILogger<MainMenuView> _logger;
        public MainMenuView(IParkingLotManager parkingLotManager, ILogger<MainMenuView> logger)
        {
            _logger = logger;
            _logger.LogInformation("Operations class initialized.");
            _parkingLotManager = parkingLotManager;

        }
        public void RenderCheckIn()
        {
            var licensePlateResult = InputValidator.ReadString("Enter vehicle license plate: ");
            if (!ValidationResult<string>.CheckValidation(licensePlateResult, out var licensePlate))
            {
                Console.WriteLine(licensePlateResult.ErrorMessage);
                return;
            }
            var vehicleTypeResult = InputValidator.ReadVehicleType("Enter vehicle type (Car, Motorcycle, Truck): ");
            if (!ValidationResult<VehicleType>.CheckValidation(vehicleTypeResult, out var type))
            {
                Console.WriteLine(vehicleTypeResult.ErrorMessage);
                return;
            }

            _parkingLotManager.CheckIn(licensePlate ?? string.Empty, type);
        }

        public void RenderCheckOut()
        {
            var checkOutLicensePlateResult = InputValidator.ReadString("Enter vehicle license plate for check-out: ");
            if (!ValidationResult<string>.CheckValidation(checkOutLicensePlateResult, out var checkOutLicensePlate))
            {
                Console.WriteLine(checkOutLicensePlateResult.ErrorMessage);
                return;
            }
            var checkOutResult = _parkingLotManager.CheckOut(checkOutLicensePlate ?? string.Empty);
            if (checkOutResult.success)
            {
                Console.WriteLine($"Vehicle with license plate {checkOutLicensePlate} checked out successfully. Fees: {checkOutResult.fees:C}");
            }
            else
            {
                Console.WriteLine($"Check-out failed for vehicle with license plate {checkOutLicensePlate}.");
            }
        }

        public void RenderExit()
        {
            _logger.LogInformation("Exiting the application.");
            Console.WriteLine("Thank you for using the Smart Parking Lot System!");
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
        }
    }
}