namespace UnitConverter.API.Services
{
    public interface ICategoryConverter
    {
        string CategoryName { get; }
        bool CanConvert(string fromUnit, string toUnit);
        decimal Convert(string fromUnit, string toUnit, decimal value);
    }
}
