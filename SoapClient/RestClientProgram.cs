using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestClient
{
    class Program
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const string BaseUrl = "http://localhost:5000";

        static async Task Main(string[] args)
        {
            Console.WriteLine("=== REST API Client Demo ===\n");

            try
            {
                // Test Calculator REST endpoints
                Console.WriteLine("Testing Calculator REST Endpoints:");
                await TestCalculatorRestEndpoints();

                Console.WriteLine("\n" + new string('=', 50) + "\n");

                // Test User REST endpoints
                Console.WriteLine("Testing User REST Endpoints:");
                await TestUserRestEndpoints();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static async Task TestCalculatorRestEndpoints()
        {
            // Test Calculator Info
            Console.WriteLine("1. Testing GET /api/calculator/info:");
            var infoResponse = await httpClient.GetStringAsync($"{BaseUrl}/api/calculator/info");
            Console.WriteLine($"Response: {infoResponse}\n");

            // Test Add operation
            Console.WriteLine("2. Testing GET /api/calculator/add?a=10&b=5:");
            var addResponse = await httpClient.GetStringAsync($"{BaseUrl}/api/calculator/add?a=10&b=5");
            Console.WriteLine($"Response: {addResponse}\n");

            // Test Multiply operation
            Console.WriteLine("3. Testing GET /api/calculator/multiply?a=7&b=6:");
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

        static async Task TestUserRestEndpoints()
        {
            // Test Get All Users
            Console.WriteLine("1. Testing GET /api/users:");
            var usersResponse = await httpClient.GetStringAsync($"{BaseUrl}/api/users");
            Console.WriteLine($"Response: {usersResponse}\n");

            // Test Get User by ID
            Console.WriteLine("2. Testing GET /api/users/1:");
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
    }
}

