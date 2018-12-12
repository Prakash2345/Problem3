using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ITEMsController : ApiController
    {
        private PODb1EntitiesModel db = new PODb1EntitiesModel();

        // GET: api/ITEMs
        public IQueryable<ITEM> GetITEMs()
        {
            return db.ITEMs;
        }

        // GET: api/ITEMs/5
        [ResponseType(typeof(ITEM))]
        public IHttpActionResult GetITEM(string id)
        {
            ITEM iTEM = db.ITEMs.Find(id);
            if (iTEM == null)
            {
                return NotFound();
            }

            return Ok(iTEM);
        }

        // PUT: api/ITEMs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutITEM(ITEM iTEM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(iTEM).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ITEMExists(iTEM.ITCODE))
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

        // POST: api/ITEMs
        [ResponseType(typeof(ITEM))]
        public IHttpActionResult PostITEM(ITEM iTEM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ITEMs.Add(iTEM);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ITEMExists(iTEM.ITCODE))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = iTEM.ITCODE }, iTEM);
        }

        // DELETE: api/ITEMs/5
        [ResponseType(typeof(ITEM))]
        public IHttpActionResult DeleteITEM(string id)
        {
            ITEM iTEM = db.ITEMs.Find(id);
            if (iTEM == null)
            {
                return NotFound();
            }

            db.ITEMs.Remove(iTEM);
            db.SaveChanges();

            return Ok(iTEM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ITEMExists(string id)
        {
            return db.ITEMs.Count(e => e.ITCODE == id) > 0;
        }
    }
}