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
    public class PODETAILsController : ApiController
    {
        private PODb1EntitiesModel db = new PODb1EntitiesModel();

        // GET: api/PODETAILs
        public IQueryable<PODETAIL> GetPODETAILs()
        {
            return db.PODETAILs;
        }

        // GET: api/PODETAILs/5
        [ResponseType(typeof(PODETAIL))]
        public IHttpActionResult GetPODETAIL(string id)
        {
            PODETAIL pODETAIL = db.PODETAILs.Where(x=>x.PONO == id).FirstOrDefault();
            if (pODETAIL == null)
            {
                return NotFound();
            }

            return Ok(pODETAIL);
        }

        // PUT: api/PODETAILs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPODETAIL(PODETAIL pODETAIL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(pODETAIL).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PODETAILExists(pODETAIL.PONO))
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

        // POST: api/PODETAILs
        [ResponseType(typeof(PODETAIL))]
        public IHttpActionResult PostPODETAIL(PODETAIL pODETAIL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PODETAILs.Add(pODETAIL);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PODETAILExists(pODETAIL.PONO))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pODETAIL.PONO }, pODETAIL);
        }

        // DELETE: api/PODETAILs/5
        [ResponseType(typeof(PODETAIL))]
        public IHttpActionResult DeletePODETAIL(string id)
        {
            PODETAIL pODETAIL = db.PODETAILs.Where(x => x.PONO == id).FirstOrDefault();
            if (pODETAIL == null)
            {
                return NotFound();
            }

            db.PODETAILs.Remove(pODETAIL);
            db.SaveChanges();

            return Ok(pODETAIL);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PODETAILExists(string id)
        {
            return db.PODETAILs.Count(e => e.PONO == id) > 0;
        }
    }
}