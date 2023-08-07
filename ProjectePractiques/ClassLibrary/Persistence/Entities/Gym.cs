using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestDep.Entities
{
    public partial class Gym
    {
        public DateTime ClosingHour
        {
            get;
            set;
        }
        public int DiscountLocal
        {
            get;
            set;
        }
        public int DiscountRetired
        {
            get;
            set;
        }
        public double FreeUserPrice
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
        public String Name
        {
            get;
            set;
        }
        public DateTime OpeningHour
        {
            get;
            set;
        }
        public int ZipCode
        {
            get;
            set;
        }

        //Associacions

        public virtual ICollection<Activity> Activities
        {
            get;
            set;
        }

        public virtual ICollection<Room> Rooms
        {
            get;
            set;
        }

        public virtual CityHall CityHall
        {
            get;
            set;
        }

    }
}
