using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Loans.Tests
{
  public class MonthlyRepaymentTestDataFromCSV
    {
        public static IEnumerable GetTestData(string fileName)
        {
           

            var data = new List<TestCaseData>();

            var lines = File.ReadAllLines(fileName);
            foreach(var item in lines)
            {
                string[] values = item.Replace(" ", "").Split(',');
                decimal principal = decimal.Parse(values[0]);
                decimal interestRate = decimal.Parse(values[1]);
                int loanTerm = int.Parse(values[2]);
                decimal result = decimal.Parse(values[3]);

                data.Add(new TestCaseData(principal, interestRate, loanTerm, result));
            }

            return data;
        }
    }
}
