using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeworkAssignment03.Models;
using PagedList.Mvc;
using PagedList;

namespace HomeworkAssignment03.Controllers
{
    public class typesController : Controller
    {
        private LibraryEntities2 db = new LibraryEntities2();

        // GET: types
        public async Task<ActionResult> Index()
        {
            return View(await db.types.ToListAsync());
        }

        // GET: types/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            types types = await db.types.FindAsync(id);
            if (types == null)
            {
                return HttpNotFound();
            }
            return View(types);
        }

        // GET: types/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: types/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "typeId,name")] types types)
        {
            if (ModelState.IsValid)
            {
                db.types.Add(types);
                await db.SaveChangesAsync();
                return RedirectToAction("Maintain","Home");
            }

            return View(types);
        }

        // GET: types/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            types types = await db.types.FindAsync(id);
            if (types == null)
            {
                return HttpNotFound();
            }
            return View(types);
        }

        // POST: types/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "typeId,name")] types types)
        {
            if (ModelState.IsValid)
            {
                db.Entry(types).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Maintain", "Home");
            }
            return View(types);
        }

        // GET: types/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            types types = await db.types.FindAsync(id);
            if (types == null)
            {
                return HttpNotFound();
            }
            return View(types);
        }

        // POST: types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            types types = await db.types.FindAsync(id);
            db.types.Remove(types);
            await db.SaveChangesAsync();
            return RedirectToAction("Maintain", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
