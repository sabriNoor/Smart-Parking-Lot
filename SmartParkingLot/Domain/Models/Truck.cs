using SmartParkingLot.Domain.Enums;

namespace SmartParkingLot.Domain.Models
{
    public class Truck : Vehicle
    {
        /// <summary>
        /// Represents the type of vehicle.
        /// For Truck, this will always return VehicleType.Truck. 
        /// </summary>
        public override VehicleType Type => VehicleType.Truck;

        public Truck (string licensePlate,DateTime? entryTime=null) : base(licensePlate,entryTime)
        {
        }
    }
}