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
using ToDoListWebAPI.Contexts;
using ToDoListWebAPI.Models;

namespace ToDoListWebAPI.Controllers
{
    public class ItemsController : ApiController
    {
        private ToDoListAzureContext db = new ToDoListAzureContext();

        // GET: api/Items
        public IQueryable<Item> GetItems()
        {
            return db.Items;
        }

        // GET: api/Items/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult GetItem(Guid id)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Items/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItem(Guid id, Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.Id)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        [ResponseType(typeof(Item))]
        public IHttpActionResult PostItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items.Add(item);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ItemExists(item.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, item);
        }

        // DELETE: api/Items/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult DeleteItem(Guid id)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            db.Items.Remove(item);
            db.SaveChanges();

            return Ok(item);
        }

        // DELETE: api/Items
        [HttpDelete]
        public IHttpActionResult DeleteAllItems()
        {
            db.Items.RemoveRange(db.Items);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(Guid id)
        {
            return db.Items.Count(e => e.Id == id) > 0;
        }
    }
}