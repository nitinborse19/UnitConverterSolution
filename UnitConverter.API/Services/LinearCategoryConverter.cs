namespace UnitConverter.API.Services;

public class LinearCategoryConverter : ICategoryConverter
{
    public string CategoryName { get; }
    private readonly Dictionary<string, decimal> _factorsToBase;

    public LinearCategoryConverter(string categoryName, Dictionary<string, decimal> factorsToBase)
    {
        CategoryName = categoryName;
        _factorsToBase = new Dictionary<string, decimal>(factorsToBase, StringComparer.OrdinalIgnoreCase);
    }

    public bool CanConvert(string fromUnit, string toUnit) =>
        _factorsToBase.ContainsKey(fromUnit) && _factorsToBase.ContainsKey(toUnit);

    public decimal Convert(string fromUnit, string toUnit, decimal value)
    {
        if (!CanConvert(fromUnit, toUnit))
        {
            throw new ArgumentException($"Unsupported units '{fromUnit}' or '{toUnit}' in category '{CategoryName}'.");
        }

        // Convert input unit -> Base Unit -> Target unit
        decimal baseValue = value * _factorsToBase[fromUnit];
        return baseValue / _factorsToBase[toUnit];
    }
}