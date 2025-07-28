using SmartParkingLot.Core.Factories;
using SmartParkingLot.Core.Validations;
using SmartParkingLot.Domain.Enums;
using SmartParkingLot.Domain.Exceptions;
using SmartParkingLot.Domain.Interfaces;
using SmartParkingLot.Domain.Models;
using Microsoft.Extensions.Logging;

namespace SmartParkingLot.Core.Services
{
    public class ParkingLotManager : IParkingLotManager
    {
        private readonly List<Vehicle> vehicles;
        private readonly ILogger<ParkingLotManager> _logger;
        public int Capacity { get; set; }

        public ParkingLotManager(int capacity, ILogger<ParkingLotManager> logger)
        {
            _logger = logger;
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

    }
}