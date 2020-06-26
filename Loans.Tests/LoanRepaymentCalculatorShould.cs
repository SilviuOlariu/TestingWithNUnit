using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Loans.Tests
{
    public class LoanRepaymentCalculatorShould
    {
        [Test]
        [TestCase(200_000, 6.5, 30, 1264.14)]
        [TestCase(200_000, 10, 30, 1755.14)]
        public void CalculateCorrectMonthlyRepayment(decimal principal, decimal interestRate, int term, decimal expectedValue)
        {
            var loan = new LoanRepaymentCalculator();
            var act = loan.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(term));

            Assert.That(act, Is.EqualTo(expectedValue));
        }

        [Test]
        [TestCase(200_000, 6.5, 30, ExpectedResult = 1264.14)]
        [TestCase(200_000, 10, 30, ExpectedResult = 1755.14)]
        public decimal CalculateCorrectMonthlyRepayment_Simplified(decimal principal, decimal interestRate, int term)
        {
            var loan = new LoanRepaymentCalculator();
            double act = (double)loan.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(term));

            return new decimal(Math.Round(act, 2, MidpointRounding.AwayFromZero));
        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestData), "TestData")]
        public void CalculateCorrectMonthlyRepayment_Concrete(decimal principal, decimal interestRate, int term, decimal expectedValue)
        {
            var loan = new LoanRepaymentCalculator();
            var act = loan.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(term));

            Assert.That(act, Is.EqualTo(expectedValue));
        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestDataWithReturn), "TestData")]
        public decimal CalculateCorrectMonthlyRepayment_return(decimal principal, decimal interestRate, int term)
        {
            var loan = new LoanRepaymentCalculator();
            double act = (double)loan.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(term));

            return new decimal(Math.Round(act, 2, MidpointRounding.AwayFromZero));
        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestDataFromCSV), "GetTestData", new object[] { "Data.csv" })]
        public void CalculateCorrectMonthlyRepayment_csv(decimal principal, decimal interestRate, int term, decimal expectedValue)
        {
            var loan = new LoanRepaymentCalculator();
            var act = loan.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(term));

            Assert.That(act, Is.EqualTo(expectedValue));
        }

        [Test]

        public void CalculateCorrectMonthlyRepayment_GenerateTestData(
            [Values(100000, 200000)] decimal principal,
           [Values(5, 6)]decimal interestRate,
           [Values(20, 30)]int term)
        {
            var loan = new LoanRepaymentCalculator();
            var act = loan.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(term));
        }

        [Test]
        [Sequential]
        public void CalculateCorrectMonthlyRepayment_Sequential(
                   [Values(200_000, 200_000, 500_000)]decimal principal,
                   [Values(6.5, 10, 10)]decimal interestRate,
                   [Values(30, 30, 30)]int termInYears,
                   [Values(1264.14, 1755.14, 4387.86)]decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(
                new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        [Test]
        public void CalculateCorrectMonthlyRepayment_Range(
            [Range(50_000, 1_000_000, 50_000)]decimal principal,
            [Range(0.5, 20.00, 0.5)]decimal interestRate,
            [Values(10, 20, 30)]int termInYears)
        {
            var sut = new LoanRepaymentCalculator();

            sut.CalculateMonthlyRepayment(
                new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
