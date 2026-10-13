using UnitConverter.API.Models;

namespace UnitConverter.API.Services 
{
    public interface IConversionService
    {
        ConversionResponse Convert(ConversionRequest request);
    }
}

