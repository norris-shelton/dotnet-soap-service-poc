using Microsoft.AspNetCore.Mvc;
using SoapServicePoc.Contracts;

namespace SoapServicePoc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        /// <summary>
        /// Get calculator service information
        /// </summary>
        /// <returns>Service information</returns>
        [HttpGet("info")]
        public ActionResult<object> GetCalculatorInfo()
        {
            try
            {
                var info = _calculatorService.GetCalculatorInfo();
                return Ok(new { 
                    success = true, 
                    message = "Calculator info retrieved successfully",
                    info = info 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = $"Internal server error: {ex.Message}" 
                });
            }
        }

        /// <summary>
        /// Add two numbers
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Sum of the two numbers</returns>
        [HttpGet("add")]
        public ActionResult<object> Add([FromQuery] double a, [FromQuery] double b)
        {
            try
            {
                var result = _calculatorService.Add(a, b);
                return Ok(new { 
                    success = true,
                    operation = "add",
                    firstNumber = a,
                    secondNumber = b,
                    result = result,
                    calculatedAt = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = $"Internal server error: {ex.Message}" 
                });
            }
        }

        /// <summary>
        /// Subtract two numbers
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Difference of the two numbers</returns>
        [HttpGet("subtract")]
        public ActionResult<object> Subtract([FromQuery] double a, [FromQuery] double b)
        {
            try
            {
                var result = _calculatorService.Subtract(a, b);
                return Ok(new { 
                    success = true,
                    operation = "subtract",
                    firstNumber = a,
                    secondNumber = b,
                    result = result,
                    calculatedAt = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = $"Internal server error: {ex.Message}" 
                });
            }
        }

        /// <summary>
        /// Multiply two numbers
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Product of the two numbers</returns>
        [HttpGet("multiply")]
        public ActionResult<object> Multiply([FromQuery] double a, [FromQuery] double b)
        {
            try
            {
                var result = _calculatorService.Multiply(a, b);
                return Ok(new { 
                    success = true,
                    operation = "multiply",
                    firstNumber = a,
                    secondNumber = b,
                    result = result,
                    calculatedAt = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = $"Internal server error: {ex.Message}" 
                });
            }
        }

        /// <summary>
        /// Divide two numbers
        /// </summary>
        /// <param name="a">First number (dividend)</param>
        /// <param name="b">Second number (divisor)</param>
        /// <returns>Quotient of the two numbers</returns>
        [HttpGet("divide")]
        public ActionResult<object> Divide([FromQuery] double a, [FromQuery] double b)
        {
            try
            {
                var result = _calculatorService.Divide(a, b);
                return Ok(new { 
                    success = true,
                    operation = "divide",
                    firstNumber = a,
                    secondNumber = b,
                    result = result,
                    calculatedAt = DateTime.Now
                });
            }
            catch (DivideByZeroException ex)
            {
                return BadRequest(new { 
                    success = false,
                    operation = "divide",
                    firstNumber = a,
                    secondNumber = b,
                    message = ex.Message,
                    calculatedAt = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = $"Internal server error: {ex.Message}" 
                });
            }
        }

        /// <summary>
        /// Perform calculation based on operation string
        /// </summary>
        /// <param name="request">Calculation request with operation details</param>
        /// <returns>Calculation result</returns>
        [HttpPost("calculate")]
        public ActionResult<CalculationResult> Calculate([FromBody] CalculationRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new CalculationResult
                    {
                        Success = false,
                        ErrorMessage = "Request body is required",
                        CalculatedAt = DateTime.Now
                    });
                }

                var result = _calculatorService.Calculate(request);
                
                if (!result.Success)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CalculationResult
                {
                    Success = false,
                    ErrorMessage = $"Internal server error: {ex.Message}",
                    CalculatedAt = DateTime.Now
                });
            }
        }

        /// <summary>
        /// Perform calculation using POST with individual parameters
        /// </summary>
        /// <param name="request">Simple calculation request</param>
        /// <returns>Calculation result</returns>
        [HttpPost("simple")]
        public ActionResult<object> SimpleCalculate([FromBody] SimpleCalculationRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { 
                        success = false, 
                        message = "Request body is required" 
                    });
                }

                var calcRequest = new CalculationRequest
                {
                    FirstNumber = request.A,
                    SecondNumber = request.B,
                    Operation = request.Operation
                };

                var result = _calculatorService.Calculate(calcRequest);
                
                return Ok(new {
                    success = result.Success,
                    operation = result.Operation,
                    firstNumber = request.A,
                    secondNumber = request.B,
                    result = result.Result,
                    errorMessage = result.ErrorMessage,
                    calculatedAt = result.CalculatedAt
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = $"Internal server error: {ex.Message}" 
                });
            }
        }
    }

    /// <summary>
    /// Simple calculation request for REST API
    /// </summary>
    public class SimpleCalculationRequest
    {
        public double A { get; set; }
        public double B { get; set; }
        public string Operation { get; set; } = string.Empty;
    }
}

