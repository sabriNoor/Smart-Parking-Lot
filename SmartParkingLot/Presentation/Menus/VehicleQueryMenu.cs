using Microsoft.Extensions.Logging;
using SmartParkingLot.Presentation.Enums;
using SmartParkingLot.Presentation.Interfaces;
using SmartParkingLot.Presentation.Validations;

namespace SmartParkingLot.Presentation.Menus
{
    public class VehicleQueryMenu : IVehicleQueryMenu
    {
         private readonly IVehicleQueryView _operations;
        private readonly ILogger<VehicleQueryMenu> _logger;
        public VehicleQueryMenu(IVehicleQueryView operations, ILogger<VehicleQueryMenu> logger)
        {
            _logger = logger;
            _operations = operations;
            _logger.LogInformation("VehicleQueryMenu class initialized.");
        }
        
        private void ShowQueryMenu()
        {
            Console.WriteLine("== Vehicle Query Menu ==");
            Console.WriteLine("1. Filter by Type");
            Console.WriteLine("2. Search by License Plate");
            Console.WriteLine("3. Sort by Entry Time");
            Console.WriteLine("4. Show All Vehicles");
            Console.WriteLine("5. Return to Main Menu");
            Console.Write("Please select an option: ");
        }
       
        public void RenderQueryMenu()
        {
            try
            {
                _logger.LogInformation("Rendering Vehicle Query Menu.");
                while (true)
                {
                    ShowQueryMenu();
                    var optionResult = InputValidator.ReadVehicleQueryOption("Please select an option: ");
                    if (!ValidationResult<VehicleQueryMenuOptions>.CheckValidation(optionResult, out var optionValue))
                    {
                        Console.WriteLine(optionResult.ErrorMessage);
                        continue;
                    }
                    if (optionValue == VehicleQueryMenuOptions.BackToMainMenu)
                    {
                        Console.WriteLine("Returning to main menu...");
                        return;
                    }
                    ExecuteOption(optionValue);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while rendering the main menu.");
                return;
            }


        }


        private void ExecuteOption(VehicleQueryMenuOptions option)
        {
            switch (option)
            {
                case VehicleQueryMenuOptions.AllVehicles:
                    _operations.RenderAllVehicles();
                    break;
                case VehicleQueryMenuOptions.ByType:
                    _operations.RenderVehiclesByType();
                    break;
                case VehicleQueryMenuOptions.ByLicensePlate:
                    _operations.RenderVehiclesByLicensePlate();
                    break;
                case VehicleQueryMenuOptions.ByEntryTime:
                    _operations.RenderVehiclesByEntryTime();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

}