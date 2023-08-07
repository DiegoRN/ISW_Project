using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestDep.Entities
{
   public partial class User
     {
     public User() {
        Enrollments = new List<Enrollment>();

     }
        public User(string address, string iban, string id, string name,
        int zipCode, DateTime birthDate, bool retired) :
        base(address, iban, id, name, zipCode)
        {
            BirthDate = birthDate;
            Retired = retired;
            Enrollments = new List<Enrollment>();
        }

        public ICollection<int> GetActivityIdsFromEnrollments()
        {
            ICollection<int> ids = new List<int>();
            foreach (Enrollment en in Enrollments)
            {
                ids.Add(en.GetActivity().Id);
            }
            return ids;
        }

        public double GetQuotaFromActivityIdFromEnrollments(int id)
        {
            foreach (Enrollment en in Enrollments)
            {
                if (en.Activity.Id == id) { return en.Activity.Price; }
            }
            return 0.0;
        }

        public void InsertarEnrollment(Enrollment en)
        {
            Enrollments.Add(en);
        }
        public void GetUserData(out string address, out string iban, out string name, out int zipCode, out DateTime birthDate, out bool retired, out ICollection<int> enrollmentIds)
        {
            address = Address;
            iban = IBAN;
            name = Name;
            zipCode = ZipCode;
            birthDate = BirthDate;
            retired = Retired;
            ICollection<int> enrollmentIds2 = new List<int>();
            foreach(Enrollment en in Enrollments)
            {
                enrollmentIds2.Add(en.Id);
            }
            enrollmentIds = enrollmentIds2;
        }
    }

}
