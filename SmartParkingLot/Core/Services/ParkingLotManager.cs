using SmartParkingLot.Core.Factories;
using SmartParkingLot.Core.Validations;
using SmartParkingLot.Domain.Enums;
using SmartParkingLot.Domain.Exceptions;
using SmartParkingLot.Domain.Models;
using Microsoft.Extensions.Logging;
using SmartParkingLot.Core.Services.Interfaces;

namespace SmartParkingLot.Core.Services
{
    public class ParkingLotManager : IParkingLotManager
    {
        private readonly List<Vehicle> vehicles;
        private readonly ILogger<ParkingLotManager> _logger;
        private IFeeCalculator feeCalculator;
        public int Capacity { get; set; }

        public ParkingLotManager(int capacity, ILogger<ParkingLotManager> logger,IFeeCalculator feeCalculator)
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

                _logger.LogInformation($"Vehicle with license plate {licensePlate} checked in successfully at {vehicle.EntryTime}.");
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
                return (false,null);
            }
        }

        private Vehicle GetVehicle(string licensePlate)
        {
            Vehicle? vehicle = vehicles.FirstOrDefault(v => v.LicensePlate == licensePlate);
            
            return vehicle ?? throw new ParkingLotException("Vehicle not found.", OperationType.CheckOut); ;
        }

    }
}