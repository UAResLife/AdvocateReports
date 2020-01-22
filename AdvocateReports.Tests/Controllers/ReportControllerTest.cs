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


        [TestMethod]
        public void OnlyGetData()
        {
            var report = TestsHelper.CreateAdvocateReport("readonly", "");
            var xmlData = report.GetReportData("4a94dc89a2b1e0859e809f74dcfeb0a4");
            var stringData = xmlData.InnerText;
            var formattedData = Utilities.CSVToList(stringData);
            Assert.IsTrue(formattedData.Count == 14639 && formattedData[0].Count == 35);
        }

    }



}
