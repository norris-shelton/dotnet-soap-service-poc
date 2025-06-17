# C# SOAP Service Proof of Concept

## Overview

This project demonstrates a comprehensive C# SOAP service implementation using .NET 8 and SoapCore. The proof of concept includes two fully functional SOAP services with complete WSDL generation, a client application for testing, and detailed documentation.

## Project Structure

```
soap-service-poc/
├── SoapServicePoc/                 # Main SOAP service project
│   ├── Contracts/                  # Service contracts (interfaces)
│   │   ├── IUserService.cs         # User service contract
│   │   └── ICalculatorService.cs   # Calculator service contract
│   ├── Models/                     # Data models and DTOs
│   │   └── User.cs                 # User-related data contracts
│   ├── Services/                   # Service implementations
│   │   ├── UserService.cs          # User service implementation
│   │   └── CalculatorService.cs    # Calculator service implementation
│   ├── Program.cs                  # Application startup and configuration
│   └── SoapServicePoc.csproj       # Project file
├── SoapClient/                     # SOAP client test application
│   ├── Program.cs                  # Client implementation
│   └── SoapClient.csproj           # Client project file
└── README.md                       # This documentation
```

## Features

### SOAP Services Implemented

#### 1. User Service (`/UserService.asmx`)
A comprehensive user management service providing CRUD operations:

- **CreateUserAsync**: Create new users with validation
- **GetUserByIdAsync**: Retrieve users by ID
- **GetAllUsersAsync**: Get all users in the system
- **UpdateUserAsync**: Update existing user information
- **DeleteUserAsync**: Remove users from the system
- **GetUserByEmailAsync**: Find users by email address

#### 2. Calculator Service (`/CalculatorService.asmx`)
A mathematical operations service demonstrating different data types:

- **Add**: Simple addition of two numbers
- **Subtract**: Subtraction operation
- **Multiply**: Multiplication operation
- **Divide**: Division with error handling
- **Calculate**: Complex calculation with operation string
- **GetCalculatorInfo**: Service information and status

### Key Technical Features

- **WSDL Generation**: Automatic WSDL generation for both services
- **Data Contracts**: Proper XML serialization with DataContract attributes
- **Error Handling**: Comprehensive error handling and validation
- **Async Operations**: Support for asynchronous operations
- **Type Safety**: Strong typing with proper data models
- **Documentation**: Built-in service documentation and help pages

## Technology Stack

- **.NET 8**: Latest .NET framework
- **SoapCore**: SOAP service implementation for .NET Core/8
- **ASP.NET Core**: Web framework for hosting services
- **System.ServiceModel**: Service model primitives for SOAP
- **C#**: Primary programming language

## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or VS Code (optional)
- Any SOAP client tool (SoapUI, Postman, etc.)

### Running the Service

1. Navigate to the service directory:
   ```bash
   cd SoapServicePoc
   ```

2. Build and run the service:
   ```bash
   dotnet build
   dotnet run --urls="http://localhost:5000"
   ```

3. Access the service:
   - Homepage: http://localhost:5000
   - User Service WSDL: http://localhost:5000/UserService.asmx?wsdl
   - Calculator Service WSDL: http://localhost:5000/CalculatorService.asmx?wsdl

### Running the Test Client

1. Navigate to the client directory:
   ```bash
   cd SoapClient
   ```

2. Build and run the client:
   ```bash
   dotnet build
   dotnet run
   ```

The client will automatically test both services and display the results.

## Service Endpoints

### User Service Endpoint: `/UserService.asmx`

**WSDL**: http://localhost:5000/UserService.asmx?wsdl

**Operations**:
- `CreateUserAsync(CreateUserRequest)` → `UserResponse`
- `GetUserByIdAsync(int)` → `UserResponse`
- `GetAllUsersAsync()` → `UsersListResponse`
- `UpdateUserAsync(User)` → `UserResponse`
- `DeleteUserAsync(int)` → `UserResponse`
- `GetUserByEmailAsync(string)` → `UserResponse`

