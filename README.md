# C# SOAP/REST Hybrid Service

## Overview

This project demonstrates a comprehensive hybrid service implementation using .NET 8 and SoapCore that provides both SOAP and REST endpoints for the same business operations. The service offers maximum flexibility by allowing clients to choose between SOAP (XML) and REST (JSON) protocols while maintaining a single codebase and shared business logic.

## Project Structure

```
soap-service-poc/
├── SoapServicePoc/                 # Main hybrid service project
│   ├── Controllers/                # REST API controllers
│   │   ├── UsersController.cs      # User REST endpoints
│   │   └── CalculatorController.cs # Calculator REST endpoints
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
│   ├── Program.cs                  # SOAP client implementation
│   ├── RestClientProgram.cs        # REST client implementation
│   └── SoapClient.csproj           # Client project file
└── README.md                       # This documentation
```

## Features

### Hybrid Architecture
- **Single Codebase**: Both SOAP and REST endpoints share the same business logic
- **Protocol Flexibility**: Clients can choose between SOAP (XML) and REST (JSON)
- **Consistent Data Models**: Same data structures used for both protocols
- **Unified Error Handling**: Consistent error responses across protocols

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

### REST API Endpoints

#### User Management API (`/api/users`)
- **GET /api/users** - Get all users
- **GET /api/users/{id}** - Get user by ID
- **GET /api/users/by-email/{email}** - Get user by email
- **POST /api/users** - Create new user
- **PUT /api/users/{id}** - Update existing user
- **DELETE /api/users/{id}** - Delete user

#### Calculator API (`/api/calculator`)
- **GET /api/calculator/info** - Get service information
- **GET /api/calculator/add?a={num}&b={num}** - Add two numbers
- **GET /api/calculator/subtract?a={num}&b={num}** - Subtract numbers
- **GET /api/calculator/multiply?a={num}&b={num}** - Multiply numbers
- **GET /api/calculator/divide?a={num}&b={num}** - Divide numbers
- **POST /api/calculator/calculate** - Complex calculation
- **POST /api/calculator/simple** - Simple calculation with JSON body

### Key Technical Features

- **WSDL Generation**: Automatic WSDL generation for SOAP services
- **Swagger Documentation**: Interactive REST API documentation
- **JSON Serialization**: Camel case JSON with proper formatting
- **CORS Support**: Cross-origin requests enabled for REST APIs
- **Data Contracts**: Proper XML/JSON serialization with attributes
- **Error Handling**: Comprehensive error handling and validation
- **Async Operations**: Support for asynchronous operations
- **Type Safety**: Strong typing with proper data models

## Technology Stack

- **.NET 8**: Latest .NET framework with performance improvements
- **SoapCore**: SOAP service implementation for .NET Core/8
- **ASP.NET Core**: Web framework for hosting both SOAP and REST services
- **System.ServiceModel**: Service model primitives for SOAP
- **Swagger/OpenAPI**: Interactive REST API documentation
- **System.Text.Json**: High-performance JSON serialization
- **C#**: Primary programming language

## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or VS Code (optional)
- Any SOAP client tool (SoapUI, Postman, etc.)
- Any REST client tool (Postman, curl, browser, etc.)

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
   - **Homepage**: http://localhost:5000
   - **Swagger UI**: http://localhost:5000/swagger
   - **User Service WSDL**: http://localhost:5000/UserService.asmx?wsdl
   - **Calculator Service WSDL**: http://localhost:5000/CalculatorService.asmx?wsdl

### Testing the Services

#### SOAP Testing
1. Navigate to the client directory:
   ```bash
   cd SoapClient
   ```

2. Run the SOAP client:
   ```bash
   dotnet run
   ```

#### REST Testing
- **Browser**: Visit http://localhost:5000/api/users or http://localhost:5000/api/calculator/info
- **Swagger UI**: Use the interactive documentation at http://localhost:5000/swagger
- **Postman**: Import the endpoints and test with JSON payloads
- **curl**: Use command line HTTP requests

## API Documentation

### REST Endpoints

#### User Management

**Get All Users**
```http
GET /api/users
```
Response:
```json
{
  "success": true,
  "message": "Retrieved 3 users successfully.",
  "users": [
    {
      "id": 1,
      "firstName": "John",
      "lastName": "Doe",
      "email": "john.doe@example.com",
      "createdDate": "2025-06-18T12:00:00Z",
      "isActive": true
    }
  ]
}
```

