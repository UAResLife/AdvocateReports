using AdvocateAPI;
using AdvocateAPI.Common;
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
        /// <param name="UserAndPassword">User and password separated by a colom (:)</param>
        /// <param name="ByPassCache">If true, the memory cache will not be used</param>
        /// <returns>An Advocate report object</returns>
        public static AdvocateReport CreateReportObject(string UserAndPassword, bool BypassCache = false)
        {
            var credentials = UserAndPassword.Split(':');

            var username = credentials[0];
            var password = credentials[1];

            var AdvocateApiURL = ConfigHelper.GetStringValue("AdvocateApiURL");
            var CacheExpirationHours = ConfigHelper.GetIntValue("CacheExpirationHours");



            var reports = new AdvocateReport(new Uri(AdvocateApiURL))
            {
                APIPassword = password,
                APIUserName = username,
                BypassCache = BypassCache,
                CacheExpirationHours = CacheExpirationHours,
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