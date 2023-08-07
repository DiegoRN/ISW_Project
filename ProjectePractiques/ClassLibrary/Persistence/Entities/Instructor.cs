using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestDep.Entities
{
    public partial class Instructor : Person
    {
        public String Ssn
        {
            get;
            set;
        }
        /* Associations*/

        //[InverseProperty("Manages")]
        public virtual ICollection<Activity> Activities
        {
            get;
            set;
        }
    }
}

