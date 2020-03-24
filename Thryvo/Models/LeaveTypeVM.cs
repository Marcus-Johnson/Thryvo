﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thryvo.Models
{
    public class LeaveTypeVM
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Default Number of Days")]
        [Range(1,25, ErrorMessage = "Please enter a Valid Number.")]
        public int DefaultDays { get; set; }
        [Display(Name="Date Created")]
        public DateTime? DateCreated { get; set; }
    }
}
