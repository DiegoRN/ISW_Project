using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestDep.Entities
{
    public partial class Activity
    {
        public Activity(){
            Enrollments = new List<Enrollment>();
            Rooms = new List<Room>();
        }
        public Activity(Days activityDays, String description, TimeSpan duration,
            DateTime finishDate, int maximumEnrollments, int minimumEnrollments, double price,
            DateTime startDate, DateTime startHour) : this()
        {
            ActivityDays = activityDays;
            Cancelled = false;
            Description = description;
            Duration = duration;
            FinishDate = finishDate;
            //Del ID s'encarrega el EF
            MaximumEnrollments = maximumEnrollments;
            MinimumEnrollments = minimumEnrollments;
            Price = price;
            StartDate = startDate;
            StartHour = startHour;
            //Relacions (Istructor no va xq la card. minima és 0)
            Rooms = new List<Room>();
        }
        public void GetActivityData(out Days activityDays, out string description, out TimeSpan duration,
            out DateTime finishDate, out int maximumEnrollments, out int minimumEnrollments, out double price,
            out DateTime startDate, out DateTime startHour, out ICollection<int> enrollmentIds,
            out string instructorId, out ICollection<int> roomIds)
        {
            activityDays = this.ActivityDays;
            description = this.Description;
            duration = this.Duration;
            finishDate = Convert.ToDateTime(this.FinishDate);
            maximumEnrollments = this.MaximumEnrollments;
            minimumEnrollments = this.MinimumEnrollments;
            price = this.Price;
            startDate = Convert.ToDateTime(this.StartDate);
            startHour = Convert.ToDateTime(this.StartHour);
            ICollection<int> enrollmentsIds = new List<int>();
            foreach (Enrollment en in Enrollments)
            {
                enrollmentsIds.Add(en.GetId());
            }
            enrollmentIds = enrollmentsIds;
            if(Instructor != null)  instructorId = this.Instructor.GetId(); else instructorId = "";
            ICollection<int> roomsIds = new List<int>();
            foreach (Room ro in Rooms)
            {
                roomsIds.Add(ro.GetId());
            }
            roomIds = roomsIds;
        }
        public void AddInstructor(Instructor instructor) { Instructor = instructor;}
        public int GetId() { return Id; }
        public void AddRoom(Room ro)
        {
            Rooms.Add(ro);
        }
        public Enrollment GetEnrollmentById(int id)
        {
            foreach(Enrollment en in Enrollments)
            {
                if(en.Id == id) { return en; }
            }
            return null;
        }
    }
}
