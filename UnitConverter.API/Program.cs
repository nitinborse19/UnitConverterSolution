using UnitConverter.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Length Category (Base Unit: Meter)
builder.Services.AddSingleton<ICategoryConverter>(_ => new LinearCategoryConverter(
    "Length",
    new Dictionary<string, decimal>
    {
        { "Meter", 1.0m },
        { "Kilometer", 1000m },
        { "Centimeter", 0.01m },
        { "Millimeter", 0.001m },
        { "Mile", 1609.344m },
        { "Yard", 0.9144m },
        { "Foot", 0.3048m },
        { "Inch", 0.0254m }
    }
));

// Register Weight/Mass Category (Base Unit: Gram)
builder.Services.AddSingleton<ICategoryConverter>(_ => new LinearCategoryConverter(
    "Weight",
    new Dictionary<string, decimal>
    {
        { "Gram", 1.0m },
        { "Kilogram", 1000m },
        { "Milligram", 0.001m },
        { "Pound", 453.59237m },
        { "Ounce", 28.34952m }
    }
));

// Register Temperature Category
builder.Services.AddSingleton<ICategoryConverter, TemperatureConverter>();

// Register Service
builder.Services.AddScoped<IConversionService, ConversionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();