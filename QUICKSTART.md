# Quick Start Guide - SOAP/REST Hybrid Service

## Prerequisites

- .NET 8 SDK installed
- Command line terminal
- Optional: Visual Studio 2022, VS Code, or any C# IDE
- Optional: Postman, SoapUI, or other API testing tools

## 5-Minute Setup

### 1. Extract and Navigate
```bash
# Extract the project files
# Navigate to the project directory
cd soap-service-poc
```

### 2. Start the Hybrid Service
```bash
cd SoapServicePoc
dotnet run --urls="http://localhost:5000"
```

The service will start and display:
- **Homepage**: http://localhost:5000 (shows both SOAP and REST endpoints)
- **Swagger UI**: http://localhost:5000/swagger (interactive REST API docs)
- **User Service WSDL**: http://localhost:5000/UserService.asmx?wsdl
- **Calculator Service WSDL**: http://localhost:5000/CalculatorService.asmx?wsdl

### 3. Test SOAP Services (New Terminal)
```bash
cd SoapClient
dotnet run
```

You'll see SOAP test results for both services immediately.

### 4. Test REST APIs (Browser/Postman)

**Quick Browser Tests:**
- Get all users: http://localhost:5000/api/users
- Add numbers: http://localhost:5000/api/calculator/add?a=5&b=3
- Calculator info: http://localhost:5000/api/calculator/info

**Interactive Testing:**
- Visit http://localhost:5000/swagger for full REST API documentation

## What You Get

✅ **Hybrid SOAP/REST Architecture**
- Same business logic serves both protocols
- Choose SOAP (XML) or REST (JSON) based on your needs
- Single codebase, dual protocol support

✅ **Two Complete Services**
- User Management (CRUD operations)
- Calculator (Math operations)

✅ **Multiple Access Methods**
- SOAP endpoints with WSDL generation
- REST endpoints with JSON responses
- Interactive Swagger documentation
- Test clients for both protocols

✅ **Production-Ready Features**
- Proper error handling and validation
- CORS support for web applications
- Async operations support
- Comprehensive documentation

## Protocol Comparison

| Feature | SOAP | REST |
|---------|------|------|
| **Format** | XML | JSON |
| **Protocol** | HTTP POST | HTTP GET/POST/PUT/DELETE |
| **Documentation** | WSDL | Swagger/OpenAPI |
| **Best For** | Enterprise integration | Web/mobile apps |
| **Testing** | SoapUI, specialized tools | Browser, Postman, curl |

## Quick API Examples

### REST Examples

**Get All Users (JSON)**
```bash
curl http://localhost:5000/api/users
```

**Add Numbers (JSON)**
```bash
curl "http://localhost:5000/api/calculator/add?a=10&b=5"
```

**Create User (JSON)**
```bash
curl -X POST http://localhost:5000/api/users \
  -H "Content-Type: application/json" \
  -d '{"firstName":"Alice","lastName":"Johnson","email":"alice@example.com"}'
```

### SOAP Examples

**Calculator Add (XML)**
```xml
POST /CalculatorService.asmx
Content-Type: text/xml; charset=utf-8

<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
    <soap:Body>
        <Add xmlns="http://tempuri.org/">
            <a>10</a>
            <b>5</b>
        </Add>
    </soap:Body>
</soap:Envelope>
```

## Service Endpoints Summary

### User Service
- **SOAP**: `/UserService.asmx` (WSDL: `?wsdl`)
- **REST**: `/api/users` (Swagger: `/swagger`)

### Calculator Service  
- **SOAP**: `/CalculatorService.asmx` (WSDL: `?wsdl`)
- **REST**: `/api/calculator` (Swagger: `/swagger`)

## Next Steps

1. **Explore Both Protocols**: Test the same operations via SOAP and REST
2. **Use Swagger UI**: Interactive testing at http://localhost:5000/swagger
3. **Check WSDL**: View formal contracts at the WSDL endpoints
4. **Extend Services**: Add new operations following the established patterns
5. **Deploy**: Configure for your target environment

## Common Commands

```bash
# Build the service
dotnet build

# Run with custom port
dotnet run --urls="http://localhost:8080"

# Publish for deployment
dotnet publish -c Release

# Test SOAP client
cd SoapClient && dotnet run

# View project structure
tree soap-service-poc
```

## Testing Tools

### For REST APIs
- **Browser**: Direct URL access for GET requests
- **Swagger UI**: Interactive documentation and testing
- **Postman**: Full-featured API testing
- **curl**: Command-line HTTP requests
- **HTTPie**: User-friendly command-line tool

### For SOAP Services
- **SoapUI**: Professional SOAP testing tool
- **Postman**: SOAP request support
- **Custom .NET clients**: Like the included SoapClient
- **Browser**: WSDL viewing and basic testing

## Troubleshooting

**Port in use?** Change the port number in the run command.

**WSDL not loading?** Ensure the service is running and accessible.

**REST endpoints not found?** Check the service startup logs for errors.

**JSON format issues?** Verify Content-Type headers for POST requests.

**CORS errors?** The service includes CORS support for development.

## Architecture Benefits

✅ **Code Reuse**: Single business logic implementation
✅ **Client Choice**: SOAP for enterprise, REST for web/mobile  
✅ **Gradual Migration**: Support both protocols during transitions
✅ **Testing Flexibility**: Multiple ways to test and validate
✅ **Documentation**: Both WSDL and Swagger available

---

*Ready to build hybrid SOAP/REST services with .NET 8! Choose your protocol, same great functionality.*

