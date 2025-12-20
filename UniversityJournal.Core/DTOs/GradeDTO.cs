using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.DTOs
{
    public class GradeDTO
    {
        public Guid GradeId { get; set; }      
        public Guid StudentId { get; set; }    
        public Guid SubjectId { get; set; }    
        public int LabNumber { get; set; }     
        public int Score { get; set; }         
        public DateTime Date { get; set; }     
        public string Comment { get; set; }    
        public GradeDTO(Grade grade)
        {
            GradeId =grade.GradeId;
            StudentId =grade.StudentId;
            SubjectId =grade.SubjectId;
            LabNumber =grade.LabNumber;
            Score =grade.Score;
            Date =grade.Date;
            Comment =grade.Comment;
        }
    }
}
