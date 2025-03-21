﻿using Microsoft.AspNetCore.Identity;

namespace HotelGestionale.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> ApplicationUserRole { get; set; }
    }
}
