using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using HomeworkAssignment03.Models;
using System.Data.Entity;
using System.Net.NetworkInformation;
using System.Net;
using System.Web.UI;
using System.Web.Services.Description;
using System.IO;
using System.Text;

namespace HomeworkAssignment03.Controllers
{
    public class HomeController : Controller
    {

        private LibraryEntities2 db = new LibraryEntities2();
        private const int PageSize = 10;


        public async Task<ActionResult> Index(int? page)
        {
            var viewModel = new StudentBooksViewModel
            {
                Authors = await db.authors.ToListAsync(),
                Types = await db.types.ToListAsync()
            };

            int pageNumber = page ?? 1;
            int pageSize = 10; // Set your desired page size here.

            // Paginate the students data.
            var studentsQuery = db.students.OrderBy(s => s.studentId).AsQueryable();
            var studentsPagedList = studentsQuery.ToPagedList(pageNumber, pageSize);
            viewModel.Students = studentsPagedList;

            // Paginate the books data.
            var booksQuery = db.books
                .Include(b => b.authors)
                .Include(b => b.types)
                .OrderBy(b => b.bookId)
                .AsQueryable();
            var booksPagedList = booksQuery.ToPagedList(pageNumber, pageSize);
            viewModel.Books = booksPagedList;

            return View(viewModel);
        }

        public async Task<ActionResult> BooksIndex()
        {
            var books = db.books.Include(t => t.authors).Include(t => t.types);
            return View(await books.ToListAsync());
        }

        //Books
        public async Task<ActionResult> BooksDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            books books = await db.books.FindAsync(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }
        public ActionResult BooksCreate()
        {
            ViewBag.authorId = new SelectList(db.authors, "authorId", "name");
            ViewBag.typeId = new SelectList(db.types, "typeId", "name");
            return View();
        }

        // POST: books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BooksCreate([Bind(Include = "bookId,name,pagecount,point,authorId,typeId")] books books)
        {
            if (ModelState.IsValid)
            {
                db.books.Add(books);
                await db.SaveChangesAsync();
                return RedirectToAction("BooksIndex");
            }

            ViewBag.authorId = new SelectList(db.authors, "authorId", "name", books.authorId);
            ViewBag.typeId = new SelectList(db.types, "typeId", "name", books.typeId);
            return View(books);
        }


        public async Task<ActionResult> StudentsIndex()
        {
            return View(await db.students.ToListAsync());
        }

        //Students
        public async Task<ActionResult> StudentsDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            students students = await db.students.FindAsync(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }
        public ActionResult StudentsCreate()
        {
            return View();
        }

        // POST: students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StudentsCreate([Bind(Include = "studentId,name,surname,birthdate,gender,class,point")] students students)
        {
            if (ModelState.IsValid)
            {
                db.students.Add(students);
                await db.SaveChangesAsync();
                return RedirectToAction("StudentsIndex");
            }

            return View(students);
        }

        public async Task<ActionResult> Maintain(int? page)
        {

            var viewModel = new MaintainViewModel
            {
                //authors = await db.authors.ToListAsync(),
                //types = await db.types.ToListAsync(),
                //borrows = await db.borrows.ToListAsync(),
                Students = await db.students.ToListAsync(),
                Books = await db.books.ToListAsync()
            };

            int pageNumber = page ?? 1;
            int pageSize = 10; // Set your desired page size here.

            // Paginate the authors data.
            var authorsQuery = db.authors.OrderBy(s => s.authorId).AsQueryable();
            var authorPagedList = authorsQuery.ToPagedList(pageNumber, pageSize);
            viewModel.Authors = authorPagedList;

            // Paginate the types data.
            var typesQuery = db.types.OrderBy(s => s.typeId).AsQueryable();
            var typesPagedList = typesQuery.ToPagedList(pageNumber, pageSize);
            viewModel.Types = typesPagedList;

            // Paginate the types data.
            var borrowsQuery = db.borrows
               .Include(b => b.books)
               .OrderBy(b => b.borrowId)
               .AsQueryable();
            var borrowsPagedList = borrowsQuery.ToPagedList(pageNumber, pageSize);
            viewModel.Borrows = borrowsPagedList;

            return View(viewModel);
        }

        public ActionResult Report()
        {
            var popularBooks = db.borrows
                .GroupBy(b => b.bookId)
                .Select(g => new ReportViewModel
                {
                    BookName = g.FirstOrDefault().books.name,
                    BorrowCount = g.Count()
                })
                .OrderByDescending(r => r.BorrowCount)
                .ToList();

            return View(popularBooks);
        }



       


    }
}