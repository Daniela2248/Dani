using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using ApiRest.Models;

namespace ApiRest.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using ApiRest.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Doctor>("Doctors");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class DoctorsController : ODataController
    {
        private Modelo db = new Modelo();

        // GET: odata/Doctors
        [EnableQuery]
        public IQueryable<Doctor> GetDoctors()
        {
            return db.Doctors;
        }

        // GET: odata/Doctors(5)
        [EnableQuery]
        public SingleResult<Doctor> GetDoctor([FromODataUri] int key)
        {
            return SingleResult.Create(db.Doctors.Where(doctor => doctor.IDdoctor == key));
        }

        // PUT: odata/Doctors(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Doctor> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Doctor doctor = await db.Doctors.FindAsync(key);
            if (doctor == null)
            {
                return NotFound();
            }

            patch.Put(doctor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(doctor);
        }

        // POST: odata/Doctors
        public async Task<IHttpActionResult> Post(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Doctors.Add(doctor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DoctorExists(doctor.IDdoctor))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(doctor);
        }

        // PATCH: odata/Doctors(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Doctor> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Doctor doctor = await db.Doctors.FindAsync(key);
            if (doctor == null)
            {
                return NotFound();
            }

            patch.Patch(doctor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(doctor);
        }

        // DELETE: odata/Doctors(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Doctor doctor = await db.Doctors.FindAsync(key);
            if (doctor == null)
            {
                return NotFound();
            }

            db.Doctors.Remove(doctor);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DoctorExists(int key)
        {
            return db.Doctors.Count(e => e.IDdoctor == key) > 0;
        }
    }
}
