using System;
using System.Collections.ObjectModel;
namespace BNE_Airport_App
{
    /// <summary>
    /// A class that represents a flight manager controller. They can create and delaay flights as well as view all flights.
    /// 
    /// Responsibility: Handles a logged in flight manager.
    /// </summary>
    public class FlightManagerController : UserController
    {
        /// <summary>
        /// The logged in flght manager.
        /// </summary>
        private FlightManager flight_manager;

        /// <summary>
        /// A constructor for the controller.
        /// </summary>
        /// <param name="logged_in_flight_manager">The specific logged in flight manager.</param>
        /// <param name="database">The database with all the data.</param>
        public FlightManagerController(FlightManager logged_in_flight_manager, Database database)
            : base(database)
        {
            this.flight_manager = logged_in_flight_manager;

        }

        /// <summary>
        /// This method is called when the Run() method is called from the base class.
        /// Presents the flight manager with all their options.
        /// </summary>
        /// <returns>A list of functions that the traveller can select.</returns>
        protected override bool ProcessUserMenu()
        {
            // Define consts unique to this controller to use in the list of options.
            const string CREATE_ARRIVAL_FLIGHT_STR = "Create an arrival flight";
            const string CREATE_DEPARTURE_FLIGHT_STR = "Create a departure flight";
            const string DELAY_ARRIVAL_FLIGHT_STR = "Delay an arrival flight";
            const string DELAY_DEPARTURE_FLIGHT_STR = "Delay a departure flight";
            const string SEE_ALL_FLIGHT_DETAILS_STR = "See the details of all flights";

            // A list of options.
            List<string> options = new List<string>();
            options.Add(AppConsts.SEE_PERSONAL_DETAILS_STR);
            options.Add(AppConsts.CHANGE_PASSWORD_STR);
            options.Add(CREATE_ARRIVAL_FLIGHT_STR);
            options.Add(CREATE_DEPARTURE_FLIGHT_STR);
            options.Add(DELAY_ARRIVAL_FLIGHT_STR);
            options.Add(DELAY_DEPARTURE_FLIGHT_STR);
            options.Add(SEE_ALL_FLIGHT_DETAILS_STR);
            options.Add(AppConsts.LOGOUT_STR);

            // Consts for the all option.
            const int CREATE_ARRIVAL_FLIGHT_INT = 2;
            const int CREATE_DEPARTURE_FLIGHT_INT = 3;
            const int DELAY_ARRIVAL_FLIGHT_INT = 4;
            const int DELAY_DEPARTURE_FLIGHT_INT = 5;
            const int SEE_ALL_FLIGHT_DETAILS_INT = 6;
            const int LOGOUT_INT = 7;

            const string FLIGHT_MANAGER_MENU_TITLE = "Flight Manager Menu.";

            // // Gets the option from the flight manager to determine what they want to do.
            int option = menu.DisplayUserMenu(FLIGHT_MANAGER_MENU_TITLE, AppConsts.USER_MENU_MSG, options);

            switch (option)
            {
                // Shows their details.
                case AppConsts.SEE_PERSONAL_DETAILS_INT:
                    menu.DisplayMessage(flight_manager);
                    break;

                // Allows them to change their password.
                case AppConsts.CHANGE_PASSWORD_INT:
                    ProcessChangePassword();
                    break;

                // Create an arrival flight.
                case CREATE_ARRIVAL_FLIGHT_INT:
                    ProcessCreateArrivalFlight();
                    break;

                // Create a departure flight.
                case CREATE_DEPARTURE_FLIGHT_INT:
                    ProcessCreateDepartureFlight();
                    break;

                // Delay an arrival flight.
                case DELAY_ARRIVAL_FLIGHT_INT:
                    ProcessDelayArrivalFlight();
                    break;

                // Delay a departure flight.
                case DELAY_DEPARTURE_FLIGHT_INT:
                    ProcessDelayDepartureFlight();
                    break;

                // View all flights.
                case SEE_ALL_FLIGHT_DETAILS_INT:
                    ProcessViewArrivalFlights();
                    ProcessViewDepartureFlights();
                    break;

                // Logs the flight manager out.
                case LOGOUT_INT:
                    return false;
                    break;

                // Error message for invalid choices.
                default:
                    menu.DisplayError(AppConsts.INVALID_CHOICE_ERROR);
                    break;
            }

            return true;
        }

