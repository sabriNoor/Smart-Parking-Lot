using SmartParkingLot.Core.Factories;
using SmartParkingLot.Core.Validations;
using SmartParkingLot.Domain.Enums;
using SmartParkingLot.Domain.Exceptions;
using SmartParkingLot.Domain.Models;
using Microsoft.Extensions.Logging;
using SmartParkingLot.Core.Services.Interfaces;
using SmartParkingLot.Domain.Events;

namespace SmartParkingLot.Core.Services
{
    public class ParkingLotManager : IParkingLotManager
    {
        public delegate void LotFullHandler<T>(object sender, EventArgs e);
        private readonly List<Vehicle> vehicles;
        private readonly ILogger<ParkingLotManager> _logger;
        private IFeeCalculator feeCalculator;
        public event LotFullHandler<LotFullEventArgs>? LotFull;
        public int Capacity { get; set; }


        public ParkingLotManager(int capacity, ILogger<ParkingLotManager> logger, IFeeCalculator feeCalculator)
        {
            _logger = logger;
            this.feeCalculator = feeCalculator;
            vehicles = new List<Vehicle>();
            Capacity = capacity;
        }

        public bool CheckIn(string licensePlate, VehicleType vehicleType)
        {
            try
            {
                Vehicle vehicle = CreateAndValidateVehicle(licensePlate, vehicleType);
                EnsureCapacity();
                vehicles.Add(vehicle);
                bool isFull = vehicles.Count == Capacity;
                _logger.LogInformation($"Vehicle with license plate {licensePlate} checked in successfully at {vehicle.EntryTime}.");
                if (isFull)
                {
                    _logger.LogWarning("Parking lot is full.");
                    OnLotFull(new LotFullEventArgs("Parking lot is full now.", DateTime.Now));
                }
                return true;
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Check-in failed due to invalid vehicle data.");
                return false;
            }
            catch (ParkingLotException ex)
            {
                _logger.LogWarning(ex, "Check-in failed due to parking lot capacity issue.");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during check-in.");
                return false;
            }
        }

        private Vehicle CreateAndValidateVehicle(string licensePlate, VehicleType vehicleType)
        {
            Vehicle? vehicle = VehicleFactory.GetVehicle(licensePlate, vehicleType);
            if (vehicle is null)
            {
                throw new ArgumentException("Vehicle type is not supported.");
            }

            if (!VehicleValidator.TryValidate(vehicle, out List<string> errors))
            {
                string errorMessage = string.Join(", ", errors);
                throw new ArgumentException($"Invalid vehicle data: {errorMessage}");
            }

            return vehicle;
        }

        private void EnsureCapacity()
        {
            if (vehicles.Count >= Capacity)
            {
                throw new ParkingLotException("Parking lot is full.", OperationType.CheckIn);
            }
        }


        protected void OnLotFull(LotFullEventArgs e)
        {
            if (LotFull == null)
            {
                _logger.LogWarning("No subscribers for LotFull event.");
            }
            else
            {
                _logger.LogInformation($"LotFull event triggered with message: {e.Message} at {e.Timestamp}.");
                LotFull(this, e);
            }

        }

        public (bool success, decimal? fees) CheckOut(string licensePlate)
        {
            try
            {
                Vehicle vehicle = GetVehicle(licensePlate);
                decimal fees = feeCalculator.CalculateFees(vehicle);
                vehicles.Remove(vehicle);
                _logger.LogInformation($"Vehicle with license plate {licensePlate} checked out successfully at {DateTime.Now}.");
                return (true, fees);
            }
            catch (ParkingLotException ex)
            {
                _logger.LogWarning(ex, "Check-out failed due to vehicle not found.");
                return (false, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during check-out.");
                return (false, null);
            }
        }

        private Vehicle GetVehicle(string licensePlate)
        {
            Vehicle? vehicle = vehicles.FirstOrDefault(v => v.LicensePlate == licensePlate);

            return vehicle ?? throw new ParkingLotException("Vehicle not found.", OperationType.CheckOut); ;
        }

        public void DisplayParkingVehicles()
        {
            var vehiclesCount = vehicles.Count;
            if (vehiclesCount == 0)
            {
                _logger.LogInformation("Parking lot is empty.");
                Console.WriteLine("Parking lot is empty.");
            }
            else
            {
                foreach (var vehicle in vehicles)
                {
                    Console.WriteLine(vehicle);
                }
                Console.WriteLine($"Total parked vehicles: {vehiclesCount}");
                _logger.LogInformation("Current parking lot status displayed successfully.");

            }
        }

        public bool FilterAndDisplayVehicles<T>(VehicleQueryOption vehicleQueryOption, T? value = default)
        {
            try
            {
                List<Vehicle> vehiclesResults = ApplyVehicleQuery(vehicleQueryOption, value);
                return DisplayVehicles(vehicleQueryOption, vehiclesResults);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while displaying filtered vehicles.");
                return false;
            }
        }

        private List<Vehicle> ApplyVehicleQuery<T>(VehicleQueryOption vehicleQueryOption, T? value)
        {
            
            return vehicleQueryOption switch
            {
                VehicleQueryOption.ByType => value is not null && value is VehicleType vt
                    ? GetVehiclesByType(vehicles, vt)
                    : throw new ArgumentException("Value for ByType filter must be a non-null VehicleType."),
                VehicleQueryOption.ByLicensePlate => value is not null
                    ? GetVehiclesByLicensePlate(vehicles, value.ToString()!)
                    : throw new ArgumentException("Value for ByLicensePlate filter must be non-null."),
                VehicleQueryOption.ByEntryTime => GetVehiclesSortedByEntryTime(vehicles),
                VehicleQueryOption.All => vehicles,
                _ => throw new ArgumentException("Invalid filter criteria.")
            };
        }

        private static List<Vehicle> GetVehiclesByType (List<Vehicle> vehicles,VehicleType vehicleType)
        {
            return [.. vehicles.Where(v => v.Type == vehicleType)];
            
        }

        private static List<Vehicle> GetVehiclesByLicensePlate (List<Vehicle> vehicles,string licensePlate)
        {
            return [.. vehicles.Where(v => v.LicensePlate == licensePlate)];
            
        }
        private static List<Vehicle> GetVehiclesSortedByEntryTime(List<Vehicle> vehicles)
        {
            return [.. vehicles.OrderBy(v => v.EntryTime)];
        }

       
        private bool DisplayVehicles(VehicleQueryOption vehicleQueryOption, List<Vehicle> vehiclesResults)
        {
            if (vehiclesResults.Count == 0)
            {
                _logger.LogInformation($"No vehicles found for criteria: {vehicleQueryOption}.");
                Console.WriteLine($"No vehicles found for criteria: {vehicleQueryOption}.");
            }
            foreach (var vehicle in vehiclesResults)
            {
                Console.WriteLine(vehicle);
            }
            _logger.LogInformation($"Filtered vehicles displayed successfully for criteria: {vehicleQueryOption}.");
            Console.WriteLine($"Total vehicles found: {vehiclesResults.Count}");
            return true;
        }

    }
}