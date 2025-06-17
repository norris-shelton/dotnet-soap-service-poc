using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SoapClient
{
    class Program
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const string BaseUrl = "http://localhost:5000";

        static async Task Main(string[] args)
        {
            Console.WriteLine("=== SOAP Service Client Demo ===\n");

            try
            {
                // Test Calculator Service
                Console.WriteLine("Testing Calculator Service:");
                await TestCalculatorService();

                Console.WriteLine("\n" + new string('=', 50) + "\n");

                // Test User Service
                Console.WriteLine("Testing User Service:");
                await TestUserService();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static async Task TestCalculatorService()
        {
            // Test simple Add operation
            Console.WriteLine("1. Testing Add operation (5 + 3):");
            var addSoapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <Add xmlns=""http://tempuri.org/"">
            <a>5</a>
            <b>3</b>
        </Add>
    </soap:Body>
</soap:Envelope>";

            var addResult = await SendSoapRequest("/CalculatorService.asmx", addSoapRequest, "http://tempuri.org/ICalculatorService/Add");
            Console.WriteLine($"Response: {addResult}\n");

            // Test Calculator Info
            Console.WriteLine("2. Testing GetCalculatorInfo:");
            var infoSoapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <GetCalculatorInfo xmlns=""http://tempuri.org/"" />
    </soap:Body>
</soap:Envelope>";

            var infoResult = await SendSoapRequest("/CalculatorService.asmx", infoSoapRequest, "http://tempuri.org/ICalculatorService/GetCalculatorInfo");
            Console.WriteLine($"Response: {infoResult}\n");

            // Test Complex Calculate operation
            Console.WriteLine("3. Testing Calculate operation (10 * 4):");
            var calculateSoapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
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

            var calculateResult = await SendSoapRequest("/CalculatorService.asmx", calculateSoapRequest, "http://tempuri.org/ICalculatorService/Calculate");
            Console.WriteLine($"Response: {calculateResult}\n");
        }

        static async Task TestUserService()
        {
            // Test GetAllUsers
            Console.WriteLine("1. Testing GetAllUsers:");
            var getAllUsersSoapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <GetAllUsers xmlns=""http://tempuri.org/"" />
    </soap:Body>
</soap:Envelope>";

            var getAllUsersResult = await SendSoapRequest("/UserService.asmx", getAllUsersSoapRequest, "http://tempuri.org/IUserService/GetAllUsers");
            Console.WriteLine($"Response: {getAllUsersResult}\n");

            // Test GetUserById
            Console.WriteLine("2. Testing GetUserById (ID: 1):");
            var getUserByIdSoapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <GetUserById xmlns=""http://tempuri.org/"">
            <userId>1</userId>
        </GetUserById>
    </soap:Body>
</soap:Envelope>";

            var getUserByIdResult = await SendSoapRequest("/UserService.asmx", getUserByIdSoapRequest, "http://tempuri.org/IUserService/GetUserById");
            Console.WriteLine($"Response: {getUserByIdResult}\n");

            // Test CreateUser
            Console.WriteLine("3. Testing CreateUser:");
            var createUserSoapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
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

            var createUserResult = await SendSoapRequest("/UserService.asmx", createUserSoapRequest, "http://tempuri.org/IUserService/CreateUser");
            Console.WriteLine($"Response: {createUserResult}\n");
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
                
                return xml;
            }
            catch
            {
                return xml;
            }
        }
    }
}

