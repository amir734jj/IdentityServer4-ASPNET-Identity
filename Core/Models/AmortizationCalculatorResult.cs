namespace Core.Models
{
    public class AmortizationCalculatorResult
    {
        public decimal StartingBalance { get; set; }
        
        public decimal Apr { get; set; }
        
        public int Months { get; set; }
        
        public decimal TotalBalance { get; set; }
        
        public decimal Interest { get; set; }
        
        public decimal MonthlyPayment { get; set; }

        public int Period { get; set; }
    }
}