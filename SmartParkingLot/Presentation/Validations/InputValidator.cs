using SmartParkingLot.Domain.Enums;
using SmartParkingLot.Presentation.Enums;

namespace SmartParkingLot.Presentation.Validations
{
    public class InputValidator
    {
        public static ValidationResult<string> ReadString(string prompt)
        {
            Console.Write($"{prompt}");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                return ValidationResult<string>.Failure("Input cannot be empty.");
            }
            return ValidationResult<string>.Success(input.Trim());
        }

        public static ValidationResult<VehicleType> ReadVehicleType(string prompt)
        {
            var vehicleTypeInput = ReadString(prompt);
            if (!ValidationResult<string>.CheckValidation(vehicleTypeInput, out var vehicleTypeInputValue))
            {
                return ValidationResult<VehicleType>.Failure(vehicleTypeInput.ErrorMessage ?? "Unknown error");
            }
            if (Enum.TryParse<VehicleType>(vehicleTypeInputValue, true, out var vehicleType))
            {
                return ValidationResult<VehicleType>.Success(vehicleType);
            }
            return ValidationResult<VehicleType>.Failure("Invalid vehicle type entered.");
        }

        public static ValidationResult<MainMenuOptions> ReadMainMenuOption(string prompt)
        {
            Console.Write($"{prompt}");
            if (!int.TryParse(Console.ReadLine(), out int option) || !Enum.IsDefined(typeof(MainMenuOptions), option))
            {
                return ValidationResult<MainMenuOptions>.Failure("Invalid option. Please try again.");
            }
            return ValidationResult<MainMenuOptions>.Success((MainMenuOptions)option);
        }

        public static ValidationResult<VehicleQueryMenuOptions> ReadVehicleQueryOption(string prompt)
        {
            Console.Write($"{prompt}");
            if (!int.TryParse(Console.ReadLine(), out int option) || !Enum.IsDefined(typeof(VehicleQueryMenuOptions), option))
            {
                return ValidationResult<VehicleQueryMenuOptions>.Failure("Invalid option. Please try again.");
            }
            return ValidationResult<VehicleQueryMenuOptions>.Success((VehicleQueryMenuOptions)option);
        }

        public static ValidationResult<int> ReadPositiveInt(string prompt)
        {
            Console.Write($"{prompt}");
            if (!int.TryParse(Console.ReadLine(), out int value) || value <= 0)
            {
                return ValidationResult<int>.Failure("Invalid input. Please enter a positive integer.");
            }
            return ValidationResult<int>.Success(value);
        }
       
    }
}