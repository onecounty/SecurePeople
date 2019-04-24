using log4net;
using OneCountry.Data.Base;
using OneCountry.Data.MobileOnly;
using OneCountryWebApi.Interfaces;
using OneCountryWebApi.Models;
using OneCountryWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace OneCountryWebApi.Controllers
{
    [Authorize(Roles = "OneCountry")]
    public class ReportsController : ApiController
    {
        private IAdmin _repo { get; set; }
        private static ILog _log = LogManager.GetLogger(typeof(ReportsController));
       
        public ReportsController()
        {
            _repo = new AdminRepository();
        }

        // GET: one10/Reports/{location}/{actionId}/{description}/{page}/{itemsPerPage}
        [ResponseType(typeof(IEnumerable<Report>))]
        public async Task<IHttpActionResult> GetReports(string location, int actionId, string description,int page,int itemsPerPage)
        {
            try
            {

                return Ok(new BaseResponseModel<List<Report>>()
                {
                    IsSucess = true,
                    RequestedUserMobile = null,
                    Results = _repo.GetReports(location,actionId,description, page,itemsPerPage)
                });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Ok(new BaseResponseModel<List<MyReportListItem>>()
                {
                    IsSucess = true,
                    RequestedUserMobile = null,
                    Exception = new Error(2001, "Processing Error")
                });
            }
        }

        //// GET: one10/ReportsMobile/5
        //[ResponseType(typeof(MyReports))]
        //public async Task<IHttpActionResult> GetReport(int id)
        //{
        //    try
        //    {

        //        return Ok(new BaseResponseModel<MyReports>()
        //        {
        //            IsSucess = true,
        //            RequestedUserMobile = null,
        //            Results = _repo.GetMyReportDetail(id, 1)
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error(ex);
        //        return Ok(new BaseResponseModel<MyReports>()
        //        {
        //            IsSucess = true,
        //            RequestedUserMobile = null,
        //            Exception = new Error(2001, "Processing Error")
        //        });
        //    }
        //}

        //// POST: one10/ReportsMobile
        //[ResponseType(typeof(MyReportUpload))]
        //public async Task<IHttpActionResult> PostReport(MyReportUpload report)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        report.MobileNumber = null;
        //        var value = await _repo.CreateReport(report, 1, "");

        //        if (value.Id == 0) throw new Exception("Db Create error");

        //        return CreatedAtRoute("DefaultApi", new
        //        {
        //            id = value.Id
        //        }, new BaseResponseModel<MyReportUpload>()
        //        {
        //            IsSucess = true,
        //            RequestedUserMobile = null,
        //            Results = value

        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error(ex);
        //        return Ok(new BaseResponseModel<MyReports>()
        //        {
        //            IsSucess = true,
        //            RequestedUserMobile = null,
        //            Exception = new Error(2001, "Processing Error")
        //        });
        //    }

        //}

        //// DELETE: one10/ReportsMobile/5
        //[ResponseType(typeof(Report))]
        //public async Task<IHttpActionResult> DeleteReport(int id)
        //{
        //    try
        //    {
        //        var status = await _repo.DeleteReport(id, 1);
        //        return Ok(new BaseResponseModel<bool>()
        //        {
        //            IsSucess = true,
        //            RequestedUserMobile = null,
        //            Results = status

        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error(ex);
        //        return Ok(new BaseResponseModel<MyReports>()
        //        {
        //            IsSucess = true,
        //            RequestedUserMobile = null,
        //            Exception = new Error(2001, "Processing Error")
        //        });
        //    }
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool ReportExists(int id)
        //{
        //    return db.Reports.Count(e => e.ReportId == id) > 0;
        //}
    }
}
