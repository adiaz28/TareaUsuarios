﻿using System.ComponentModel.DataAnnotations;

namespace Cibertec.Models
{
    public class Customer
    {
        [Required]
        public int Id { get; set; }  
        
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
    }
}
