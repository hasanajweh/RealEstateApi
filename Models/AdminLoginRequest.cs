﻿using System.ComponentModel.DataAnnotations;

namespace PalsoftRealEstate.Models
{
    public class AdminLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

