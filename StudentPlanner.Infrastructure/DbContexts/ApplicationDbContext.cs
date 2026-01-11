using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Entities.Events;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StudentPlanner.Core.Domain;

namespace Entities;
public class ApplicationDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    //Must be virtual to be mocked - internally the mocking framework is trying to create an alternative implementation for a particular property or method that we're trying to mock
    public virtual DbSet<PersonalEvent> PersonalEvents { get; set; }
    public virtual DbSet<AcademicEvent> AcademicEvents { get; set; }
    public virtual DbSet<Faculty> Faculties { get; set; }
    //public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserFacultyAssignment> UserFacultyAssignments {  get; set; }
    public virtual DbSet<EventRequest> EventRequests { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EventRequest>().ToTable("EventRequests");
        modelBuilder.Entity<PersonalEvent>().ToTable("PersonalEvents");
        modelBuilder.Entity<AcademicEvent>().ToTable("AcademicEvents");
        modelBuilder.Entity<Faculty>().ToTable("Faculties");
        modelBuilder.Entity<ApplicationUser>().ToTable("Users");
        modelBuilder.Entity<UserFacultyAssignment>().ToTable("UserFacultyAssignments");

        //Relationships
        modelBuilder.Entity<UserFacultyAssignment>()
        .HasKey(x => new { x.UserId, x.FacultyId });

        modelBuilder.Entity<UserFacultyAssignment>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<UserFacultyAssignment>()
            .HasOne(x => x.Faculty)
            .WithMany()
            .HasForeignKey(x => x.FacultyId);

        modelBuilder.Entity<AcademicEvent>()
            .HasOne<Faculty>(p => p.Faculty)
            .WithMany(p => p.AcademicEvents)
            .HasForeignKey(p => p.FacultyId);

        modelBuilder.Entity<EventRequest>()
            .HasOne<Faculty>(p => p.Faculty)
            .WithMany(p => p.EventRequests)
            .HasForeignKey(p => p.FacultyId);

        modelBuilder.Entity<EventRequest>()
            .HasOne<ApplicationUser>(e => e.User)
            .WithMany(usr => usr.EventRequests)
            .HasForeignKey(req => req.UserId);

        modelBuilder.Entity<PersonalEvent>()
            .HasOne<ApplicationUser>(e => e.User)
            .WithMany(usr => usr.PersonalEvents)
            .HasForeignKey(e => e.UserId);

    }

}
