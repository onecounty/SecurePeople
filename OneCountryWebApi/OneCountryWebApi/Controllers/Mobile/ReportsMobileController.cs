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
    public class ReportsMobileController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IMobileOperations _repo = new MobileOperationsRepository();
        private static ILog _log = LogManager.GetLogger(typeof(ReportsMobileController));

        private ApplicationUserManager _userManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        // GET: one10/ReportsMobile
        [ResponseType(typeof(IEnumerable<Report>))]
        public async Task<IHttpActionResult> GetReports()
        {
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            try
            {
                
                return Ok(new BaseResponseModel<List<MyReportListItem>>() {
                    IsSucess=true,
                    RequestedUserMobile= user.PhoneNumber,
                    Results = _repo.GetAllMyReports(user.Id)
                });
            }
            catch (Exception ex)
            {

                return Ok(new BaseResponseModel<List<MyReportListItem>>()
                {
                    IsSucess = true,
                    RequestedUserMobile = user.PhoneNumber,
                    Exception = new Error(2001,"Processing Error")
                });
            }
           
        }

        // GET: one10/ReportsMobile/5
        [ResponseType(typeof(Report))]
        public async Task<IHttpActionResult> GetReport(int id)
        {
            Report report = await db.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        // PUT: one10/ReportsMobile/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReport(int id, Report report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != report.ReportId)
            {
                return BadRequest();
            }

            db.Entry(report).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: one10/ReportsMobile
        [ResponseType(typeof(Report))]
        public async Task<IHttpActionResult> PostReport(Report report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reports.Add(report);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = report.ReportId }, report);
        }

        // DELETE: one10/ReportsMobile/5
        [ResponseType(typeof(Report))]
        public async Task<IHttpActionResult> DeleteReport(int id)
        {
            Report report = await db.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            db.Reports.Remove(report);
            await db.SaveChangesAsync();

            return Ok(report);
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