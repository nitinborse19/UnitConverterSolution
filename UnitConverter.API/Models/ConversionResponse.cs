namespace UnitConverter.API.Models
{
    public record ConversionResponse(
    string Category,
    string FromUnit,
    string ToUnit,
    decimal OriginalValue,
    decimal ConvertedValue
);
}
