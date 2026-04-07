# Quantity Measurement Application

A robust, multi-layered .NET Core Web API designed for unit measurement, conversion, comparison, and arithmetic operations across various physical quantities.

## 🚀 Features

- **Unit Conversion**: Seamlessly convert between different units within the same category (Length, Volume, Weight, and Temperature).
- **Comparison**: Compare two quantities to determine equality or scale.
- **Arithmetic Operations**: Perform Addition, Subtraction, and Division on compatible units.
- **Historical Records**: Securely store and retrieve measurement history.
- **User Authentication**: JWT-based registration and login system to protect sensitive data and user-specific records.

## 🛠️ Tech Stack

- **Framework**: .NET Core (ASP.NET Core Web API)
- **Database**: Microsoft SQL Server
- **Authentication**: JSON Web Tokens (JWT)
- **Design Pattern**: Repository Pattern, Service Layer, Layered Architecture
- **Testing**: xUnit / MS Test for unit and integration testing

## 📏 Supported Units

| Category | Supported Units |
| :--- | :--- |
| **Length** | Feet, Inch, Yard, Centimeter |
| **Temperature** | Celsius, Fahrenheit |
| **Volume** | Gallon, Litre, Millilitre |
| **Weight** | Kilogram, Gram, Tonne |

## 📂 Project Structure

- **QuantityMeasurementApp.Controller**: API endpoints and request handling.
- **QuantityMeasurementApp.Service**: Core business logic and service orchestration.
- **QuantityMeasurementApp.Repository**: Data access layer and database interactions.
- **QuantityMeasurementApp.Library**: Shared models, enums, and utility classes for unit conversion.
- **QuantityMeasurementApp.Model**: Data Transfer Objects (DTOs) and Domain Models.
- **QuantityMeasurementApp.Tests**: Comprehensive test suite for validating conversion logic and API behavior.

## 🚦 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (Version 6.0 or higher recommended)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1.  **Clone the repository**:
    ```bash
    git clone https://github.com/shyamrai22/QuantityMeasurementApp.git
    cd QuantityMeasurementApp
    ```

2.  **Configure Database**:
    Update the connection string in `QuantityMeasurementApp.Controller/appsettings.json` to match your local SQL Server instance.

3.  **Restore Dependencies**:
    ```bash
    dotnet restore
    ```

4.  **Run the Application**:
    ```bash
    dotnet run --project QuantityMeasurementApp.Controller
    ```

## 🔌 API Endpoints

### Authentication
- `POST /api/auth/register` - Create a new user account.
- `POST /api/auth/login` - Authenticate and receive a JWT token.

### Quantity Operations
- `POST /api/quantity/compare` - Compare two quantities.
- `POST /api/quantity/convert` - Convert a quantity to a target unit.
- `POST /api/quantity/add` - Add two quantities.
- `POST /api/quantity/subtract` - Subtract one quantity from another.
- `POST /api/quantity/divide` - Divide one quantity by another.
- `GET /api/quantity/records` - Retrieve measurement history (Requires Auth).

## 🧪 Running Tests

To execute the test suite, run the following command from the root directory:
```bash
dotnet test
```
