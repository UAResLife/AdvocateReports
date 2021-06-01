using AdvocateReports.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Xml;
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
        /// <param name="BypassCache">If true, the memory cache is not used.</param>
        /// <returns>A json object with the report's data</returns>
        /// 
        [ResponseType(typeof(List<Dictionary<string, string>>))]
        [HttpGet]
        [BasicAuthentication]
        public JsonResult<List<Dictionary<string, string>>> Json(string Id, bool BypassCache = false)
        {
            var report = ReportHelper.CreateReportObject(Thread.CurrentPrincipal.Identity.Name, BypassCache);
            return Json(report.GetReportAsList(Id));
        }

        /// <summary>
        /// This command gets any report's data from Advocate by providing its ID.
        /// Credentials need to be provided in the request header as a "pwd" and "usr" headers.
        /// </summary>
        /// <param name="Id">The Advocate ID of the report, e.g. 857e4c823cd4c3e7687f88c3b03ae273</param>
        /// <param name="BypassCache">If true, the memory cache is not used.</param>
        /// <returns>An XML object with the report's data</returns>
        /// 
        [ResponseType(typeof(XmlDocument))]
        [HttpGet]
        [BasicAuthentication]
        public HttpResponseMessage Xml(string Id, bool BypassCache = false)
        {
            var report = ReportHelper.CreateReportObject(Thread.CurrentPrincipal.Identity.Name, BypassCache);
            var data = report.GetReportAsXml(Id);
            return new HttpResponseMessage() { Content = new StringContent(data.InnerXml, Encoding.UTF8, "application/xml") };
        }

        /// <summary>
        /// This command gets any report's data from Advocate by providing its ID.
        /// Credentials need to be provided in the request header as a "pwd" and "usr" headers.
        /// </summary>
        /// <param name="Id">The Advocate ID of the report, e.g. 857e4c823cd4c3e7687f88c3b03ae273</param>
        /// <param name="BypassCache">If true, the memory cache is not used.</param>
        /// <returns>A string representing the Advocate report in CSV format</returns>
        [ResponseType(typeof(string))]
        [HttpGet]
        [BasicAuthentication]
        public HttpResponseMessage Csv(string Id, bool BypassCache = false)
        {
            var report = ReportHelper.CreateReportObject(Thread.CurrentPrincipal.Identity.Name, BypassCache);


            var result = report.GetReportAsText(Id);

            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(result, Encoding.UTF8, "text/csv");

            return res;

        }

    }
}