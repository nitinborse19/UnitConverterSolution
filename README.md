# Unit Conversion API

An extensible RESTful API built with ASP.NET Core for converting numerical values between different units of measurement across multiple categories (Length, Weight/Mass, Temperature).

## Architecture & Design Decisions

1. **Base-Unit Normalization (Linear Categories):**
   - For linear conversions (e.g., Length, Weight), each unit defines a single factor relative to a chosen base unit (Meter, Gram).
   - This prevents $O(N^2)$ direct unit-to-unit mapping and scales linearly ($O(N)$). Adding a new unit only requires adding one ratio entry.
2. **Strategy Pattern:**
   - Converters implement `ICategoryConverter`. Non-linear calculations like Temperature are decoupled into their own dedicated converter.
   - Satisfies the Open/Closed Principle (OCP): new categories or data providers can be plugged in without changing core conversion logic.
3. **Extensibility for Hundreds of Units:**
   - While factors are initialized in memory for this assessment, the design allows replacing the dictionary with a database repository or configuration file without modifying any controller or business logic.

## Prerequisites

- [.NET 8 SDK or .NET 9 SDK](https://dotnet.microsoft.com/download)

## How to Run Locally

1. **Clone the repository:**
   ```bash
   git clone <your-repo-url>
   cd UnitConverterSolution
