using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestDep.Entities
{

    public partial class Enrollment
    {
        public Enrollment()
        {
            Payments = new List<Payment>();
        }
        public Enrollment(DateTime enrollmentDate, Activity activity,
        Payment payment, User user) : this()
        {
            CancellationDate = null;
            EnrollmentDate = enrollmentDate;
            ReturnedFirstCuotaIfCancelledActivity = null;
            //Id is managed by EE
            Activity = activity;
            Payments.Add(payment);
            User = user;
        }
        public int GetId() { return Id;}
        public Activity GetActivity() { return Activity;}
        public void GetEnrollmentData(out DateTime? cancellationDate, out DateTime enrollmentDate, out DateTime? returnedFirstCuotaIfCancelledActivity, out ICollection<int> paymentIds, out string userId)
        {
            cancellationDate = CancellationDate;
            enrollmentDate = EnrollmentDate;
            returnedFirstCuotaIfCancelledActivity = ReturnedFirstCuotaIfCancelledActivity;
            ICollection<int> paymentIds2 = new List<int>();
            foreach (Payment pa in Payments)
            {
                paymentIds2.Add(pa.Id);
            }
            paymentIds = paymentIds2;
            userId = User.Id;
        }
    }
}
