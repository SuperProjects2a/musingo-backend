using System.ComponentModel.DataAnnotations;

namespace musingo_backend.CustomValidation;

public class NumberLessThanAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public NumberLessThanAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        ErrorMessage = ErrorMessageString;
        var currentValue = (double?)value;
        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
        if (property is null)
            throw new ArgumentException("Property with this name not found");

        var comparisonValue = (double?)property.GetValue(validationContext.ObjectInstance);

        if (currentValue > comparisonValue)
            return new ValidationResult(ErrorMessage);

        return ValidationResult.Success;
    }
}