using AssignmentCheck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Data.Contexts
{
    public class AssignmentCheckDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }

        public AssignmentCheckDbContext(DbContextOptions<AssignmentCheckDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>()
                .HasOne(u => u.Subject)
                .WithMany(u => u.Assignments)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
