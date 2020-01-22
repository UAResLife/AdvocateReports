using AdvocateAPI;
using System;
using System.Linq;
using System.Net.Http.Headers;

namespace AdvocateReports.Models
{
    /// <summary>
    /// This class helps with misc methods to support the Reports creation
    /// </summary>
    public static class ReportHelper
    {
        /// <summary>
        /// This method creates the Advocate report object
        /// </summary>
        /// <param name="headers">The headers object in the HTTP request</param>
        /// <returns>An Advocate report object</returns>
        public static AdvocateReport CreateReportObject(HttpRequestHeaders headers)
        {
            if (!headers.Contains("usr") | !headers.Contains("pwd")) throw new Exception("No credentials were provided");


            var password = headers.GetValues("pwd").FirstOrDefault();
            var username = headers.GetValues("usr").FirstOrDefault();

            var reports = new AdvocateReport(new Uri("http://arizona-advocate.symplicity.com/ws/report_api.php"))
            {
                APIPassword = password,
                APIUserName = username,
                sleepBetweenTries = 300,
                maxTries = 3,
                GetReportXMLRequestBody = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:q1=\"https://arizona-advocate.symplicity.com/ws/report_api.php\">\r\n   <s:Body xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" s:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">\r\n      <q1:getReportData>\r\n         <run_id xsi:type=\"xsd:string\">{RunID}</run_id>\r\n      </q1:getReportData>\r\n   </s:Body>\r\n</s:Envelope>",
                RunReportXMLRequestBody = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:q1=\"https://arizona-advocate.symplicity.com/ws/report_api.php\">\r\n   <s:Body xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" s:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">\r\n      <q1:runReport>\r\n         <report_id xsi:type=\"xsd:string\">{ReportID}</report_id>\r\n      </q1:runReport>\r\n   </s:Body>\r\n</s:Envelope>",
                CheckReportStatusXMLRequestBody = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:q1=\"https://arizona-advocate.symplicity.com/ws/report_api.php\">\r\n   <s:Body xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" s:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">\r\n      <q1:checkReportRun>\r\n         <run_id xsi:type=\"xsd:string\">{RunID}</run_id>\r\n      </q1:checkReportRun>\r\n   </s:Body>\r\n</s:Envelope>"
            };

            return reports;
        }
    }
}