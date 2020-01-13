using AdvocateAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace AdvocateReports.Controllers
{
    /// <summary>
    /// Some help for this controller
    /// </summary>
    public class ReportController : ApiController
    {

        /// <summary>
        /// This command gets any report's data from Advocate by providing its ID.
        /// Credentials need to be provided in the request header as a "pwd" and "usr" headers.
        /// </summary>
        /// <param name="Id">The Advocate ID of the report, e.g. 857e4c823cd4c3e7687f88c3b03ae273</param>
        /// <returns>A json object with the report's data</returns>
        /// 
        [ResponseType(typeof(List<Dictionary<string, string>>))]
        public JsonResult<List<Dictionary<string, string>>> Get(string Id)
        {
            if (!Request.Headers.Contains("usr") | !Request.Headers.Contains("pwd")) throw new Exception("No credentials were provided");


            var password = Request.Headers.GetValues("pwd").FirstOrDefault();
            var username = Request.Headers.GetValues("usr").FirstOrDefault();

            

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

            return Json(reports.GetReport(Id));
        }
    }
}