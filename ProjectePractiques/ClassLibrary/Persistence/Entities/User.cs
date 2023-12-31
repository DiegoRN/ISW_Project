﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestDep.Entities
{
    public partial class User : Person
    {
        public DateTime BirthDate
        {
            get;
            set;
        }

        public bool Retired
        {
            get;
            set;
        }
        /* Associations*/
        //[Key]
        //[InverseProperty("Enrollments")]
        public virtual ICollection<Enrollment> Enrollments
        {
            get;
            set;
        }
    }
}
