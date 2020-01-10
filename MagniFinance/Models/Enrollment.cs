using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MagniFinanceExercise.Models
{
    public class Enrollment
    {
        [Key, Column(Order=0)]
        public int SubjectID { get; set; }
        [Key, Column(Order = 1)]
        public int StudentID { get; set; }

        [Key, Column(Order = 2)]
        public int Year { get; set; }
        public double GradeValue { get; set; }

        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Grade Grade { get; set; }
    }
}