using AngelsAutomotive.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data
{
    public class DataContext : IdentityDbContext<User>//we inject the user here
    {
        public DbSet<Vehicle> Vehicles { get; set; }


       // public DbSet<Brand> Brands { get; set; }


        public DbSet<Appointment> Appointments { get; set; }


        public DbSet<AppointmentDetail> AppointmentDetails { get; set; }


        public DbSet<AppointmentDetailTemp> AppointmentDetailsTemp { get; set; }


        public DbSet<State> States { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<ServiceOrder> ServiceOrders { get; set; }

        public DbSet<ServiceOrderDetails> ServiceOrderDetails { get; set; }

        public DbSet<ServiceOrderDetailTemp> ServiceOrderDetailTemp { get; set; }

        public DbSet<Parts> Parts { get; set; }

        public DbSet<PartsServiceOrder> PartsServiceOrders { get; set; }

        public DbSet<ServiceType> ServiceTypes { get; set; }

        public DbSet<ServiceTypeOrder> ServiceTypeOrders { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.VehiclePlateNumber)
                .IsUnique();

          /* modelBuilder.Entity<Vehicle>()
              .HasMany<AppointmentDetail>(a => a.)
              .WithOptional(x => x.id)
              .WillCascadeOnDelete(true);*/
            
            //TODO: Add unique index to other Entities 

            //Cascade Deleting Rule
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach(var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }



            base.OnModelCreating(modelBuilder);
        }

    }
}
