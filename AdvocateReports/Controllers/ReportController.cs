using AdvocateReports.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;

namespace AdvocateReports.Controllers
{
    /// <summary>
    /// Returns reports data from the Reporting section of Advocate. 
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
        [HttpGet]
        public JsonResult<List<Dictionary<string, string>>> Json(string Id)
        {
            var report = ReportHelper.CreateReportObject(Request.Headers);
            return Json(report.GetReportAsList(Id));
        }

        /// <summary>
        /// This command gets any report's data from Advocate by providing its ID.
        /// Credentials need to be provided in the request header as a "pwd" and "usr" headers.
        /// </summary>
        /// <param name="Id">The Advocate ID of the report, e.g. 857e4c823cd4c3e7687f88c3b03ae273</param>
        /// <returns>A string representing the Advocate report in CSV format</returns>
        [ResponseType(typeof(string))]
        [HttpGet]
        public HttpResponseMessage Csv(string Id)
        {
            var report = ReportHelper.CreateReportObject(Request.Headers);


            var result = report.GetReportAsText(Id);

            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(result, Encoding.UTF8, "text/csv");

            return res;

        }

    }
}