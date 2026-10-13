namespace UnitConverter.API.Services;

public class TemperatureConverter : ICategoryConverter
{
    public string CategoryName => "Temperature";

    private static readonly HashSet<string> ValidUnits = new(StringComparer.OrdinalIgnoreCase)
    {
        "Celsius",
        "Fahrenheit",
        "Kelvin"
    };

    public bool CanConvert(string fromUnit, string toUnit) =>
        ValidUnits.Contains(fromUnit) && ValidUnits.Contains(toUnit);

    public decimal Convert(string fromUnit, string toUnit, decimal value)
    {
        if (!CanConvert(fromUnit, toUnit))
        {
            throw new ArgumentException($"Unsupported temperature units: '{fromUnit}' to '{toUnit}'.");
        }

        if (fromUnit.Equals(toUnit, StringComparison.OrdinalIgnoreCase))
        {
            return value;
        }

        // Normalize to Celsius
        decimal celsius = fromUnit.ToLowerInvariant() switch
        {
            "celsius" => value,
            "fahrenheit" => (value - 32m) * 5m / 9m,
            "kelvin" => value - 273.15m,
            _ => throw new ArgumentException($"Invalid unit {fromUnit}")
        };

        // Convert Celsius to Target Unit
        return toUnit.ToLowerInvariant() switch
        {
            "celsius" => celsius,
            "fahrenheit" => (celsius * 9m / 5m) + 32m,
            "kelvin" => celsius + 273.15m,
            _ => throw new ArgumentException($"Invalid unit {toUnit}")
        };
    }
}