using SoapCore;
using SoapServicePoc.Contracts;
using SoapServicePoc.Services;
using System.ServiceModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register SOAP services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICalculatorService, CalculatorService>();

// Add SoapCore
builder.Services.AddSoapCore();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure SOAP endpoints
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<IUserService>("/UserService.asmx", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
    endpoints.UseSoapEndpoint<ICalculatorService>("/CalculatorService.asmx", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
});

// Add a simple welcome page
app.MapGet("/", () => 
{
    var html = @"
<!DOCTYPE html>
<html>
<head>
    <title>SOAP Service POC</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 40px; }
        .service { background: #f5f5f5; padding: 20px; margin: 20px 0; border-radius: 5px; }
        .endpoint { background: #e8f4fd; padding: 10px; margin: 10px 0; border-radius: 3px; }
        code { background: #f0f0f0; padding: 2px 4px; border-radius: 2px; }
    </style>
</head>
<body>
    <h1>SOAP Service POC</h1>
    <p>Welcome to the SOAP Service Proof of Concept. This service provides two SOAP endpoints:</p>
    
    <div class='service'>
        <h2>User Service</h2>
        <p>Provides user management operations including create, read, update, and delete operations.</p>
        <div class='endpoint'>
            <strong>WSDL:</strong> <a href='/UserService.asmx?wsdl'>/UserService.asmx?wsdl</a><br>
            <strong>Endpoint:</strong> <code>/UserService.asmx</code>
        </div>
        <p><strong>Available Operations:</strong></p>
        <ul>
            <li>CreateUserAsync - Create a new user</li>
            <li>GetUserByIdAsync - Get user by ID</li>
            <li>GetAllUsersAsync - Get all users</li>
            <li>UpdateUserAsync - Update existing user</li>
            <li>DeleteUserAsync - Delete user by ID</li>
            <li>GetUserByEmailAsync - Get user by email</li>
        </ul>
    </div>
    
    <div class='service'>
        <h2>Calculator Service</h2>
        <p>Provides mathematical calculation operations.</p>
        <div class='endpoint'>
            <strong>WSDL:</strong> <a href='/CalculatorService.asmx?wsdl'>/CalculatorService.asmx?wsdl</a><br>
            <strong>Endpoint:</strong> <code>/CalculatorService.asmx</code>
        </div>
        <p><strong>Available Operations:</strong></p>
        <ul>
            <li>Add - Add two numbers</li>
            <li>Subtract - Subtract two numbers</li>
            <li>Multiply - Multiply two numbers</li>
            <li>Divide - Divide two numbers</li>
            <li>Calculate - Perform calculation based on operation string</li>
            <li>GetCalculatorInfo - Get service information</li>
        </ul>
    </div>
    
    <h3>Testing the Services</h3>
    <p>You can test these services using:</p>
    <ul>
        <li>SOAP UI or similar SOAP testing tools</li>
        <li>Postman with SOAP requests</li>
        <li>Custom .NET client applications</li>
        <li>Any SOAP-compatible client</li>
    </ul>
    
    <p><em>Built with .NET 8 and SoapCore</em></p>
</body>
</html>";
    return Results.Content(html, "text/html");
});

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

