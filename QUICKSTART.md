# Quick Start Guide

## Prerequisites

- .NET 8 SDK installed
- Command line terminal
- Optional: Visual Studio 2022, VS Code, or any C# IDE

## 5-Minute Setup

### 1. Extract and Navigate
```bash
# Extract the project files
# Navigate to the project directory
cd soap-service-poc
```

### 2. Start the SOAP Service
```bash
cd SoapServicePoc
dotnet run --urls="http://localhost:5000"
```

The service will start and display:
- Homepage: http://localhost:5000
- User Service WSDL: http://localhost:5000/UserService.asmx?wsdl
- Calculator Service WSDL: http://localhost:5000/CalculatorService.asmx?wsdl

### 3. Test with Client (New Terminal)
```bash
cd SoapClient
dotnet run
```

You'll see test results for both services immediately.

## What You Get

✅ **Two Working SOAP Services**
- User Management (CRUD operations)
- Calculator (Math operations)

✅ **Complete WSDL Generation**
- Automatic metadata generation
- Standards-compliant SOAP contracts

✅ **Test Client**
- Demonstrates all operations
- Shows proper SOAP request/response handling

✅ **Production-Ready Structure**
- Proper separation of concerns
- Error handling and validation
- Extensible architecture

## Next Steps

1. **Explore the Code**: Check out the service implementations in `/Services/`
2. **Test with Tools**: Use SoapUI or Postman to test the endpoints
3. **Extend Services**: Add new operations or services following the patterns
4. **Deploy**: Configure for your target environment

## Common Commands

```bash
# Build the service
dotnet build

# Run with custom port
dotnet run --urls="http://localhost:8080"

# Publish for deployment
dotnet publish -c Release

# Run tests
dotnet test
```

## Troubleshooting

**Port in use?** Change the port number in the run command.

**WSDL not loading?** Ensure the service is running and accessible.

**Client connection failed?** Verify the service URL in the client code.

---

*Ready to build enterprise SOAP services with .NET 8!*

