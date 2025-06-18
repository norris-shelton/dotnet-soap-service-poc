using SoapCore;
using SoapServicePoc.Contracts;
using SoapServicePoc.Services;
using System.ServiceModel;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configure JSON serialization for REST APIs
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// Add CORS support for REST APIs
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Register SOAP services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICalculatorService, CalculatorService>();

// Add SoapCore
builder.Services.AddSoapCore();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "SOAP/REST Hybrid Service", 
        Version = "v1",
        Description = "A hybrid service providing both SOAP and REST endpoints for User and Calculator operations"
    });
    
    // Include XML comments for better API documentation
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SOAP/REST Hybrid Service v1");
        c.RoutePrefix = "swagger";
    });
}

// Enable CORS
app.UseCors();

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
    <title>SOAP/REST Hybrid Service</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 40px; }
        .service { background: #f5f5f5; padding: 20px; margin: 20px 0; border-radius: 5px; }
        .endpoint { background: #e8f4fd; padding: 10px; margin: 10px 0; border-radius: 3px; }
        .rest-endpoint { background: #e8f8e8; padding: 10px; margin: 10px 0; border-radius: 3px; }
        code { background: #f0f0f0; padding: 2px 4px; border-radius: 2px; }
        .method { font-weight: bold; color: #0066cc; }
        .url { font-family: monospace; background: #f8f8f8; padding: 2px 4px; }
    </style>
</head>
<body>
    <h1>SOAP/REST Hybrid Service</h1>
    <p>Welcome to the hybrid service that provides both SOAP and REST endpoints for the same operations.</p>
    
    <div class='service'>
        <h2>User Service</h2>
        <p>Provides user management operations including create, read, update, and delete operations.</p>
        
        <h3>SOAP Endpoint</h3>
        <div class='endpoint'>
            <strong>WSDL:</strong> <a href='/UserService.asmx?wsdl'>/UserService.asmx?wsdl</a><br>
            <strong>Endpoint:</strong> <code>/UserService.asmx</code>
        </div>
        
        <h3>REST Endpoints</h3>
        <div class='rest-endpoint'>
            <span class='method'>GET</span> <span class='url'>/api/users</span> - Get all users<br>
            <span class='method'>GET</span> <span class='url'>/api/users/{id}</span> - Get user by ID<br>
            <span class='method'>GET</span> <span class='url'>/api/users/by-email/{email}</span> - Get user by email<br>
            <span class='method'>POST</span> <span class='url'>/api/users</span> - Create new user<br>
            <span class='method'>PUT</span> <span class='url'>/api/users/{id}</span> - Update user<br>
            <span class='method'>DELETE</span> <span class='url'>/api/users/{id}</span> - Delete user
        </div>
    </div>
    
    <div class='service'>
        <h2>Calculator Service</h2>
        <p>Provides mathematical calculation operations.</p>
        
        <h3>SOAP Endpoint</h3>
        <div class='endpoint'>
            <strong>WSDL:</strong> <a href='/CalculatorService.asmx?wsdl'>/CalculatorService.asmx?wsdl</a><br>
            <strong>Endpoint:</strong> <code>/CalculatorService.asmx</code>
        </div>
        
        <h3>REST Endpoints</h3>
        <div class='rest-endpoint'>
            <span class='method'>GET</span> <span class='url'>/api/calculator/info</span> - Get service info<br>
            <span class='method'>GET</span> <span class='url'>/api/calculator/add?a={num}&b={num}</span> - Add numbers<br>
            <span class='method'>GET</span> <span class='url'>/api/calculator/subtract?a={num}&b={num}</span> - Subtract numbers<br>
            <span class='method'>GET</span> <span class='url'>/api/calculator/multiply?a={num}&b={num}</span> - Multiply numbers<br>
            <span class='method'>GET</span> <span class='url'>/api/calculator/divide?a={num}&b={num}</span> - Divide numbers<br>
            <span class='method'>POST</span> <span class='url'>/api/calculator/calculate</span> - Complex calculation<br>
            <span class='method'>POST</span> <span class='url'>/api/calculator/simple</span> - Simple calculation
        </div>
    </div>
    
    <h3>API Documentation</h3>
    <p><strong>Swagger UI:</strong> <a href='/swagger'>/swagger</a> - Interactive REST API documentation</p>
    
    <h3>Testing the Services</h3>
    <p>You can test these services using:</p>
    <ul>
        <li><strong>SOAP:</strong> SOAP UI, Postman with SOAP requests, or custom .NET client applications</li>
        <li><strong>REST:</strong> Postman, curl, browser, or any HTTP client</li>
        <li><strong>Interactive:</strong> Swagger UI for REST endpoints</li>
    </ul>
    
    <h3>Example REST Calls</h3>
    <div class='rest-endpoint'>
        <strong>Get all users:</strong> <a href='/api/users'>GET /api/users</a><br>
        <strong>Add numbers:</strong> <a href='/api/calculator/add?a=5&b=3'>GET /api/calculator/add?a=5&b=3</a><br>
        <strong>Calculator info:</strong> <a href='/api/calculator/info'>GET /api/calculator/info</a>
    </div>
    
    <p><em>Built with .NET 8, SoapCore, and ASP.NET Core - Hybrid SOAP/REST Service</em></p>
</body>
</html>";
    return Results.Content(html, "text/html");
});

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

