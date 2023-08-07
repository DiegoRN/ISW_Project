using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestDep.Entities
{
    public partial class Gym
    {
        public Gym()
        {
            Activities = new List<Activity>();
            Rooms = new List<Room>();
        }
        public Gym(DateTime closinghour, int discountlocal, int discountretired, double freeuserprice, string name,
            DateTime openinghour, int zipcode) : this()
        {
            ClosingHour = closinghour;
            DiscountLocal = discountlocal;
            DiscountRetired = discountretired;
            //Id is managed by EE
            FreeUserPrice = freeuserprice;
            Name = name;
            OpeningHour = openinghour;
            ZipCode = zipcode;
            //Relacions
            Activities = new List<Activity>();
            Rooms = new List<Room>();
        }

        public void AddActivity(Days activityDays, String description, TimeSpan duration,
            DateTime finishDate, int maximumEnrollments, int minimumEnrollments, double price,
            DateTime startDate, DateTime startHour) {
            
            Activity actividad = new Activity(activityDays,description,duration,finishDate,maximumEnrollments,minimumEnrollments,price,startDate,startHour);
            Activities.Add(actividad);
        }

        public ICollection<int> GetActivitiesIds()
        {
            ICollection<int> AllIds = new List<int>();
            foreach (Activity ac in Activities)
            {
                AllIds.Add(ac.GetId());
            }
            return AllIds;
        }

        public ICollection<Room> GetRooms() {
            return Rooms;
        }

        public Room GetRoomById(int id)
        {
            foreach (Room o in Rooms)
            {
                if(o.Id == id) { return o; }
            }
            return null;
        }

        public Activity getActivityById(int id) {
            foreach(Activity ac in Activities) {
                if(id.CompareTo(ac.GetId()) == 0) {return ac;}
            }
            return null;
        }
        public ICollection<Room> GetRoomsAvailable(Days activityDays2, TimeSpan duration2, DateTime finishDate2, DateTime startDate2, DateTime startHour2)
        {
            ICollection<Room> rooms = new List<Room>();
            foreach (Room p in Rooms)
            {
                if (p.IsAvailable(activityDays2, duration2, finishDate2, startDate2, startHour2))
                {
                    rooms.Add(p);
                }
            }
            return rooms;
        }
        public ICollection<int> GetRunningOrFutureActivitiesIds()
        {
            ICollection<int> AllIds = new List<int>();
            foreach (Activity ac in Activities)
            {
                if(ac.FinishDate >= DateTime.Today)
                AllIds.Add(ac.GetId());
            }
            return AllIds;
        }
        public void GetData(out int gymId, out DateTime closingHour, out int discountLocal, out int discountRetired, out double freeUserPrice, out string name, out DateTime openingHour, out int zipCode, out ICollection<int> activityIds, out ICollection<int> roomIds)
        {
            gymId = Id;
            closingHour = ClosingHour;
            discountLocal = DiscountLocal;
            discountRetired = DiscountRetired;
            freeUserPrice = FreeUserPrice;
            name = Name;
            openingHour = OpeningHour;
            zipCode = ZipCode;
            ICollection<int> activityIds2 = new List<int>();
            foreach(Activity ac in Activities)
            {
                activityIds2.Add(ac.Id);
            }
            activityIds = activityIds2;
            ICollection<int> roomIds2 = new List<int>();
            foreach (Room ro in Rooms)
            {
                roomIds2.Add(ro.Id);
            }
            roomIds = roomIds2;


        }
    }
}
