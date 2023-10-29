using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using PagedList;
using HomeworkAssignment03.Models;

namespace HomeworkAssignment03.Models
{
    public class StudentBooksViewModel
    {
        public IPagedList<students> Students { get; set; }
        public IPagedList<books> Books { get; set; }
        public int BookId { get; set; }
        public DateTime takendate { get; set; }
        public IEnumerable<borrows> Borrows { get; set; }

        public IEnumerable<types> Types { get; set; }
        public IEnumerable<authors> Authors { get; set; }


    }
}