**Create User**
```http
POST /api/users
Content-Type: application/json

{
  "firstName": "Alice",
  "lastName": "Johnson",
  "email": "alice.johnson@example.com"
}
```

#### Calculator Operations

**Add Numbers**
```http
GET /api/calculator/add?a=5&b=3
```
Response:
```json
{
  "success": true,
  "operation": "add",
  "firstNumber": 5,
  "secondNumber": 3,
  "result": 8,
  "calculatedAt": "2025-06-18T12:00:00Z"
}
```

**Complex Calculation**
```http
POST /api/calculator/simple
Content-Type: application/json

{
  "a": 10,
  "b": 4,
  "operation": "multiply"
}
```

### SOAP Endpoints

#### User Service WSDL: `/UserService.asmx?wsdl`

**Sample SOAP Request - Get All Users**
```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
    <soap:Body>
        <GetAllUsers xmlns="http://tempuri.org/" />
    </soap:Body>
</soap:Envelope>
```

#### Calculator Service WSDL: `/CalculatorService.asmx?wsdl`

**Sample SOAP Request - Add Numbers**
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

## Configuration

### Service Configuration (Program.cs)
```csharp
// Configure JSON serialization for REST APIs
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Add CORS support for REST APIs
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Register SOAP services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICalculatorService, CalculatorService>();
builder.Services.AddSoapCore();

// Configure SOAP endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<IUserService>("/UserService.asmx", 
        new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
    endpoints.UseSoapEndpoint<ICalculatorService>("/CalculatorService.asmx", 
        new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
});
```

## Testing Results

### SOAP Service Tests ✅
- **Calculator Add**: 5 + 3 = 8 (Success)
- **Calculator Info**: Service metadata retrieved (Success)
- **Calculator Complex**: 10 × 4 = 40 (Success)
- **User GetAll**: Retrieved 3 users (Success)
- **User GetById**: Found user ID 1 (Success)
- **User Create**: Created Alice Johnson (Success)

### REST API Tests ✅
- **GET /api/calculator/info**: Service info retrieved (Success)
- **GET /api/calculator/add?a=5&b=3**: 5 + 3 = 8 (Success)
- **GET /api/users**: Retrieved all users in JSON (Success)
- **POST /api/calculator/simple**: Complex calculation (Success)
- **Swagger UI**: Interactive documentation working (Success)

## Deployment Considerations

### Production Deployment
- Configure HTTPS endpoints for both SOAP and REST
- Implement proper authentication/authorization
- Add comprehensive logging and monitoring
- Configure database persistence (currently uses in-memory storage)
- Set up proper error handling and fault contracts
- Implement rate limiting for REST endpoints

### Security Considerations
- Implement WS-Security for SOAP message-level security
- Add JWT authentication for REST endpoints
- Configure input validation and sanitization
- Implement proper exception handling to avoid information disclosure
- Set up CORS policies for production environments

## Performance Benefits

### Shared Business Logic
- Single implementation serves both protocols
- Reduced code duplication and maintenance overhead
- Consistent business rules across SOAP and REST

### Protocol Optimization
- SOAP: Formal contracts, enterprise integration
- REST: Lightweight JSON, web/mobile applications
- Clients choose optimal protocol for their needs

## Extending the Service

### Adding New Operations
1. Define the operation in the service contract interface
2. Implement the operation in the service class
3. Add REST endpoint in the appropriate controller
4. Update documentation

### Adding New Services
1. Create new service contract interface
2. Implement the service class
3. Create new REST controller
4. Register services in Program.cs
5. Configure SOAP endpoint

## Troubleshooting

### Common Issues
- **Port conflicts**: Change the port in the run command
- **WSDL not generating**: Ensure SoapCore is properly configured
- **REST endpoints not found**: Verify controller routing and registration
- **JSON serialization issues**: Check JsonOptions configuration
- **CORS errors**: Verify CORS policy configuration

### Debugging
- Enable detailed logging in appsettings.json
- Use browser developer tools for REST endpoint inspection
- Check Swagger UI for REST API testing
- Use SOAP testing tools for SOAP endpoint validation

## License

This is a proof of concept project for demonstration purposes.

## Author

**Manus AI** - Hybrid SOAP/REST Service Implementation Specialist

---

*Built with .NET 8, SoapCore, and ASP.NET Core - The ultimate hybrid SOAP/REST service solution*

