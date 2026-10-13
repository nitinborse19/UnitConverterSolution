using UnitConverter.API.Models;

namespace UnitConverter.API.Services;

public class ConversionService : IConversionService
{
    private readonly IEnumerable<ICategoryConverter> _converters;

    public ConversionService(IEnumerable<ICategoryConverter> converters)
    {
        _converters = converters;
    }

    public ConversionResponse Convert(ConversionRequest request)
    {
        var converter = _converters.FirstOrDefault(c =>
            c.CategoryName.Equals(request.Category, StringComparison.OrdinalIgnoreCase));

        if (converter == null)
        {
            throw new ArgumentException($"Category '{request.Category}' is not supported.");
        }

        if (!converter.CanConvert(request.FromUnit, request.ToUnit))
        {
            throw new ArgumentException($"Conversion from '{request.FromUnit}' to '{request.ToUnit}' is not supported in '{request.Category}'.");
        }

        decimal convertedValue = converter.Convert(request.FromUnit, request.ToUnit, request.Value);

        return new ConversionResponse(
            request.Category,
            request.FromUnit,
            request.ToUnit,
            request.Value,
            Math.Round(convertedValue, 6)
        );
    }
}