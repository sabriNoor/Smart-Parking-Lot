using SmartParkingLot.Domain.Enums;

namespace SmartParkingLot.Domain.Models
{
    class Motorcycle : Vehicle
    {
        /// <summary>
        /// Represents the type of vehicle.
        /// For Motorcycle, this will always return VehicleType.Motorcycle. 
        /// </summary>
        public override VehicleType Type => VehicleType.Motorcycle;

        public Motorcycle (string licensePlate) : base(licensePlate)
        {
        }
    }
}