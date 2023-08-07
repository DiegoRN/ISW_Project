using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestDep.Entities
{

    public partial class Enrollment
    {
        public DateTime? CancellationDate
        {
            get;
            set;
        }
        public DateTime EnrollmentDate
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
        public DateTime? ReturnedFirstCuotaIfCancelledActivity
        {
            get;
            set;
        }
        //Associations
        
        //[InverseProperty("Offers")]
        public virtual Activity Activity
        {
            get;
            set;
        }

        
        public virtual ICollection<Payment> Payments
        {
            get;
            set;
        }

        //[InverseProperty("Enrolls")]
        public virtual User User
        {
            get;
            set;
        }
    }
}
