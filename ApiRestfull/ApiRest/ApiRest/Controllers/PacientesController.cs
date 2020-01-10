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
    builder.EntitySet<Paciente>("Pacientes");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PacientesController : ODataController
    {
        private Modelo db = new Modelo();

        // GET: odata/Pacientes
        [EnableQuery]
        public IQueryable<Paciente> GetPacientes()
        {
            return db.Pacientes;
        }

        // GET: odata/Pacientes(5)
        [EnableQuery]
        public SingleResult<Paciente> GetPaciente([FromODataUri] int key)
        {
            return SingleResult.Create(db.Pacientes.Where(paciente => paciente.IdPaciente == key));
        }

        // PUT: odata/Pacientes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Paciente> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Paciente paciente = await db.Pacientes.FindAsync(key);
            if (paciente == null)
            {
                return NotFound();
            }

            patch.Put(paciente);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(paciente);
        }

        // POST: odata/Pacientes
        public async Task<IHttpActionResult> Post(Paciente paciente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pacientes.Add(paciente);
            await db.SaveChangesAsync();

            return Created(paciente);
        }

        // PATCH: odata/Pacientes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Paciente> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Paciente paciente = await db.Pacientes.FindAsync(key);
            if (paciente == null)
            {
                return NotFound();
            }

            patch.Patch(paciente);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(paciente);
        }

        // DELETE: odata/Pacientes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Paciente paciente = await db.Pacientes.FindAsync(key);
            if (paciente == null)
            {
                return NotFound();
            }

            db.Pacientes.Remove(paciente);
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

        private bool PacienteExists(int key)
        {
            return db.Pacientes.Count(e => e.IdPaciente == key) > 0;
        }
    }
}
