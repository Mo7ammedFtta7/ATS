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
using System.Web.Http.Description;
using SmartHup.Models;

namespace SmartHup.Controllers
{
    public class NotifController : ApiController
    {
        private SMARTEntities db = new SMARTEntities();

        // GET: api/Notif
        public IQueryable<MONITORINGLOG> GetMONITORINGLOG()
        {
            return db.MONITORINGLOG;
        }

        // GET: api/Notif/5
        [ResponseType(typeof(MONITORINGLOG))]
        public async Task<IHttpActionResult> GetMONITORINGLOG(long id)
        {
            MONITORINGLOG mONITORINGLOG = await db.MONITORINGLOG.FindAsync(id);
            if (mONITORINGLOG == null)
            {
                return NotFound();
            }

            return Ok(mONITORINGLOG);
        }

        // PUT: api/Notif/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMONITORINGLOG(long id, MONITORINGLOG mONITORINGLOG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mONITORINGLOG.systemId)
            {
                return BadRequest();
            }

            db.Entry(mONITORINGLOG).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MONITORINGLOGExists(id))
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

        // POST: api/Notif
        [ResponseType(typeof(MONITORINGLOG))]
        public async Task<IHttpActionResult> PostMONITORINGLOG(MONITORINGLOG mONITORINGLOG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MONITORINGLOG.Add(mONITORINGLOG);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MONITORINGLOGExists(mONITORINGLOG.systemId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = mONITORINGLOG.systemId }, mONITORINGLOG);
        }

        // DELETE: api/Notif/5
        [ResponseType(typeof(MONITORINGLOG))]
        public async Task<IHttpActionResult> DeleteMONITORINGLOG(long id)
        {
            MONITORINGLOG mONITORINGLOG = await db.MONITORINGLOG.FindAsync(id);
            if (mONITORINGLOG == null)
            {
                return NotFound();
            }

            db.MONITORINGLOG.Remove(mONITORINGLOG);
            await db.SaveChangesAsync();

            return Ok(mONITORINGLOG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MONITORINGLOGExists(long id)
        {
            return db.MONITORINGLOG.Count(e => e.systemId == id) > 0;
        }
    }
}