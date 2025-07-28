using SmartParkingLot.Domain.Enums;
using SmartParkingLot.Domain.Models;

namespace SmartParkingLot.Core.Factories
{
    public class VehicleFactory
    {
        public static Vehicle? GetVehicle(string licensePlate, VehicleType vehicleType)
        {
            Vehicle? vehicle = null;
            if (vehicleType == VehicleType.Car)
            {
                vehicle = new Car(licensePlate);
            }
            else if (vehicleType == VehicleType.Motorcycle)
            {
                vehicle = new Motorcycle(licensePlate);
            }
            else if (vehicleType == VehicleType.Truck)
            {
                vehicle = new Truck(licensePlate);

            }
            return vehicle;
            
        }
    }
}