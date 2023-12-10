using DomainLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
        }
    
         
        public DbSet<BookingAppointment> bookingAppointments { get; set; }
        public DbSet<BookingRequest> bookingRequests { get; set; }
        public DbSet<Discound> discounds { get; set; }
        public DbSet<Specialization> specializations { get; set; }
        public DbSet<BookingDays> bookingDays { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }

    }
}
