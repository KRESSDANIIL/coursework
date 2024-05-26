﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Login> Logins { get; set; }

    }
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MembershipType { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

    }

    public class Trainer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }

    public class Session
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public int ClientId { get; set; }
        public DateTime SessionDate { get; set; }
        public TimeSpan SessionTime { get; set; }
        public int Duration { get; set; }
        public string SessionType { get; set; }
        public virtual Trainer Trainer { get; set; }
        public virtual Client Client { get; set; }
    }

    public class Payment
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentMethod { get; set; }
        public virtual Client Client { get; set; }
    }
    public class Login
    {
        [Key]
        public string AdminLogin { get; set; }
        public string AdminPassword { get; set; }
    }

}
