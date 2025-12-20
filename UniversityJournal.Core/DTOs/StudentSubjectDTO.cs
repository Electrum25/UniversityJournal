using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.DTOs
{
    public class StudentSubjectDTO
    {
        public Guid StudentId { get; set; }    
        public Guid SubjectId { get; set; }    
        public decimal? FinalScore { get; set; } 
        public string FinalGrade { get; set; } 
        public StudentSubjectDTO(StudentSubject studentSubject) 
        {
            StudentId = studentSubject.StudentId;
            SubjectId = studentSubject.SubjectId;
            FinalScore = studentSubject.FinalScore;
            FinalGrade = studentSubject.FinalGrade;
        }
    }
}
