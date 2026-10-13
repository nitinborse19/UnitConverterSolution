using UnitConverter.API.Models;
using UnitConverter.API.Services;
using Xunit;

namespace UnitConverter.Tests;

public class ConversionServiceTests
{
    private readonly IConversionService _service;

    public ConversionServiceTests()
    {
        var lengthConverter = new LinearCategoryConverter("Length", new Dictionary<string, decimal>
        {
            { "Meter", 1.0m },
            { "Kilometer", 1000m },
            { "Foot", 0.3048m }
        });

        var weightConverter = new LinearCategoryConverter("Weight", new Dictionary<string, decimal>
        {
            { "Gram", 1.0m },
            { "Kilogram", 1000m },
            { "Pound", 453.59237m }
        });

        var tempConverter = new TemperatureConverter();

        var converters = new List<ICategoryConverter> { lengthConverter, weightConverter, tempConverter };
        _service = new ConversionService(converters);
    }

    [Theory]
    [InlineData("Length", "Kilometer", "Meter", 1, 1000)]
    [InlineData("Length", "Meter", "Kilometer", 1000, 1)]
    [InlineData("Length", "Foot", "Meter", 1, 0.3048)]
    public void Convert_LinearLength_ReturnsExpectedValue(string category, string from, string to, decimal value, decimal expected)
    {
        var request = new ConversionRequest(category, from, to, value);
        var result = _service.Convert(request);
        Assert.Equal(expected, result.ConvertedValue);
    }

    [Theory]
    [InlineData("Temperature", "Celsius", "Fahrenheit", 0, 32)]
    [InlineData("Temperature", "Celsius", "Fahrenheit", 100, 212)]
    [InlineData("Temperature", "Fahrenheit", "Celsius", 32, 0)]
    [InlineData("Temperature", "Celsius", "Kelvin", 0, 273.15)]
    public void Convert_Temperature_ReturnsExpectedValue(string category, string from, string to, decimal value, decimal expected)
    {
        var request = new ConversionRequest(category, from, to, value);
        var result = _service.Convert(request);
        Assert.Equal(expected, result.ConvertedValue);
    }

    [Fact]
    public void Convert_UnsupportedCategory_ThrowsArgumentException()
    {
        var request = new ConversionRequest("Speed", "Mph", "Kmph", 60);
        Assert.Throws<ArgumentException>(() => _service.Convert(request));
    }

    [Fact]
    public void Convert_UnsupportedUnit_ThrowsArgumentException()
    {
        var request = new ConversionRequest("Length", "LightYear", "Meter", 1);
        Assert.Throws<ArgumentException>(() => _service.Convert(request));
    }
}