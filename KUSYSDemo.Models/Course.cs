using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYSDemo.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display (Name="Course Name")]
        [MaxLength(50)]
        public string? Name { get; set; }
        [Required]
        [Display(Name = "Student")]
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [ValidateNever]
        public Student Student { get; set; }

    }
}
