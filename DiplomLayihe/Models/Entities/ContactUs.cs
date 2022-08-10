﻿using Back_End_Layihe.AppCode.InfraStructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomLayihe.Models.Entities
{
    public class ContactUs : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Comment { get; set; }
        public string Subject { get; set; }
        public bool EmailSended { get; set; }
        public string Answer { get; set; }
        public DateTime? AnswerDate { get; set; }
        public int? AnswerById { get; set; }
    }
}
