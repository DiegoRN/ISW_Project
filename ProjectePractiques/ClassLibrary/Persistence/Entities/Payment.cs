using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestDep.Entities
{
    public partial class Payment
    {
        public DateTime Date
        {
            get;
            set;
        }
        public string Description
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
        public double Quantity
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
