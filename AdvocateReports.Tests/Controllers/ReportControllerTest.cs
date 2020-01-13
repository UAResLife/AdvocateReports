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
            var reports = new AdvocateAPI.AdvocateReport(new Uri("http://arizona-advocate.symplicity.com/ws/report_api.php"))
            {
                APIPassword = "",
                APIUserName = "readonly",
                sleepBetweenTries = 300,
                maxTries = 3,
                GetReportXMLRequestBody = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:q1=\"https://arizona-advocate.symplicity.com/ws/report_api.php\">\r\n   <s:Body xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" s:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">\r\n      <q1:getReportData>\r\n         <run_id xsi:type=\"xsd:string\">{RunID}</run_id>\r\n      </q1:getReportData>\r\n   </s:Body>\r\n</s:Envelope>",
                RunReportXMLRequestBody = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:q1=\"https://arizona-advocate.symplicity.com/ws/report_api.php\">\r\n   <s:Body xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" s:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">\r\n      <q1:runReport>\r\n         <report_id xsi:type=\"xsd:string\">{ReportID}</report_id>\r\n      </q1:runReport>\r\n   </s:Body>\r\n</s:Envelope>",
                CheckReportStatusXMLRequestBody = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:q1=\"https://arizona-advocate.symplicity.com/ws/report_api.php\">\r\n   <s:Body xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" s:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">\r\n      <q1:checkReportRun>\r\n         <run_id xsi:type=\"xsd:string\">{RunID}</run_id>\r\n      </q1:checkReportRun>\r\n   </s:Body>\r\n</s:Envelope>"
            };

            var data = reports.GetReport("857e4c823fd4f3e7687f36c3b03ae273");

            Assert.IsTrue(data.Count > 0);

        }
    }
}
