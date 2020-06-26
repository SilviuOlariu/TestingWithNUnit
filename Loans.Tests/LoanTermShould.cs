using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loans.Tests
{
    // donet tests --list-tests
    [TestFixture]
    public class LoanTermShould
    {
        private LoanTerm loanTerm;
        private ProductComparer sut;
        private List<LoanProduct> products;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //This method is called just one time before the executing of all the test classes
            //need to pay attention if any test method modify this list
            products = new List<LoanProduct>()
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3)
            };
        }

        [SetUp]
        public void SetUp()
        {
            loanTerm = new LoanTerm(1);

            sut = new ProductComparer(new LoanAmount("USD", 200_000m), products);
        }
        [TearDown]
        public void TearDown()
        {
            //this is call after every method
            //for ex you can call IDisposable();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
          //this is called one time after all test classes are executed

        }

        [Test]
        [ProductComparison]
        public void ReturnTermInMonths()
        {
            var act = loanTerm.ToMonths();

            var expect = 12;

            Assert.AreEqual(act, expect);
            //or
            Assert.That(act, Is.EqualTo(expect));
        }

        [Test]
        public void YearPropertySetOnConstructor()
        {
            Assert.That(loanTerm.Years, Is.EqualTo(1));
        }

        [Test]
        public void RespectvalueEquality()
        {
            var loanTerm2 = new LoanTerm(1);

            Assert.That(loanTerm, Is.EqualTo(loanTerm2));
        }
        [Test]
        public void RespectValueInequality()
        {
            var loanTerm2 = new LoanTerm(2);

            Assert.That(loanTerm, Is.Not.EqualTo(loanTerm2));
        }
        //for checking if 2 var points to the same obs use SameAS
        [Test]
         public void ReferenceEqualityExample1()
        {
            var loanTerm2 = loanTerm;

            Assert.That(loanTerm2, Is.SameAs(loanTerm));
        }
        [Test]
        [Ignore("No need to run this example test")]
        public void ReferenceEqualityExample2()
        {
            var loanTerm2 = new LoanTerm(1);

            Assert.That(loanTerm2, Is.SameAs(loanTerm), "object should point to the same obj");
        }

        [Test]
        public void Double()
        {
            double a = 0.2;
            double b = 0.3;

            Assert.That(a / b, Is.EqualTo(0.66).Within(5).Percent);
            Assert.That(a / b, Is.EqualTo(0.66).Within(0.03));
               
        }
        [Test]
        public void ReturnCorectNumberOfComparison()
        {
          
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Has.Exactly(3).Items);
        }
        [Test]
        public void CheckIfListIsUnique()
        {
 
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Is.Unique);
        }
        [Test]
        public void ReturnComparisonForFirstProduct()
        {
            
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));
            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);

            Assert.That(comparisons, Does.Contain(expectedProduct));
        }

        [Test]
        public void ReturnComparisonForFirstProduct_withNotKnowingAllValues()
        {
           
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));
            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);

            Assert.That(comparisons, Has.Exactly(1)
                                     .Property("ProductName")
                                     .EqualTo("a")
                                     );
        }

        [Test]
        public void NotAllowZeroYear()
        {
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>());
        }


    }

    }


