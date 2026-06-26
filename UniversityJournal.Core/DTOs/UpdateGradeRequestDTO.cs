using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityJournal.Core.DTOs
{
    public class UpdateGradeRequest
    {
        public Guid GradeId { get; set; } 
        public int Score { get; set; }
        public string Comment { get; set; }
    }
}
