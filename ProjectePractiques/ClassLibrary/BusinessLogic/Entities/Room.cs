using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestDep.Entities
{
    public partial class Room
    {
        public Room()
        {
            Activities = new List<Activity>();
        }
        public Room(int number) 
        {
            //Id = id;
            Number = number;
            //Relacions
            Activities = new List<Activity>();
        }
        public int GetId() { return Id; }
        public void GetRoomData(out int number, out ICollection<int> activityIds)
        {
            number = Number;
            ICollection<int> activities2Ids = new List<int>();
            foreach (Activity ac in Activities)
            {
                activities2Ids.Add(ac.GetId());
            }
            activityIds = activities2Ids;
        }

        public Boolean IsAvailable(Days activityDays2, TimeSpan duration2, DateTime finishDate2, DateTime startDate2, DateTime startHour2)
        {
            foreach (Activity ac in Activities)
            {
                Days ningundia = default;
                ac.GetActivityData(out Days activityDays, out string description, out TimeSpan duration, out DateTime finishDate,
                    out int maximumEnrollments, out int minimumEnrollments, out double price, out DateTime startDate,
                    out DateTime startHour, out ICollection<int> enrollmentIds, out string instructorId, out ICollection<int> roomIds);
                DateTime startHour3 = new DateTime(1,1,1, startHour.Hour, startHour.Minute, startHour.Second);
                DateTime startHour4 = new DateTime(1,1,1, startHour2.Hour, startHour2.Minute, startHour2.Second);
                if ((((activityDays & activityDays2) != ningundia)) &&
                    (((DateTime.Compare(finishDate, finishDate2) < 0) && (DateTime.Compare(finishDate, startDate2) > 0)) ||
                    ((DateTime.Compare(finishDate2, finishDate) < 0) && (DateTime.Compare(finishDate2, startDate) > 0)) ||
                    (DateTime.Compare(startDate, startDate2) == 0) ||
                    (DateTime.Compare(finishDate, finishDate2) == 0)) &&
                    (((DateTime.Compare(startHour3, startHour4) < 0) && (DateTime.Compare(startHour3 + duration, startHour4) > 0)) ||
                    ((DateTime.Compare(startHour4, startHour3) < 0) && (DateTime.Compare(startHour4 + duration2, startHour3) > 0)) ||
                    (DateTime.Compare(startHour3, startHour4) == 0))) { return false; }
            }
            return true;
        }
    }
}