        /// <summary>
        /// Changes the flight managers password.
        /// </summary>
        protected override void ProcessChangePassword()
        {
            // Verifies the old password and gets the new one.
            string new_password = menu.DisplayChangePasswordMenu(flight_manager);

            // Changes the password of the flight manager.
            flight_manager.ChangePassword(new_password);
        }

        /// <summary>
        /// Creates an arrival flight based on manager input.
        /// </summary>
        private void ProcessCreateArrivalFlight()
        {
            // Out variables and consts to be used.
            string airline_name, airline_code, city, flight_id, plane_id_num;
            DateTime date_time;
            const string CITIES_MENU_MSG = "Please enter the departing city:";
            const string ASK_FOR_ARRIVAL_DATETIME = "Please enter in the arrival date and time in the format HH:mm dd/MM/yyyy:";

            // Ask for the details about the flight to create.
            menu.CreateFlightMenu(CITIES_MENU_MSG, ASK_FOR_ARRIVAL_DATETIME, out airline_name, out airline_code, out city, out flight_id, out plane_id_num, out date_time);

            // Gets a plane from the database with this airline code and plane id.
            Plane existing_plane = GetPlane(airline_code, plane_id_num);

            // Defining booleans for readability of the if statements.
            bool plane_not_used_yet = existing_plane == null;
            bool plane_available_for_arrival_flight = existing_plane != null && existing_plane.ArrivalDateTime == DateTime.MinValue;

            // If the plane is not part of any current booking.
            if (plane_not_used_yet)
            {
                // Create a new plane and assign an arrival time to it. Add the plane to the database.
                Plane new_plane = new Plane(airline_code, plane_id_num);
                new_plane.AddArrivalDateTime(date_time);
                database.AddPlane(new_plane);

                // Create a flight and add it to the database.
                ArrivalFlight arrival_flight = new ArrivalFlight(airline_name, airline_code, city, flight_id, new_plane);
                database.AddFlight(arrival_flight);

                // Acknowledge the creation of the flight.
                menu.DisplayMessage(arrival_flight.AcknowledgeFlight());
            }

            // If the plane is already being used for a departure flight, assign the plane this arrival flight.
            else if (plane_available_for_arrival_flight)
            {
                // Assign an arrival time to the plane.
                existing_plane.AddArrivalDateTime(date_time);

                // Create a flight and add it to the database.
                ArrivalFlight arrival_flight = new ArrivalFlight(airline_name, airline_code, city, flight_id, existing_plane);
                database.AddFlight(arrival_flight);

                // Acknowledge the creation of the flight.
                menu.DisplayMessage(arrival_flight.AcknowledgeFlight());
            }
            else
            {
                // Error plane has already been assigned an arrival flight.
                menu.DisplayError(existing_plane.ArrivalFlightAlreadyExists());
            }
        }

        /// <summary>
        /// Create a departure flight based on manager input.
        /// </summary>
        private void ProcessCreateDepartureFlight()
        {
            // Out variables and consts to be used.
            string airline_name, airline_code, city, flight_id, plane_id_num;
            DateTime date_time;
            const string CITIES_MENU_MSG = "Please enter the arrival city:";
            const string ASK_FOR_DEPARTURE_DATETIME = "Please enter in the departure date and time in the format HH:mm dd/MM/yyyy:";

            // Ask for the details about the flight to create.
            menu.CreateFlightMenu(CITIES_MENU_MSG, ASK_FOR_DEPARTURE_DATETIME, out airline_name, out airline_code, out city, out flight_id, out plane_id_num, out date_time);

            // Gets a plane from the database with this airline code and plane id.
            Plane existing_plane = GetPlane(airline_code, plane_id_num);

            // Defining booleans for readability of the if statements.
            bool plane_not_used_yet = existing_plane == null;
            bool plane_available_for_departure_flight = existing_plane != null && existing_plane.DepartureDateTime == DateTime.MinValue;

            // If the plane is not part of any current booking.
            if (plane_not_used_yet)
            {
                // Create a new plane and assign an arrival time to it. Add the plane to the database.
                Plane new_plane = new Plane(airline_code, plane_id_num);
                new_plane.AddDepartureDateTime(date_time);
                database.AddPlane(new_plane);

                // Create a flight and add it to the database.
                DepartureFlight departure_flight = new DepartureFlight(airline_name, airline_code, city, flight_id, new_plane);
                database.AddFlight(departure_flight);

                // Acknowledge the creation of the flight.
                menu.DisplayMessage(departure_flight.AcknowledgeFlight());
            }

            // If the plane is already being used for an arrival flight, assign the plane this departure flight.
            else if (plane_available_for_departure_flight)
            {
                // Assign a departure time to the plane.
                existing_plane.AddDepartureDateTime(date_time);

                // Create a flight and add it to the database.
                DepartureFlight departure_flight = new DepartureFlight(airline_name, airline_code, city, flight_id, existing_plane);
                database.AddFlight(departure_flight);

                // Acknowledge the creation of the flight.
                menu.DisplayMessage(departure_flight.AcknowledgeFlight());
            }
            else
            {
                // Error plane has already been assigned a departure flight.
                menu.DisplayError(existing_plane.DepartureFlightAlreadyExists());
            }
        }


