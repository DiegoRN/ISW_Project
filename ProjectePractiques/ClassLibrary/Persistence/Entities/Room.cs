using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestDep.Entities
{
    public partial class Room
    {
        [Key]
        public int Id 
        {
            get;
            set;
        }

        public int Number 
        {
            get;
            set;
        }

        //Associacions

        //[InverseProperty("Alocates")]
        public virtual ICollection<Activity> Activities
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
