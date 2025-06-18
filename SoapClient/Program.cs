using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SoapClient
{
    class Program
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const string BaseUrl = "http://localhost:5000";

        static async Task Main(string[] args)
        {
            Console.WriteLine("=== SOAP/REST Hybrid Client Demo ===\n");
            Console.WriteLine("This client demonstrates testing both SOAP and REST endpoints of the hybrid service.\n");

            while (true)
            {
                Console.WriteLine("Choose testing option:");
                Console.WriteLine("1. Test SOAP Services (XML)");
                Console.WriteLine("2. Test REST APIs (JSON)");
                Console.WriteLine("3. Test Both Protocols");
                Console.WriteLine("4. Exit");
                Console.Write("\nEnter your choice (1-4): ");

                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await TestSoapServices();
                            break;
                        case "2":
                            await TestRestApis();
                            break;
                        case "3":
                            await TestBothProtocols();
                            break;
                        case "4":
                            Console.WriteLine("Goodbye!");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter 1-4.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Make sure the service is running at http://localhost:5000");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static async Task TestSoapServices()
        {
            Console.WriteLine("\n=== Testing SOAP Services (XML) ===\n");

            // Test Calculator SOAP Service
            Console.WriteLine("Testing Calculator SOAP Service:");
            await TestCalculatorSoap();

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            // Test User SOAP Service
            Console.WriteLine("Testing User SOAP Service:");
            await TestUserSoap();
        }

        static async Task TestRestApis()
        {
            Console.WriteLine("\n=== Testing REST APIs (JSON) ===\n");

            // Test Calculator REST endpoints
            Console.WriteLine("Testing Calculator REST Endpoints:");
            await TestCalculatorRest();

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            // Test User REST endpoints
            Console.WriteLine("Testing User REST Endpoints:");
            await TestUserRest();
        }

        static async Task TestBothProtocols()
        {
            Console.WriteLine("\n=== Testing Both SOAP and REST Protocols ===\n");
            
            Console.WriteLine("🔄 Protocol Comparison - Same Operation, Different Formats:\n");
            
            // Compare Calculator Add operation
            Console.WriteLine("Calculator Add Operation (5 + 3):");
            Console.WriteLine("📋 SOAP Response (XML):");
            await TestCalculatorSoapAdd();
            
            Console.WriteLine("\n📋 REST Response (JSON):");
            await TestCalculatorRestAdd();

            Console.WriteLine("\n" + new string('=', 60) + "\n");

            // Compare User listing
            Console.WriteLine("User Listing:");
            Console.WriteLine("📋 SOAP Response (XML):");
            await TestUserSoapGetAll();
            
            Console.WriteLine("\n📋 REST Response (JSON):");
            await TestUserRestGetAll();
        }

        static async Task TestCalculatorSoap()
        {
            // Test Calculator Add via SOAP
            Console.WriteLine("1. Testing Calculator Add (5 + 3) via SOAP:");
            await TestCalculatorSoapAdd();

            Console.WriteLine("\n2. Testing Calculator Info via SOAP:");
            await TestCalculatorSoapInfo();

            Console.WriteLine("\n3. Testing Complex Calculation (10 * 4) via SOAP:");
            await TestCalculatorSoapComplex();
        }

        static async Task TestCalculatorSoapAdd()
        {
            var soapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <Add xmlns=""http://tempuri.org/"">
            <a>5</a>
            <b>3</b>
        </Add>
    </soap:Body>
</soap:Envelope>";

            var result = await SendSoapRequest("/CalculatorService.asmx", soapRequest, "http://tempuri.org/ICalculatorService/Add");
            Console.WriteLine($"Response: {result}");
        }

        static async Task TestCalculatorSoapInfo()
        {
            var soapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <GetCalculatorInfo xmlns=""http://tempuri.org/"" />
    </soap:Body>
</soap:Envelope>";

            var result = await SendSoapRequest("/CalculatorService.asmx", soapRequest, "http://tempuri.org/ICalculatorService/GetCalculatorInfo");
            Console.WriteLine($"Response: {result}");
        }

        static async Task TestCalculatorSoapComplex()
        {
            var soapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <Calculate xmlns=""http://tempuri.org/"">
            <request>
                <FirstNumber>10</FirstNumber>
                <SecondNumber>4</SecondNumber>
                <Operation>multiply</Operation>
            </request>
        </Calculate>
    </soap:Body>
</soap:Envelope>";

            var result = await SendSoapRequest("/CalculatorService.asmx", soapRequest, "http://tempuri.org/ICalculatorService/Calculate");
            Console.WriteLine($"Response: {result}");
        }

        static async Task TestUserSoap()
        {
            Console.WriteLine("1. Testing Get All Users via SOAP:");
            await TestUserSoapGetAll();

            Console.WriteLine("\n2. Testing Get User by ID (1) via SOAP:");
            await TestUserSoapGetById();

            Console.WriteLine("\n3. Testing Create User via SOAP:");
            await TestUserSoapCreate();
        }

        static async Task TestUserSoapGetAll()
        {
            var soapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <GetAllUsers xmlns=""http://tempuri.org/"" />
    </soap:Body>
</soap:Envelope>";

            var result = await SendSoapRequest("/UserService.asmx", soapRequest, "http://tempuri.org/IUserService/GetAllUsers");
            Console.WriteLine($"Response: {result}");
        }

        static async Task TestUserSoapGetById()
        {
            var soapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <GetUserById xmlns=""http://tempuri.org/"">
            <userId>1</userId>
        </GetUserById>
    </soap:Body>
</soap:Envelope>";

            var result = await SendSoapRequest("/UserService.asmx", soapRequest, "http://tempuri.org/IUserService/GetUserById");
            Console.WriteLine($"Response: {result}");
        }

        static async Task TestUserSoapCreate()
        {
            var soapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <CreateUser xmlns=""http://tempuri.org/"">
            <request>
                <FirstName>Alice</FirstName>
                <LastName>Johnson</LastName>
                <Email>alice.johnson@example.com</Email>
            </request>
        </CreateUser>
    </soap:Body>
</soap:Envelope>";

            var result = await SendSoapRequest("/UserService.asmx", soapRequest, "http://tempuri.org/IUserService/CreateUser");
            Console.WriteLine($"Response: {result}");
        }

        static async Task TestCalculatorRest()
        {
            // Test Calculator Info
            Console.WriteLine("1. Testing GET /api/calculator/info:");
            var infoResponse = await httpClient.GetStringAsync($"{BaseUrl}/api/calculator/info");
            Console.WriteLine($"Response: {infoResponse}\n");

            // Test Add operation
            Console.WriteLine("2. Testing GET /api/calculator/add?a=10&b=5:");
            await TestCalculatorRestAdd();

            // Test Multiply operation
            Console.WriteLine("\n3. Testing GET /api/calculator/multiply?a=7&b=6:");
            var multiplyResponse = await httpClient.GetStringAsync($"{BaseUrl}/api/calculator/multiply?a=7&b=6");
            Console.WriteLine($"Response: {multiplyResponse}\n");

            // Test Complex calculation via POST
            Console.WriteLine("4. Testing POST /api/calculator/simple:");
            var calcRequest = new
            {
                a = 20,
                b = 4,
                operation = "divide"
            };

            var json = JsonSerializer.Serialize(calcRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var calcResponse = await httpClient.PostAsync($"{BaseUrl}/api/calculator/simple", content);
            var calcResult = await calcResponse.Content.ReadAsStringAsync();
            Console.WriteLine($"Response: {calcResult}\n");
        }

        static async Task TestCalculatorRestAdd()
        {
            var addResponse = await httpClient.GetStringAsync($"{BaseUrl}/api/calculator/add?a=5&b=3");
            Console.WriteLine($"Response: {addResponse}");
        }

        static async Task TestUserRest()
        {
            // Test Get All Users
            Console.WriteLine("1. Testing GET /api/users:");
            await TestUserRestGetAll();

            // Test Get User by ID
            Console.WriteLine("\n2. Testing GET /api/users/1:");
            var userResponse = await httpClient.GetStringAsync($"{BaseUrl}/api/users/1");
            Console.WriteLine($"Response: {userResponse}\n");

            // Test Create User via POST
            Console.WriteLine("3. Testing POST /api/users:");
            var newUser = new
            {
                firstName = "Sarah",
                lastName = "Wilson",
                email = "sarah.wilson@example.com"
            };

            var userJson = JsonSerializer.Serialize(newUser);
            var userContent = new StringContent(userJson, Encoding.UTF8, "application/json");
            var createResponse = await httpClient.PostAsync($"{BaseUrl}/api/users", userContent);
            var createResult = await createResponse.Content.ReadAsStringAsync();
            Console.WriteLine($"Response: {createResult}\n");

            // Test Get User by Email
            Console.WriteLine("4. Testing GET /api/users/by-email/john.doe@example.com:");
            var emailResponse = await httpClient.GetStringAsync($"{BaseUrl}/api/users/by-email/john.doe@example.com");
            Console.WriteLine($"Response: {emailResponse}\n");
        }

        static async Task TestUserRestGetAll()
        {
            var usersResponse = await httpClient.GetStringAsync($"{BaseUrl}/api/users");
            Console.WriteLine($"Response: {usersResponse}");
        }

        static async Task<string> SendSoapRequest(string endpoint, string soapRequest, string soapAction)
        {
            try
            {
                var content = new StringContent(soapRequest, Encoding.UTF8, "text/xml");
                content.Headers.Add("SOAPAction", soapAction);

                var response = await httpClient.PostAsync($"{BaseUrl}{endpoint}", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return FormatXmlResponse(responseContent);
                }
                else
                {
                    return $"Error: {response.StatusCode} - {responseContent}";
                }
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }

        static string FormatXmlResponse(string xml)
        {
            try
            {
                // Simple formatting - extract the main content from SOAP envelope
                var startIndex = xml.IndexOf("<soap:Body>");
                var endIndex = xml.IndexOf("</soap:Body>");
                
                if (startIndex >= 0 && endIndex >= 0)
                {
                    var bodyContent = xml.Substring(startIndex + 11, endIndex - startIndex - 11);
                    return bodyContent.Trim();
                }
                
                return xml.Length > 200 ? xml.Substring(0, 200) + "..." : xml;
            }
            catch
            {
                return xml.Length > 200 ? xml.Substring(0, 200) + "..." : xml;
            }
        }
    }
}

