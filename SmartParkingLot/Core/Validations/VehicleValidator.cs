using SmartParkingLot.Domain.Models;

namespace SmartParkingLot.Core.Validations
{
    public class VehicleValidator
    {
        public static bool TryValidate(Vehicle? vehicle, out List<string> errors)
        {
            errors = new List<string>();

            if (vehicle is null)
            {
                errors.Add("Vehicle cannot be null.");
                return false;
            }

            string license = vehicle.LicensePlate?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(license))
            {
                errors.Add("License plate cannot be empty.");
            }
            else if (license.Length < 3 || license.Length > 10)
            {
                errors.Add("License plate must be between 3 and 10 characters.");
            }

            if (vehicle.EntryTime > DateTime.Now)
            {
                errors.Add("Entry time cannot be in the future.");
            }

            return errors.Count == 0;
        }
    }
}
