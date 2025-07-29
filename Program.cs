using Microsoft.Extensions.Logging;
using SmartParkingLot.Presentation.Interfaces;
using SmartParkingLot.Presentation.Views;
using Serilog;
using SmartParkingLot.Core.Services.Interfaces;
using SmartParkingLot.Core.Services;
using SmartParkingLot.Domain.Interfaces;
using SmartParkingLot.Presentation.Menus;

public class Program
{
    public static void Main(string[] args)
    {
        using ILoggerFactory loggerFactory = CreateLoggerFactory();

        var logger = loggerFactory.CreateLogger<Program>();

        logger.LogInformation("Application started.");

        IRateProvider rateProvider = new RateProvider(loggerFactory.CreateLogger<RateProvider>());
        IFeeCalculator feeCalculator = new FeeCalculator(rateProvider);
        IParkingLotManager parkingLotManager = new ParkingLotManager(10, loggerFactory.CreateLogger<ParkingLotManager>(), feeCalculator);
        IMainMenuView mainMenuView = new MainMenuView(parkingLotManager, loggerFactory.CreateLogger<MainMenuView>());
        IVehicleQueryMenu vehicleQueryMenu = new VehicleQueryMenu(new VehicleQueryView(parkingLotManager, loggerFactory.CreateLogger<VehicleQueryView>()), loggerFactory.CreateLogger<VehicleQueryMenu>());
        IMainMenu mainMenu = new MainMenu(mainMenuView, vehicleQueryMenu, loggerFactory.CreateLogger<MainMenu>());
        INotifiable consoleNotification = new ConsoleNotification(loggerFactory.CreateLogger<ConsoleNotification>());
        mainMenu.RenderMainMenu();

    }

    private static ILoggerFactory CreateLoggerFactory()
    {
        try
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/smart_parking_lot.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.ClearProviders();
            builder.AddSerilog();
        });
            return loggerFactory;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing Serilog: {ex.Message}");
            throw;
        }
    }
}