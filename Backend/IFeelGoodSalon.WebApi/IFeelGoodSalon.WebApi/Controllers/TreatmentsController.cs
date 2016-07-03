using IFeelGoodSalon.BusinessLogic;
using IFeelGoodSalon.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace IFeelGoodSalon.WebApi.Controllers
{
    public class TreatmentsController : ApiController
    {
        private readonly ITreatmentService _businessService;

        public TreatmentsController(ITreatmentService businessService)
        {
            this._businessService = businessService;
        }

        // GET: api/Treatments
        public IQueryable<Treatment> GetTreatments()
        {
            return this._businessService.Queryable();
        }

        // GET: api/Courses/5
        [ResponseType(typeof(Treatment))]
        public async Task<IHttpActionResult> GetTreatment(int id)
        {
            Treatment course = await this._businessService.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // PUT: api/Treatments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTreatment(int id, Treatment treatment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != treatment.Id)
            {
                return BadRequest();
            }

            try
            {
                await _businessService.UpdateAsync(treatment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreatmentExists(id))
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

        // POST: api/Treatments
        [ResponseType(typeof(Treatment))]
        public async Task<IHttpActionResult> PostTreatment(Treatment treatment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _businessService.InsertAsync(treatment);
            }
            catch (DbUpdateException)
            {
                if (TreatmentExists(treatment.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = treatment.Id }, treatment);
        }

        // DELETE: api/Treatment/5
        [ResponseType(typeof(Treatment))]
        public async Task<IHttpActionResult> DeleteTreatment(int id)
        {
            Treatment treatment = await this._businessService.FindAsync(id);
            if (treatment == null)
            {
                return NotFound();
            }

            try
            {
                await this._businessService.DeleteAsync(treatment);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok(treatment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._businessService.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool TreatmentExists(int id)
        {
            return this._businessService.Queryable().Count(treatment => treatment.Id == id) > 0;
        }
    }
}