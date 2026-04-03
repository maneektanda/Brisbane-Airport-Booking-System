using System;
using System.Collections.ObjectModel;

namespace BNE_Airport_App
{
    /// <summary>
    /// The abstract base user controller for all users to inherit from.
    /// 
    /// Responsibility: A strating template that all user controllers should follow.
    /// </summary>
    public abstract class UserController
    {
        /// <summary>
        /// The database.
        /// </summary>
        protected Database database;

        /// <summary>
        /// The menu.
        /// </summary>
        protected BNEAirportMenus menu;

        /// <summary>
        /// A constructor for the controller.
        /// </summary>
        /// <param name="database">The database.</param>
        public UserController(Database database)
        {
            //this.user = user;
            this.database = database;
            menu = new BNEAirportMenus();
        }

        /// <summary>
        /// This method runs the respective controller until the user logs out.
        /// </summary>
        public void Run()
        {
            bool keep_going = true;

            while (keep_going)
            {
                keep_going = ProcessUserMenu();
            }
        }

        /// <summary>
        /// The method that the Run() method keeps running while logged in.
        /// </summary>
        /// <returns></returns>
        protected abstract bool ProcessUserMenu();

        /// <summary>
        /// A method to change a users password, common to all controllers.
        /// </summary>
        protected abstract void ProcessChangePassword();

        /// <summary>
        /// Gets arrival flights from database.
        /// </summary>
        /// <returns>A readonly sorted copy of the list of arrival flights.</returns>
        protected List<ArrivalFlight> GetArrivalFlights()
        {
            // Creates a readonly collection and retireves the entire flights list from the database.
            ReadOnlyCollection<Flight> all_flights = database.ReadOnlyFlightsList;

            // Gets all the flights of type ArrivalFlight and makes a new list.
            var arrival_flights = all_flights.OfType<ArrivalFlight>();

            // Assigning the list of flights to a specific ArrivalFlight list. This allows for sorting.
            List<ArrivalFlight> all_arrival_flights = new List<ArrivalFlight>(arrival_flights);
            all_arrival_flights.Sort();

            // Return the sorted list of arrival flights.
            return all_arrival_flights;
        }


        /// <summary>
        /// Gets departure flights from database.
        /// </summary>
        /// <returns>A readonly sorted copy of the list of departure flights.</returns>
        protected List<DepartureFlight> GetDepartureFlights()
        {
            // Creates a readonly collection and retireves the entire flights list from the database.
            ReadOnlyCollection<Flight> all_flights = database.ReadOnlyFlightsList;

            // Gets all the flights of type ArrivalFlight and makes a new list.
            var departure_flights = all_flights.OfType<DepartureFlight>();

            // Assigning the list of flights to a specific DepartureFlight list. This allows for sorting.
            List<DepartureFlight> all_departure_flights = new List<DepartureFlight>(departure_flights);
            all_departure_flights.Sort();

            // Return the sorted list of arrival flights.
            return all_departure_flights;
        }
    }
}