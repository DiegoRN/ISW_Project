using GestDep.Entities;
using GestDep.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestDep.Services
{
    public class GestDepService : IGestDepService
    {
        private readonly IDAL dal; //Persistence Layer Interface
        public CityHall cityHall;  //Services only work on a City Hall
        public Gym gym;			   // Gym of the City Hall. Also available from cityHall.Gyms.First();
        
        /// </summary>
        /// Returns a service Layer connected with the persistence Layer. Retrieves the CitiHall and Gym from the database if they exist. If not, it creates new ones
        /// </summary>
        /// <param name="dal"> Persistence Layer Interface</param>
        
        public GestDepService(IDAL dal)
        {
            this.dal = dal;
            try
            {
                    
                if (dal.GetAll<CityHall>().Count() == 0) //No cityHall in the system. Data initilization. 
                {
                    bool CLEAR_DATABASE = true;
                    int ROOMS_NUMBER = 9;
                    int INSTRUCTORS_NUMBER = 5;
                    Populate populateDB = new Populate(CLEAR_DATABASE,dal);
                    cityHall = populateDB.InsertCityHall();
                    gym = populateDB.InsertGym(cityHall);     //Also in cityHall.First();                
                    populateDB.InsertRooms(ROOMS_NUMBER, gym);  //Now available from gym.rooms;
                    populateDB.InsertInstructors(INSTRUCTORS_NUMBER, cityHall); //Now available from cityHall.People;

                }
                else
                {   //Retrieve the CityHall stored
                    cityHall = dal.GetAll<CityHall>().First();

                    if (cityHall.Gyms.Count > 0)
                    { //Retrieve the Gym stored
                        gym = cityHall.Gyms.First();                       

                    }
                    else
                    { //Adding Rooms and Gym
                        bool MANTAIN_DATABASE = false;
                        int ROOMS_NUMBER = 9;
                        Populate populateDB = new Populate(MANTAIN_DATABASE, dal);
                        gym = populateDB.InsertGym(cityHall);
                        populateDB.InsertRooms(ROOMS_NUMBER, gym);
                    }
                    int INSTRUCTORS_NUMBER = 5;
                    if (dal.GetAll<Instructor>().Count() == 0)//No instructors
                    { 
                        bool MANTAIN_DATABASE = false;
                        Populate populateDB = new Populate(MANTAIN_DATABASE, dal);
                        populateDB.InsertInstructors(INSTRUCTORS_NUMBER, cityHall); //Now available from cityHall.People;
                    }

                }
            } catch(Exception e)
            {
                throw new ServiceException("Error in the service init process", e);
                
            }
        }

        public int AddNewActivity(Days activityDays, string description, TimeSpan duration, DateTime finishDate, int maximumEnrollments, int minimumEnrollments, double price, DateTime startDate, DateTime startHour, ICollection<int> roomsIds)
        {
            if (finishDate < startDate)
            {
                throw new ServiceException("La data final és anterior a la data de començament");
            }
            if (startDate < DateTime.Now)
            {
                throw new ServiceException("La data de començament es troba en el passat");
            }
            if (activityDays == Days.None)
            {
                throw new ServiceException("No hi ha ningún dia de la setmana seleccionat");
            }
            if (duration.CompareTo(TimeSpan.Zero) < 0)
            {
                throw new ServiceException("La duració està en negatiu");
            }
            if (minimumEnrollments < 0)
            {
                throw new ServiceException("minimumEnrollments està en negatiu");
            }
            if (maximumEnrollments < minimumEnrollments)
            {
                throw new ServiceException("maximumEnrollments és menor que minimumEnrollments");
            }
            if (price < 0)
            {
                throw new ServiceException("El preu és negatiu");
            }

            if (roomsIds == null) { throw new ServiceException("No s'han insertat sales"); }
            if (!roomsIds.Any()) { throw new ServiceException("No s'han insertat sales"); }
            ICollection<int> disponibles = GetListAvailableRoomsIds(activityDays, duration, finishDate, startDate, startHour);

            //bool encontrado = true;
            foreach (int o in roomsIds)
            {
                if (!disponibles.Contains(o)) {
                    throw new ServiceException("La sala amb ID " + o.ToString() + " està ocupada");
                }
            }
            Activity activitat = new Activity(activityDays, description, duration, finishDate, maximumEnrollments, minimumEnrollments, price, startDate, startHour);
            gym.Activities.Add(activitat);
            foreach (int o in roomsIds)
            {
                //comprovar si la room existeix en GetRoomById, si es null no s'afegeix
                if (gym.GetRoomById(o) != null)
                {
                    gym.Activities.Last().AddRoom(gym.GetRoomById(o));
                    Room sala = gym.GetRoomById(o);
                    sala.Activities.Add(activitat);
                }
                else { throw new ServiceException("La sala amb ID " + o.ToString() + " no existeix"); }
            }
            this.SaveChanges();
            return activitat.Id;
            
        }

        public void AddNewUser(string address, string iban, string id, string name, int zipCode, DateTime birthDate, bool retired)
        
        {
            if (birthDate > DateTime.Now)
            {
                throw new ServiceException("La data de naixement és incorrecta");
            }
            if (iban == "") { throw new ServiceException("El IBAN està buit");}
            if (zipCode <= 0) { throw new ServiceException("El zipCode és incorrecte"); }
            if (id == "") { throw new ServiceException("El DNI està buit"); }
            if (id.Length < 9 || id.Length > 9) { throw new ServiceException("El DNI no té la longitud correcta");}
            if (!Char.IsLetter(Convert.ToChar((id.Substring(id.Length - 1))))) { throw new ServiceException("L'últim dígit no és una lletra"); };
            if (name == "") { throw new ServiceException("El nom està buit"); }
            if (address == "") { throw new ServiceException("L'adreça està buida"); }
            User usuari_n = new User(address, iban, id, name, zipCode, birthDate, retired);
            if (!cityHall.ja_user(id) && !cityHall.ja_instructor(id)) {
                cityHall.People.Add(usuari_n);
                this.SaveChanges();
            }
            else {
                throw new ServiceException("El user ja existeix");
            }
        }

        public void AssignInstructorToActivity(int activityId, string instructorId2)
        {
            if(cityHall.getInstructorById(instructorId2) == null) { throw new ServiceException("Instructor incorrecte"); }
            GetActivityDataFromId(activityId, out Days activityDays, out string description, out TimeSpan duration, out DateTime finishDate, out int maximumEnrollments, out int minimumEnrollments, out double price, out DateTime startDate, out DateTime startHour, out ICollection<int> enrollmentIds, out string instructorId, out ICollection<int> roomIds);
            if (!cityHall.getInstructorById(instructorId2).IsAvailable(activityDays, duration, finishDate, startDate, startHour)) { throw new ServiceException("Instructor no disponible"); }
            gym.getActivityById(activityId).AddInstructor(cityHall.getInstructorById(instructorId2));
            cityHall.getInstructorById(instructorId2).AddActivity(gym.getActivityById(activityId));
            this.SaveChanges();
        }

        public int EnrollUserInActivity(int activityId, string userId)
        {
            if(gym.getActivityById(activityId) == null) { throw new ServiceException("L'activitat no existeix");}
            if (userId == "") { throw new ServiceException("El Id de l'usuari està buit"); }
            if (!cityHall.ja_user(userId)) { throw new ServiceException("Invàlid Id de l'usuari"); }
            if (cityHall.getUserById(userId).GetActivityIdsFromEnrollments().Contains(activityId)) { throw new ServiceException("L'usuari ja està inscrit en l'activitat"); }
            double precio = GetUserDataNotInActivityAndFirstQuota(activityId, userId, out string address, out string iban, out string name, out int zipCode, out DateTime birthDate, out bool retired, out ICollection<int> enrollmentIds);
            Payment payment = new Payment(DateTime.Today, gym.getActivityById(activityId).Description, precio);
            Enrollment enrollment = new Enrollment(DateTime.Today, gym.getActivityById(activityId), payment, cityHall.getUserById(userId));
            cityHall.getUserById(userId).InsertarEnrollment(enrollment);
            cityHall.Payments.Add(payment);
            this.SaveChanges();

            return enrollment.Id;
        }

        public void GetActivityDataFromId(int ActivityId, out Days activityDays, out string description, out TimeSpan duration, out DateTime finishDate, out int maximumEnrollments, out int minimumEnrollments, out double price, out DateTime startDate, out DateTime startHour, out ICollection<int> enrollmentIds, out string instructorId, out ICollection<int> roomIds)
        {
            //ICollection<int> disponibles = GetAllActivitiesIds();
            Activity activity = gym.getActivityById(ActivityId);
            if (activity != null)
                activity.GetActivityData(out activityDays, out description, out duration,
            out finishDate, out maximumEnrollments, out minimumEnrollments, out price,
            out startDate, out startHour, out enrollmentIds, out instructorId, out roomIds);
            else //No debería ser alcanzable
                throw new ServiceException("L'activitat no es troba en la base de dades");

        }

    

        public ICollection<int> GetAllActivitiesIds()
        {
            return gym.GetActivitiesIds();
            //throw new NotImplementedException();
        }

        public ICollection<int> GetAllRunningOrFutureActivitiesIds()
        {
            ICollection<int> totes = gym.GetRunningOrFutureActivitiesIds();
            if(!totes.Any()) { throw new ServiceException("No hi ha activitats disponibles en aquest moment"); }
            else { return totes; }
            //throw new NotImplementedException();
        }

        public ICollection<string> GetAvailableInstructorsIds(Days activityDays, TimeSpan duration, DateTime finishDate, DateTime startDate, DateTime startHour)
        {

            if (finishDate < startDate) {
                throw new ServiceException("La data final és anterior a la data de començament");
            }
            if (startDate < DateTime.Now)
            {
                throw new ServiceException("La data de començament es troba en el passat");
            }
            if (activityDays == Days.None)
            {
                throw new ServiceException("No hi ha ningún dia de la setmana seleccionat");
            }
            if (duration.CompareTo(TimeSpan.Zero) < 0)
            {
                throw new ServiceException("La duració està en negatiu");
            }
            ICollection<Instructor> instructores = cityHall.GetInstructorsAvailable(activityDays, duration, finishDate, startDate, startHour);
            if (!instructores.Any())
            {
                throw new ServiceException("Ningún instructor disponible");
            }
            else
            {
                ICollection<string> instructoresIds = new List<string>();
                foreach (Instructor ins in instructores)
                {
                    instructoresIds.Add(ins.GetId());
                }
                return instructoresIds;
            }
        }

        public void GetEnrollmentDataFromIds(int activityId, int enrollmentId, out DateTime? cancellationDate, out DateTime enrollmentDate, out DateTime? returnedFirstCuotaIfCancelledActivity, out ICollection<int> paymentIds, out string userId)
        {
            Activity activity = gym.getActivityById(activityId);
            if (activity != null)
            {
                Enrollment enrollment = gym.getActivityById(activityId).GetEnrollmentById(enrollmentId);
                if (enrollment != null)
                    enrollment.GetEnrollmentData(out cancellationDate, out enrollmentDate, out returnedFirstCuotaIfCancelledActivity,
                out paymentIds, out userId);
                else //No debería ser alcanzable
                    throw new ServiceException("La inscripció no es troba en la base de dades");
            }
            else //No debería ser alcanzable
                throw new ServiceException("L'activitat no existeix");
        }

        public void GetGymData(out int gymId, out DateTime closingHour, out int discountLocal, out int discountRetired, out double freeUserPrice, out string name, out DateTime openingHour, out int zipCode, out ICollection<int> activityIds, out ICollection<int> roomIds)
        {
            if(gym == null) { throw new ServiceException("El gimnàs no existeix"); }
            gym.GetData(out gymId, out closingHour, out discountLocal, out discountRetired, out freeUserPrice, out name, out openingHour, out zipCode, out activityIds, out roomIds);
        }

        public void GetInstructorDataFromId(string instructorId, out string address, out string IBAN, out string name, out int zipCode, out string ssn, out ICollection<int> activitiesIds)
        {
            Instructor instructor = cityHall.getInstructorById(instructorId);
            if (instructor != null)
                instructor.GetInstructorData(out address, out IBAN, out name,
            out zipCode, out ssn, out activitiesIds);
            else //No debería ser alcanzable
                throw new ServiceException("L'instructor no es troba en la base de dades");
        }

        public ICollection<int> GetListAvailableRoomsIds(Days activityDays2, TimeSpan duration2, DateTime finishDate2, DateTime startDate2, DateTime startHour2)
        {
            if (finishDate2 < startDate2)
            {
                throw new ServiceException("La data final és anterior a la data de començament");
            }
            if (startDate2 < DateTime.Now)
            {
                throw new ServiceException("La data de començament es troba en el passat");
            }
            if (activityDays2 == Days.None)
            {
                throw new ServiceException("No hi ha ningún dia de la setmana seleccionat");
            }
            if (duration2.CompareTo(TimeSpan.Zero) < 0)
            {
                throw new ServiceException("La duració està en negatiu");
            }
            ICollection<Room> rooms = gym.GetRoomsAvailable(activityDays2, duration2, finishDate2, startDate2, startHour2);
            if (!rooms.Any())
            {
                throw new ServiceException("Cap sala disponible");
            }
            else
            {
                ICollection<int> roomsIds = new List<int>();
                foreach (Room ins in rooms)
                {
                    roomsIds.Add(ins.GetId());
                }
                return roomsIds;
            }
        }

        public Dictionary<DateTime, int> GetListAvailableRoomsPerWeek(DateTime initialMonday)
        {
            if (initialMonday.DayOfWeek != DayOfWeek.Monday) { throw new ServiceException("Dia seleccionat diferent a dilluns"); }
            DateTime prueba = initialMonday.AddDays(6);
            if (prueba < DateTime.Now) { throw new ServiceException("La semana es troba en el passat"); }
            Dictionary<DateTime, int> AvailableRoomsPerWeek = new Dictionary<DateTime, int>();
            int cont = 0;
            Days dias = Days.None;

            while (cont < 7)
            {

                String diasemana = initialMonday.DayOfWeek.ToString();
                switch (diasemana)
                {
                    case "Monday":
                        dias = Days.Mon;
                        break;
                    case "Tuesday":
                        dias = Days.Tue;
                        break;
                    case "Wednesday":
                        dias = Days.Wed;
                        break;
                    case "Thursday":
                        dias = Days.Thu;
                        break;
                    case "Friday":
                        dias = Days.Fri;
                        break;
                    case "Saturday":
                        dias = Days.Sat;
                        break;
                    case "Sunday":
                        dias = Days.Sun;
                        break;

                }
                DateTime opening = new DateTime(initialMonday.Year, initialMonday.Month, initialMonday.Day, gym.OpeningHour.Hour, gym.OpeningHour.Minute, 0);
                DateTime clossing = new DateTime(initialMonday.Year, initialMonday.Month, initialMonday.Day, gym.ClosingHour.Hour, gym.ClosingHour.Minute, 0);
                DateTime auxDia = new DateTime (initialMonday.Year, initialMonday.Month, initialMonday.Day, gym.OpeningHour.Hour, gym.OpeningHour.Minute, 0);
                DateTime auxHora = new DateTime(initialMonday.Year, initialMonday.Month, initialMonday.Day, gym.OpeningHour.Hour, gym.OpeningHour.AddMinutes(45).Minute, 0);
                while (opening.CompareTo(clossing) < 0) 
                {
                    
                    TimeSpan Duration = auxHora - opening;
                    ICollection<int> Lista = new List<int>();
                    if (!(auxDia < DateTime.Now)) Lista = GetListAvailableRoomsIds(dias, Duration, clossing, auxDia, opening);  
                    AvailableRoomsPerWeek.Add(auxDia, Lista.Count);
                    auxDia = auxDia.AddMinutes(45);
                    opening = opening.AddMinutes(45);
                    auxHora = auxHora.AddMinutes(45);
                }
                initialMonday = initialMonday.AddDays(1);
                cont++;
            }
            return AvailableRoomsPerWeek;
        }

        public void GetPaymentDataFromId(int paymentId, out DateTime date, out string description, out double quantity)
        {
            Payment payment = cityHall.getPaymentById(paymentId);
            if (payment != null)
                payment.GetPaymentData(out date, out description, out quantity);
            else //No debería ser alcanzable
                throw new ServiceException("El pagament no es troba en la base de dades");
        }

        public void GetRoomDataFromId(int roomId, out int number, out ICollection<int> activityIds)
        {
            Room room = gym.GetRoomById(roomId);
            if (room != null)
                room.GetRoomData(out number, out activityIds);
            else //No debería ser alcanzable
                throw new ServiceException("La sala no es troba en la base de dades");
        }

        public void GetUserDataFromId(string userId, out string address, out string iban, out string name, out int zipCode, out DateTime birthDate, out bool retired, out ICollection<int> enrollmentIds)
        {
            User user = cityHall.getUserById(userId);
            if (user != null)
                user.GetUserData(out address, out iban, out name, out zipCode, out birthDate, out retired, out enrollmentIds);
            else //No debería ser alcanzable
                throw new ServiceException("L'usuari no es troba en la base de dades");
        }

        public double GetUserDataNotInActivityAndFirstQuota(int activityId, string userId, out string address, out string iban, out string name, out int zipCode, out DateTime birthDate, out bool retired, out ICollection<int> enrollmentIds)
        {
            Activity activity = gym.getActivityById(activityId);
            if (activity == null) { throw new ServiceException("L'activitat no es troba en la base de dades"); }
            User user = cityHall.getUserById(userId);
            if (user != null)
            {
                double quota = activity.Price;
                if (user.GetActivityIdsFromEnrollments().Contains(activityId)) { throw new ServiceException("L'usuari ja està en l'activitat"); }
                user.GetUserData(out address, out iban, out name, out zipCode, out birthDate, out retired, out enrollmentIds);
                if(retired) { quota = quota * (1 - ((Double)gym.DiscountRetired / 100)); }
                if (user.ZipCode == gym.ZipCode) { quota = quota * (1 - ((Double)gym.DiscountLocal / 100)); }
                return Math.Round(quota,1);
            }
            else //No debería ser alcanzable
                throw new ServiceException("L'usuari no es troba en la base de dades");
            
        }

        #region Connection with the Persistence Layer
        public void RemoveAllData()
        {
            dal.RemoveAllData();
        }

     
        public void SaveChanges()
        {
            dal.Commit();
        }
        #endregion
    }
}
