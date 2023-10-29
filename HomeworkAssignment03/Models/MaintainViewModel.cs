using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace HomeworkAssignment03.Models
{
    public class MaintainViewModel
    {
        public IEnumerable<students> Students { get; set; }
        public IEnumerable<books> Books { get; set; }
        public IPagedList<authors> Authors { get; set; }
        public IPagedList<types> Types { get; set; }
        public IPagedList<borrows> Borrows { get; set; }

    }
}