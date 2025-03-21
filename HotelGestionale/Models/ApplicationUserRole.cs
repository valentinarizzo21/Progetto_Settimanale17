using HotelGestionale.Models;
using Microsoft.AspNetCore.Identity;

public class ApplicationUserRole : IdentityUserRole<string>
{
    public ApplicationUser ApplicationUser { get; set; }
    public ApplicationRole ApplicationRole { get; set; }
}