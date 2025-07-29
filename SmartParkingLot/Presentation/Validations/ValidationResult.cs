namespace SmartParkingLot.Presentation.Validations
{
    public class ValidationResult<T>
    {
        public bool IsValid { get; }
        public string? ErrorMessage { get; }
        public T? Value { get; }

        private ValidationResult(bool isValid, T? value, string? errorMessage)
        {
            IsValid = isValid;
            Value = value;
            ErrorMessage = errorMessage;
        }

        public static ValidationResult<T> Success(T value) => new(true, value, null);
        public static ValidationResult<T> Failure(string errorMessage) => new(false, default, errorMessage);

        public static bool CheckValidation(ValidationResult<T> result, out T? value)
        {
            if (!result.IsValid)
            {
                Console.WriteLine(result.ErrorMessage);
                value = default;
                return false;
            }
            value = result.Value;
            return true;
        }
    }
}