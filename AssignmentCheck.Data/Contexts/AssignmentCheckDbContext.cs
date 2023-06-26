using AssignmentCheck.Domains.Entities;
using AssignmentCheck.Domains.Entities.Assignments;
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
        public AssignmentCheckDbContext(DbContextOptions<AssignmentCheckDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Subjects)
                .WithOne(e => e.Users)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            modelBuilder.Entity<Subject>()
                .HasMany(s => s.Assignments)
                .WithOne(s => s.Subjects)
                .HasForeignKey(s => s.SubjectId)
                .IsRequired();
        }*/
    }
}
