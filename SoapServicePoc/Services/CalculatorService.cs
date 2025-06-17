using SoapServicePoc.Contracts;

namespace SoapServicePoc.Services
{
    public class CalculatorService : ICalculatorService
    {
        public double Add(double a, double b)
        {
            return a + b;
        }

        public double Subtract(double a, double b)
        {
            return a - b;
        }

        public double Multiply(double a, double b)
        {
            return a * b;
        }

        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero");
            }
            return a / b;
        }

        public CalculationResult Calculate(CalculationRequest request)
        {
            var result = new CalculationResult
            {
                Operation = request.Operation,
                CalculatedAt = DateTime.Now,
                Success = true
            };

            try
            {
                switch (request.Operation.ToLower())
                {
                    case "add":
                        result.Result = Add(request.FirstNumber, request.SecondNumber);
                        break;
                    case "subtract":
                        result.Result = Subtract(request.FirstNumber, request.SecondNumber);
                        break;
                    case "multiply":
                        result.Result = Multiply(request.FirstNumber, request.SecondNumber);
                        break;
                    case "divide":
                        result.Result = Divide(request.FirstNumber, request.SecondNumber);
                        break;
                    default:
                        result.Success = false;
                        result.ErrorMessage = "Invalid operation. Supported operations: add, subtract, multiply, divide";
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.Result = 0;
            }

            return result;
        }

        public string GetCalculatorInfo()
        {
            return $"SOAP Calculator Service v1.0 - Available operations: Add, Subtract, Multiply, Divide. Current time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        }
    }
}

