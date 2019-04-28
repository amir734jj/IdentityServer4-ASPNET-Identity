using Core.Interfaces;
using Xunit;

namespace Core.Tests
{
    public class AmortizationCalculatorTest
    {
        private readonly IAmortizationCalculator _logic;

        public AmortizationCalculatorTest()
        {
            _logic = new AmortizationCalculator();
        }
        
        [Fact]
        public void Test__AmortizationCalculatorFunc()
        {
            // Arrange
            var startingBalance = new decimal(10000.00);
            var apr = new decimal(17.0 / 100);
            const int months = 2;

            // Act
            var result = _logic.AmortizationCalculatorFunc(startingBalance, apr, months);

            // Assert
            Assert.NotNull(result);
        }
    }
}