using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestDep.Entities
{
    public partial class CityHall
    {
        public CityHall() {
            Gyms = new List<Gym>();
            People = new List<Person>();
            Payments = new List<Payment>();
        }

        public CityHall(String name)
        {
            //Id = id; lliurament 2
            Name = name;

            Gyms = new List<Gym>();
            People = new List<Person>();
            Payments = new List<Payment>();
        }
        public Boolean ja_user(string id)
        {
            bool res = false;
            foreach (Person p in People)
            {
                if (p is User && p.Id.Equals(id))
                {
                    res = true;
                    return res;
                }
            }
           return res;
        }

        public Boolean ja_instructor(string id)
        {
            bool res = false;
            foreach (Person p in People)
            {
                if (p is Instructor && p.Id.Equals(id))
                {
                    res = true;
                    return res;
                }
            }
            return res;
        }
        public ICollection<Instructor> GetInstructorsAvailable(Days activityDays2, TimeSpan duration2, DateTime finishDate2, DateTime startDate2, DateTime startHour2)
        {
            ICollection<Instructor> instructors = new List<Instructor>();
            foreach (Instructor p in People)
            {
                if (p.IsAvailable(activityDays2, duration2, finishDate2, startDate2, startHour2))
                {
                    instructors.Add(p);
                }
            }
            return instructors;
        }

        public Instructor getInstructorById(string id)
        {
            foreach (Person p in People)
            {
                if (p is Instructor)
                {
                    if (p.Id.Equals(id)) { return p as Instructor; }
                }
            }
            return null;
        }
        public User getUserById(string id)
        {
            foreach (Person us in People)
            {
                if (us is User)
                {
                    if (us.Id.Equals(id)) { return (User)us; }
                }
            }
            return null;
        }
        public Payment getPaymentById(int id)
        {
            foreach (Payment p in Payments)
            {
                if (p.Id == id) { return p; }
            }
            return null;
        }
    }
}
