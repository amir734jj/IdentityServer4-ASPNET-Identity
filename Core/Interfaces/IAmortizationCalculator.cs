using Core.Models;

namespace Core.Interfaces
{
    public interface IAmortizationCalculator
    {
        AmortizationCalculatorResult AmortizationCalculatorFunc(decimal startingBalance, decimal apr, int months);
    }
}