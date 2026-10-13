using System.ComponentModel.DataAnnotations;

namespace UnitConverter.API.Models
{
    public record ConversionRequest(
    [Required] string Category,
    [Required] string FromUnit,
    [Required] string ToUnit,
    [Required] decimal Value
);
}
