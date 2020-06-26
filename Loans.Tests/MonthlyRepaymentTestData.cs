using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Loans.Tests
{
   public class MonthlyRepaymentTestData
    {
        public static IEnumerable TestData()
        {
            
            yield return new TestCaseData(200_000m, 6.5m, 30, 1264.14m);
            yield return new TestCaseData(200_000m, 10m, 30, 1755.14m);
            
        }
    }
}
