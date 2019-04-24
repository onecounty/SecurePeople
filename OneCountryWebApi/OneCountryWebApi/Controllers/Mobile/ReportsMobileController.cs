using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OneCountry.Data.Base;
using OneCountry.Data.MobileOnly;
using OneCountryWebApi.Interfaces;
using OneCountryWebApi.Models;
using OneCountryWebApi.Repository;

namespace OneCountryWebApi.Controllers.Mobile
{
    [Authorize(Roles = "Public")]
    public class ReportsMobileController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IMobileOperations _repo  { get; set;}
        private static ILog _log = LogManager.GetLogger(typeof(ReportsMobileController));
        private ApplicationUser _user { get; set; }
        private ApplicationUserManager _userManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ReportsMobileController()
        {
            _repo = new MobileOperationsRepository();
        }

        // GET: one10/ReportsMobile
        [ResponseType(typeof(IEnumerable<Report>))]
        public async Task<IHttpActionResult> GetReports()
        {
            _user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            try
            {
                
                return Ok(new BaseResponseModel<List<MyReportListItem>>() {
                    IsSucess=true,
                    RequestedUserMobile= _user.PhoneNumber,
                    Results = _repo.GetAllMyReports(_user.Id)
                });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Ok(new BaseResponseModel<List<MyReportListItem>>()
                {
                    IsSucess = true,
                    RequestedUserMobile = _user.PhoneNumber,
                    Exception = new Error(2001,"Processing Error")
                });
            }           
        }

        // GET: one10/ReportsMobile/5
        [ResponseType(typeof(MyReports))]
        public async Task<IHttpActionResult> GetReport(int id)
        {
            _user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            try
            {

                return Ok(new BaseResponseModel<MyReports>()
                {
                    IsSucess = true,
                    RequestedUserMobile = _user.PhoneNumber,
                    Results = _repo.GetMyReportDetail(id,_user.Id)
                });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Ok(new BaseResponseModel<MyReports>()
                {
                    IsSucess = true,
                    RequestedUserMobile = _user.PhoneNumber,
                    Exception = new Error(2001, "Processing Error")
                });
            }
        }

        // POST: one10/ReportsMobile
        [ResponseType(typeof(MyReportUpload))]
        public async Task<IHttpActionResult> PostReport(MyReportUpload report)
        {
            _user = await _userManager.FindByIdAsync(User.Identity.GetUserId());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                report.MobileNumber = _user.PhoneNumber;
                string path = HttpContext.Current.Server.MapPath("~/Images");
                var value = await _repo.CreateReport(report, _user.Id, path);

                if (value.Id == 0 ) throw new Exception("Db Create error");

                return CreatedAtRoute("DefaultApi", new
                {
                    id = value.Id
                } ,new BaseResponseModel<MyReportUpload>()
                    {
                        IsSucess = true,
                        RequestedUserMobile = _user.PhoneNumber,
                        Results = value

                    });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Ok(new BaseResponseModel<MyReports>()
                {
                    IsSucess = true,
                    RequestedUserMobile = _user.PhoneNumber,
                    Exception = new Error(2001, "Processing Error")
                });
            }
            
        }

        // DELETE: one10/ReportsMobile/5
        [ResponseType(typeof(Report))]
        public async Task<IHttpActionResult> DeleteReport(int id)
        {
            _user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            try
            {
                var status = await _repo.DeleteReport(id, _user.Id);
                return Ok(new BaseResponseModel<bool>()
                {
                    IsSucess = true,
                    RequestedUserMobile = _user.PhoneNumber,
                    Results = status

            });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Ok(new BaseResponseModel<MyReports>()
                {
                    IsSucess = true,
                    RequestedUserMobile = _user.PhoneNumber,
                    Exception = new Error(2001, "Processing Error")
                });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReportExists(int id)
        {
            return db.Reports.Count(e => e.ReportId == id) > 0;
        }
    }
}