        /// <summary>
        /// Delays an arrival flight.
        /// </summary>
        private void ProcessDelayArrivalFlight()
        {
            const string NO_ARRIVAL_FLIGHTS = "The airport does not have any arrival flights.";

            // Get the arrival flights.
            List<ArrivalFlight> arrival_flights = GetArrivalFlights();

            // If arrival flights exist.
            if (arrival_flights.Count != 0)
            {
                // Ask how much the delay should be.
                int flight_choice, minutes_delayed;
                menu.DelayFlightMenu(arrival_flights, out flight_choice, out minutes_delayed);

                // Get the specific flight.
                ArrivalFlight arrival_flight = arrival_flights[flight_choice];

                // Get the specific plane and delay it.
                Plane plane = GetPlane(arrival_flight.PlaneID);
                plane.DelayArrivalTime(minutes_delayed);
            }
            else
            {
                // No flights error message.
                menu.DisplayMessage(NO_ARRIVAL_FLIGHTS);
            }
        }

        /// <summary>
        /// Delays an arrival flight.
        /// </summary>
        private void ProcessDelayDepartureFlight()
        {
            const string NO_DEPARTURE_FLIGHTS = "The airport does not have any departure flights.";

            // Get the departure flights.
            List<DepartureFlight> departure_flights = GetDepartureFlights();

            // If departure flights exist.
            if (departure_flights.Count != 0)
            {
                // Ask how much the delay should be.
                int flight_choice, minutes_delayed;
                menu.DelayFlightMenu(departure_flights, out flight_choice, out minutes_delayed);

                // Get the specific flight.
                DepartureFlight departure_flight = departure_flights[flight_choice];

                // Get the specific plane and delay it.
                Plane plane = GetPlane(departure_flight.PlaneID);
                plane.DelayDepartureTime(minutes_delayed);
            }
            else
            {
                // No flights error message.
                menu.DisplayMessage(NO_DEPARTURE_FLIGHTS);
            }
        }

        /// <summary>
        /// Displays all arrival flights.
        /// </summary>
        private void ProcessViewArrivalFlights()
        {
            List<ArrivalFlight> arrival_flights = GetArrivalFlights();
            menu.DisplayFlights(arrival_flights);
        }

        /// <summary>
        /// Displays all departure flights.
        /// </summary>
        private void ProcessViewDepartureFlights()
        {
            List<DepartureFlight> departure_flights = GetDepartureFlights();
            menu.DisplayFlights(departure_flights);
        }

        /// <summary>
        /// Gets a plane using the airline code and plane id number.
        /// </summary>
        /// <param name="airline_code">The airline code.</param>
        /// <param name="plane_id_num">The plane id.</param>
        /// <returns>Returns a plane with the specified parameter values.</returns>
        private Plane GetPlane(string airline_code, string plane_id_num)
        {
            // The user input is combined to get the plane id.
            string new_plane_id = airline_code + plane_id_num;

            // Gets all planes in the database.
            ReadOnlyCollection<Plane> planes = database.ReadOnlyPlanesList;
            foreach (Plane plane in planes)
            {
                // Checks all the plane id's to look for a match.
                if (plane.PlaneID == new_plane_id)
                {
                    // Return the matching plane.
                    return plane;
                }
            }

            // Otherwise return null.
            return null;
        }

        /// <summary>
        /// Gets a plane using the plane id.
        /// </summary>
        /// <param name="plane_id">The plane id.</param>
        /// <returns>Returns a plane with the smae plane id.</returns>
        private Plane GetPlane(string plane_id)
        {
            // Gets all planes in the database.
            ReadOnlyCollection<Plane> planes = database.ReadOnlyPlanesList;
            foreach (Plane plane in planes)
            {
                // Checks all the plane id's to look for a match.
                if (plane.PlaneID == plane_id)
                {
                    // Return the matching plane.
                    return plane;
                }
            }
            
            // Otherwise return null.
            return null;
        }
    }
}