using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GestDep.Entities
{
    public partial class Person
    {
        public string Address
        {
            get;
            set;
        }
        public string IBAN
        {
            get;
            set;
        }
        [Key]
        public string Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public int ZipCode
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
