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
    builder.EntitySet<Cita>("Citas");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CitasController : ODataController
    {
        private Modelo db = new Modelo();

        // GET: odata/Citas
        [EnableQuery]
        public IQueryable<Cita> GetCitas()
        {
            return db.Citas;
        }

        // GET: odata/Citas(5)
        [EnableQuery]
        public SingleResult<Cita> GetCita([FromODataUri] int key)
        {
            return SingleResult.Create(db.Citas.Where(cita => cita.Idcita == key));
        }

        // PUT: odata/Citas(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Cita> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cita cita = await db.Citas.FindAsync(key);
            if (cita == null)
            {
                return NotFound();
            }

            patch.Put(cita);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(cita);
        }

        // POST: odata/Citas
        public async Task<IHttpActionResult> Post(Cita cita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Citas.Add(cita);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CitaExists(cita.Idcita))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(cita);
        }

        // PATCH: odata/Citas(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Cita> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cita cita = await db.Citas.FindAsync(key);
            if (cita == null)
            {
                return NotFound();
            }

            patch.Patch(cita);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(cita);
        }

        // DELETE: odata/Citas(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Cita cita = await db.Citas.FindAsync(key);
            if (cita == null)
            {
                return NotFound();
            }

            db.Citas.Remove(cita);
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

        private bool CitaExists(int key)
        {
            return db.Citas.Count(e => e.Idcita == key) > 0;
        }
    }
}
