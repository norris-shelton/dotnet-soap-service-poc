using System.ServiceModel;
using System.Runtime.Serialization;

namespace SoapServicePoc.Contracts
{
    [ServiceContract]
    public interface ICalculatorService
    {
        [OperationContract]
        double Add(double a, double b);

        [OperationContract]
        double Subtract(double a, double b);

        [OperationContract]
        double Multiply(double a, double b);

        [OperationContract]
        double Divide(double a, double b);

        [OperationContract]
        CalculationResult Calculate(CalculationRequest request);

        [OperationContract]
        string GetCalculatorInfo();
    }

    [DataContract]
    public class CalculationRequest
    {
        [DataMember]
        public double FirstNumber { get; set; }

        [DataMember]
        public double SecondNumber { get; set; }

        [DataMember]
        public string Operation { get; set; } = string.Empty; // "add", "subtract", "multiply", "divide"
    }

    [DataContract]
    public class CalculationResult
    {
        [DataMember]
        public double Result { get; set; }

        [DataMember]
        public string Operation { get; set; } = string.Empty;

        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; } = string.Empty;

        [DataMember]
        public DateTime CalculatedAt { get; set; }
    }
}

