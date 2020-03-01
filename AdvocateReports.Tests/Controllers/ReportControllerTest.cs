using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdvocateAPI;

namespace AdvocateReports.Tests.Controllers
{
    [TestClass]
    public class ReportControllerTest
    {
        [TestMethod]
        public void GetReport()
        {
            var report = TestsHelper.CreateAdvocateReport("readonly", "");
            var data = report.GetReportAsList("857e4c823fd4f3e7687f36c3b03ae273");
            Assert.IsTrue(data.Count > 0);
        }


    }



}
