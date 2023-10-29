using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeworkAssignment03.Models
{
    public class ReportViewModel
    {
        
        public int? BookId { get; set; }
        public string BookName { get; set; }
        public int BorrowCount { get; set; }
    }
}