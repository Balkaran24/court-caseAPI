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
using CourtCases_Balkaran.Models;

namespace CourtCases_Balkaran.Controllers
{
    public class JudgesAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/JudgesAPI
        public IQueryable<Judge> GetJudges()
        {
            return db.Judges;
        }

        // GET: api/JudgesAPI/5
        [ResponseType(typeof(Judge))]
        public IHttpActionResult GetJudge(int id)
        {
            Judge judge = db.Judges.Find(id);
            if (judge == null)
            {
                return NotFound();
            }

            return Ok(judge);
        }

        // PUT: api/JudgesAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJudge(int id, Judge judge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != judge.ID)
            {
                return BadRequest();
            }

            db.Entry(judge).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JudgeExists(id))
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

        // POST: api/JudgesAPI
        [ResponseType(typeof(Judge))]
        public IHttpActionResult PostJudge(Judge judge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Judges.Add(judge);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = judge.ID }, judge);
        }

        // DELETE: api/JudgesAPI/5
        [ResponseType(typeof(Judge))]
        public IHttpActionResult DeleteJudge(int id)
        {
            Judge judge = db.Judges.Find(id);
            if (judge == null)
            {
                return NotFound();
            }

            db.Judges.Remove(judge);
            db.SaveChanges();

            return Ok(judge);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JudgeExists(int id)
        {
            return db.Judges.Count(e => e.ID == id) > 0;
        }
    }
}