using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MagniFinanceExercise.Models
{
    public class Grade
    {
        [Key, Column(Order = 0)]
        public double Value { get; set; }
    }
}