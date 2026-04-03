using System;
using System.Collections.ObjectModel;

namespace BNE_Airport_App
{
    /// <summary>
    /// This class stores all the data for the app. It returns readonly copies of any of its lists of data.
    /// 
    /// Using the abstract class as the type for the lists allows for polymorphism. Which lets future child
    /// classes of those types also be stored in these lists.
    /// 
    /// Responsibility: Storing and adding Users, Flights, Planes and Bookings to the app.
    /// </summary>
    public class Database
    {
        /// <summary>
        /// A list of users.
        /// </summary>
        private List<User> users;

        /// <summary>
        /// A list of flights.
        /// </summary>
        private List<Flight> flights;

        /// <summary>
        /// A list of planes.
        /// </summary>
        private List<Plane> planes;

        /// <summary>
        /// A list of bookings.
        /// </summary>
        private List<Booking> bookings;


        /// <summary>
        /// A getter that returns a readonly copy of the users.
        /// </summary>
        public ReadOnlyCollection<User> ReadOnlyUsersList
        {
            get { return new ReadOnlyCollection<User>(users); }
        }

        /// <summary>
        /// A getter that returns a readonly copy of the flights.
        /// </summary>
        public ReadOnlyCollection<Flight> ReadOnlyFlightsList
        {
            get { return new ReadOnlyCollection<Flight>(flights); }
        }

        /// <summary>
        /// A getter that returns a readonly copy of the planes.
        /// </summary>
        public ReadOnlyCollection<Plane> ReadOnlyPlanesList
        {
            get { return new ReadOnlyCollection<Plane>(planes); }
        }

        /// <summary>
        /// A getter that returns a readonly copy of the bookings.
        /// </summary>
        public ReadOnlyCollection<Booking> ReadOnlyBookingsList
        {
            get { return new ReadOnlyCollection<Booking>(bookings); }
        }

        /// <summary>
        /// A constructor that creates all the lists.
        /// </summary>
        public Database()
        {
            users = new List<User>();
            flights = new List<Flight>();
            planes = new List<Plane>();
            bookings = new List<Booking>();
        }

        /// <summary>
        /// Adds a user to the users list.
        /// </summary>
        /// <param name="new_user">The user to add.</param>
        public void AddUser(User new_user)
        {
            users.Add(new_user);
        }

        /// <summary>
        /// Adds a flight to the flights list.
        /// </summary>
        /// <param name="new_flight">The flight to add.</param>
        public void AddFlight(Flight new_flight)
        {
            flights.Add(new_flight);
        }

        /// <summary>
        /// Adds a plane to the planes list.
        /// </summary>
        /// <param name="new_plane">The plane to add.</param>
        public void AddPlane(Plane new_plane)
        {
            planes.Add(new_plane);
        }

        /// <summary>
        /// Adds a booking to the bookings list.
        /// </summary>
        /// <param name="new_booking">The booking to add.</param>
        public void AddBooking(Booking new_booking)
        {
            bookings.Add(new_booking);
        }
    }
}