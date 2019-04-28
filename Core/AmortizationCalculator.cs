using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Core.Interfaces;
using Core.Models;

namespace Core
{
    public class AmortizationCalculator : IAmortizationCalculator
    {
        public AmortizationCalculator()
        {
            var result = new List<string>()
                .ToObservable()
                .Delay(x => Observable.Timer(TimeSpan.FromSeconds(x.Length)))
                .ForEachAsync(x =>
                {
                    
                });
        }
        
        public AmortizationCalculatorResult AmortizationCalculatorFunc(decimal startingBalance, decimal apr, int months)
        {
            var totalBalance = ResolveTotalBalance(startingBalance, apr, months); 
            var interest = totalBalance - startingBalance;
            var monthlyPayment = ResolveMonthlyPayment(startingBalance, apr, months);
            var period = ResolvePaymentPeriod(startingBalance, apr, monthlyPayment);

            return new AmortizationCalculatorResult
            {
                StartingBalance = startingBalance,
                Apr = apr,
                Months = months,
                TotalBalance = totalBalance,
                Interest = interest,
                MonthlyPayment = monthlyPayment,
                Period = period
            };
        }
        
        public decimal ResolveTotalBalance(decimal balance, decimal apr, int months) {
            // Compute our total balance which is the starting balance + interest.
            // http://www.math.com/tables/general/interest.htm
            // http://en.wikipedia.org/wiki/Compound_interest#Compound
            return balance * (decimal) Math.Pow((double) (1 + apr / 360), 360 * (months / 12));          
        }
        
        public decimal ResolveMonthlyPayment(decimal balance, decimal apr, int months) {
            // Get our monthly payment calculated by amortization (daily compounded).
            // http://www.vertex42.com/ExcelArticles/amortization-calculation.html
            // http://en.wikipedia.org/wiki/Amortization_calculator
    
            // We first need to figure out our monthly compounded payment rate.
            var r = Math.Pow( (double) (1 + apr / 360), 360.0 / 12) - 1;
    
            // Now we can use our monthly rate to figure out our monthly payment.
            return balance * (decimal) (r * Math.Pow(1 + r, months) / (Math.Pow(1 + r, months) - 1));          
        }
        
        public int ResolvePaymentPeriod(decimal balance, decimal apr, decimal payment)
        {
            var i = 0;
            var fixedPayment = payment;
    
            // Determine how many payments will be needed for the specified balance,
            // APR and payment.  We want to stop our while loop once we've made more
            // than 360 monthly payments (30 years).
            while (balance > 0 && i < 360) {
                // The line below first calculates the amount of interest (daily compounded)
                // on the current balance.  Then we subtract the current balance from
                // the current balance with interest for the monthly payment.  That result
                // gives us the total amount of interest for the month.  We then subtract
                // that value from the payment to come up with our principal.  We subtract
                // our principal from our current balance.
                var totalBalance = balance * (decimal) Math.Pow((double) (1 + (apr / 360)), 30);
                
                // Since we are manually going through the amortization schedule, our
                // last payment will be very close to our remaining total balance.
                // If that's the case, we will simply want to only subtract the remaining
                // payment, if not, then continue to subtract the principal from the
                // remaining balance.
                if (fixedPayment == totalBalance) {
                    balance -= payment;
                } else {
                    balance -= payment - (totalBalance - balance);            
                }
      
                i++;
            }
    
            return i;          
        }
    }
}