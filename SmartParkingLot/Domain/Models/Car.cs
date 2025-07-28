using SmartParkingLot.Domain.Enums;

namespace SmartParkingLot.Domain.Models
{
    public class Car : Vehicle
    {
        /// <summary>
        /// Represents the type of vehicle.
        /// For a Car, this will always return VehicleType.Car.
        /// </summary>
        public override VehicleType Type => VehicleType.Car;

        public Car(string licensePlate,DateTime? entryTime=null) : base(licensePlate,entryTime)
        {
        }
    }
}