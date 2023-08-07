using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestDep.Entities;


namespace GestDep.Persistence
{
    public class GestDepDbContext : DbContextISW
    {
        // DbSets for persistent classes in your case study
        //TODO

        public GestDepDbContext() : base("Name=GestDepDbConnection") //this is the connection string name
        {
            /*
              See DbContext.Configuration documentation
              */
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        public IDbSet<Person> People { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Instructor> Instructors { get; set; }
        public IDbSet<Payment> Payments { get; set; }
        public IDbSet<CityHall> CityHalls { get; set; }
        public IDbSet<Enrollment> Enrollments { get; set; }
        public IDbSet<Gym> Gyms { get; set; }
        public IDbSet<Room> Rooms { get; set; }
        public IDbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Primary keys with non conventional name
            /*
            modelBuilder.Entity<Person>().HasKey(p => p.Dni);
            modelBuilder.Entity<Customer>().HasKey(c => c.Dni);
            modelBuilder.Entity<CreditCard>().HasKey(c => c.Digits);
            */
            // Classes with more than one relationship
            /*
            modelBuilder.Entity<Reservation>().HasRequired(r => r.PickUpOffice).WithMany(o => o.PickUpReservations).WillCascadeOnDelete(false);
            modelBuilder.Entity<Reservation>().HasRequired(r => r.ReturnOffice).WithMany(o => o.ReturnReservations).WillCascadeOnDelete(false);
            */
        }

        // Generic method to clear all the data (except some relations if needed)
        public override void RemoveAllData()
        {
            clearSomeRelationships();

            base.RemoveAllData();
        }

        // Sometimes it is needed to clear some relationships explicitly 
        private void clearSomeRelationships()
        { 
            //TODO: if required while implementing the logic


        }
        
        static GestDepDbContext() { Database.SetInitializer<GestDepDbContext>(new DropCreateDatabaseIfModelChanges<GestDepDbContext>());}
    }


}
