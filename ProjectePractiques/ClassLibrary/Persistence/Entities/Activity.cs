using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestDep.Entities
{
    public partial class Activity
    {
        public Days ActivityDays
        {
            get;
            set;
        }

        public bool Cancelled
        {
            get;
            set;
        }

        public String Description
        {
            get;
            set;
        }

        public TimeSpan Duration
        {
            get;
            set;
        }

        public DateTime FinishDate
        {
            get;
            set;
        }
        [Key]
        public int Id
        {
            get;
            set;
        }
        public int MaximumEnrollments
        {
            get;
            set;
        }

        public int MinimumEnrollments
        {
            get;
            set;
        }

        public double Price
        {
            get;
            set;
        }

        public DateTime StartDate
        {
            get;
            set;
        }

        public DateTime StartHour
        {
            get;
            set;
        }

        //Associacions

        //[InverseProperty("Offers")]
        public virtual ICollection<Enrollment> Enrollments
        {
            get;
            set;
        }

        //[Key]
        //[InverseProperty("Manages")]
        public virtual Instructor Instructor
        {
            get;
            set;
        }

        //[InverseProperty("Alocates")]
        public virtual ICollection<Room> Rooms
        {
            get;
            set;
        }
        public virtual Gym Gym
        {
            get;
            set;

        }
    }
}