### Calculator Service Endpoint: `/CalculatorService.asmx`

**WSDL**: http://localhost:5000/CalculatorService.asmx?wsdl

**Operations**:
- `Add(double, double)` → `double`
- `Subtract(double, double)` → `double`
- `Multiply(double, double)` → `double`
- `Divide(double, double)` → `double`
- `Calculate(CalculationRequest)` → `CalculationResult`
- `GetCalculatorInfo()` → `string`

## Sample SOAP Requests

### Calculator Service - Add Operation

```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
    <soap:Body>
        <Add xmlns="http://tempuri.org/">
            <a>5</a>
            <b>3</b>
        </Add>
    </soap:Body>
</soap:Envelope>
```

### User Service - Get All Users

```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
    <soap:Body>
        <GetAllUsers xmlns="http://tempuri.org/" />
    </soap:Body>
</soap:Envelope>
```

### User Service - Create User

```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
    <soap:Body>
        <CreateUser xmlns="http://tempuri.org/">
            <request>
                <FirstName>Alice</FirstName>
                <LastName>Johnson</LastName>
                <Email>alice.johnson@example.com</Email>
            </request>
        </CreateUser>
    </soap:Body>
</soap:Envelope>
```

## Data Models

### User Model
```csharp
[DataContract]
public class User
{
    [DataMember] public int Id { get; set; }
    [DataMember] public string FirstName { get; set; }
    [DataMember] public string LastName { get; set; }
    [DataMember] public string Email { get; set; }
    [DataMember] public DateTime CreatedDate { get; set; }
    [DataMember] public bool IsActive { get; set; }
}
```

### Calculation Request Model
```csharp
[DataContract]
public class CalculationRequest
{
    [DataMember] public double FirstNumber { get; set; }
    [DataMember] public double SecondNumber { get; set; }
    [DataMember] public string Operation { get; set; }
}
```

## Testing Results

The included test client successfully demonstrates:

✅ **Calculator Service Tests**:
- Add operation: 5 + 3 = 8
- Service info retrieval
- Complex calculation: 10 × 4 = 40

✅ **User Service Tests**:
- Retrieved 3 existing users
- Successfully got user by ID
- Created new user (Alice Johnson)

All operations returned proper SOAP responses with expected data structures.

## Configuration

### Service Configuration (Program.cs)
```csharp
// Register SOAP services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICalculatorService, CalculatorService>();
builder.Services.AddSoapCore();

// Configure SOAP endpoints
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<IUserService>("/UserService.asmx", 
        new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
    endpoints.UseSoapEndpoint<ICalculatorService>("/CalculatorService.asmx", 
        new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
});
```

## Deployment Considerations

### Production Deployment
- Configure HTTPS endpoints
- Implement proper authentication/authorization
- Add logging and monitoring
- Configure database persistence (currently uses in-memory storage)
- Set up proper error handling and fault contracts

### Security Considerations
- Implement WS-Security for message-level security
- Add input validation and sanitization
- Configure rate limiting
- Implement proper exception handling to avoid information disclosure

## Extending the Service

### Adding New Operations
1. Define the operation in the service contract interface
2. Implement the operation in the service class
3. Add appropriate data contracts if needed
4. Update documentation

### Adding New Services
1. Create new service contract interface
2. Implement the service class
3. Register the service in Program.cs
4. Configure the SOAP endpoint

## Troubleshooting

### Common Issues
- **Port conflicts**: Change the port in the run command
- **WSDL not generating**: Ensure SoapCore is properly configured
- **Client connection issues**: Verify the service is running and accessible

### Debugging
- Enable detailed logging in appsettings.json
- Use browser developer tools to inspect SOAP requests/responses
- Check the console output for service startup messages

## License

This is a proof of concept project for demonstration purposes.

## Author

**Manus AI** - SOAP Service Implementation Specialist

---

*Built with .NET 8 and SoapCore - A modern approach to SOAP services in .NET*

