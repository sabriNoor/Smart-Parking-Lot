using Microsoft.Extensions.Logging;
using SmartParkingLot.Domain.Enums;
using SmartParkingLot.Core.Interfaces;
using SmartParkingLot.Core.Validations;

namespace SmartParkingLot.Presentation.Menus
{
    public class MainMenu : IMainMenu
    {
        private readonly IMainMenuView _mainMenuView;
        private IVehicleQueryMenu _vehicleQueryMenu;
        private readonly ILogger<MainMenu> _logger;
        private void ShowMainMenu()
        {
            Console.WriteLine("== Smart Parking Lot System ==");
            Console.WriteLine("1. Check-In Vehicle");
            Console.WriteLine("2. Check-Out Vehicle");
            Console.WriteLine("3. View Vehicles");
            Console.WriteLine("4. Exit");
        }

        public MainMenu(IMainMenuView operations,IVehicleQueryMenu vehicleQueryMenu ,ILogger<MainMenu> logger)
        {
            _logger = logger;
            _mainMenuView = operations;
            _vehicleQueryMenu = vehicleQueryMenu;
            _logger.LogInformation("MainMenu class initialized.");
        }

        public void RenderMainMenu()
        {
            try
            {
                while (true)
                {
                    ShowMainMenu();
                    var optionResult = InputValidator.ReadMainMenuOption("Please select an option: ");
                    if(!ValidationResult<MainMenuOptions>.CheckValidation(optionResult, out var optionValue))
                    {
                        Console.WriteLine(optionResult.ErrorMessage);
                        continue;
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

        private void ExecuteOption(MainMenuOptions option)
        {
            switch (option)
            {
                case MainMenuOptions.CheckIn:
                    _mainMenuView.RenderCheckIn();
                    break;
                case MainMenuOptions.CheckOut:
                    _mainMenuView.RenderCheckOut();
                    break;
                case MainMenuOptions.ViewVehicles:
                    _vehicleQueryMenu.RenderQueryMenu();
                    break;
                case MainMenuOptions.Exit:
                    _mainMenuView.RenderExit();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}