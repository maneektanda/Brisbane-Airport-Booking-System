using System;
using System.Collections.ObjectModel;

namespace BNE_Airport_App
{
    /// <summary>
    /// The controller specifically for travellers. Allows them to see details, change password
    /// book flights and see flight details. Has all the functionality of the base class.
    /// 
    /// Responsibility: Handles a logged in traveller.
    /// </summary>
    public class TravellerController : UserController
    {
        /// <summary>
        /// The specific logged in traveller.
        /// </summary>
        private Traveller traveller;

        /// <summary>
        /// A constructor for the controller.
        /// </summary>
        /// <param name="logged_in_traveller">The specific logged in traveller.</param>
        /// <param name="database">The database with all the data.</param>
        public TravellerController(Traveller logged_in_traveller, Database database)
            : base(database)
        {
            this.traveller = logged_in_traveller;

        }

        /// <summary>
        /// This method is called when the Run() method is called from the base class.
        /// Presents the traveller with all their options.
        /// </summary>
        /// <returns>A list of functions that the traveller can select.</returns>
        protected override bool ProcessUserMenu()
        {
            // A list of options to be selected.
            List<string> options = new List<string>();
            options.Add(AppConsts.SEE_PERSONAL_DETAILS_STR);
            options.Add(AppConsts.CHANGE_PASSWORD_STR);
            options.Add(AppConsts.BOOK_ARRIVAL_FLIGHT_STR);
            options.Add(AppConsts.BOOK_DEPARTURE_FLIGHT_STR);
            options.Add(AppConsts.SEE_FLIGHT_DETAILS_STR);
            options.Add(AppConsts.LOGOUT_STR);

            // Define some constants for readability.
            const int LOGOUT_INT = 5;

            const string TRAVELLER_MENU_TITLE = "Traveller Menu.";

            // Gets the option from the traveller to determine what they want to do.
            int option = menu.DisplayUserMenu(TRAVELLER_MENU_TITLE, AppConsts.USER_MENU_MSG, options);

            switch (option)
            {
                // Shows their details.
                case AppConsts.SEE_PERSONAL_DETAILS_INT:
                    menu.DisplayMessage(traveller);
                    break;

                // Allows them to change their password.
                case AppConsts.CHANGE_PASSWORD_INT:
                    ProcessChangePassword();
                    break;

                // Allows them to book an arrival flight.
                case AppConsts.BOOK_ARRIVAL_FLIGHT_INT:
                    ProcessBookArrivalFlight();
                    break;

                // Allows them to book a departure flight.
                case AppConsts.BOOK_DEPARTURE_FLIGHT_INT:
                    ProcessBookDepartureFlight();
                    break;

                // Allows them to see their flight details.
                case AppConsts.SEE_FLIGHT_DETAILS_INT:
                    ProcessSeeFlightDetails();
                    break;

                // Logs the traveler out.
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
        /// Changes the travellers password.
        /// </summary>
        protected override void ProcessChangePassword()
        {
            // Verifies the old password and gets the new one.
            string new_password = menu.DisplayChangePasswordMenu(traveller);

            // Changes the password of the traveller.
            traveller.ChangePassword(new_password);
        }

        /// <summary>
        /// Allows a traveller to book an arrival flight.
        /// </summary>
        protected void ProcessBookArrivalFlight()
        {
            // Check if the traveller has already booked an arrival flight. To avoid revealing unnecessary
            // information about the traveller. The existance of an arrival time can be used as a proxy for
            // an arrival booking.
            if (traveller.ArrivalDateTime == null)
            {
                // Get all the arrival flights from the database.
                List<ArrivalFlight> arrival_flights = GetArrivalFlights();

                // Check that arrival flights exist.
                if (arrival_flights.Count != 0)
                {
                    // Get the chosen flight from a menu.
                    ArrivalFlight arrival_flight = GetFlightMenu(arrival_flights, traveller);

                    // Gets the users chosen seat.
                    Seat seat = GetSeat();

                    // Makes a booking using the chosen flight and seat.
                    Booking arrival_booking = MakeBooking(arrival_flight, seat);

                    // Adds the booking to the database and assigns it to the traveller.
                    database.AddBooking(arrival_booking);
                    traveller.AddArrivalBooking(arrival_booking);

                    // Congratulates the traveller.
                    menu.DisplayMessage(traveller.CongratulateArrivalBooking());
                }

                // If there's no arrival flights available, show this error message.
                else
                {
                    const string NO_ARRIVAL_FLIGHTS = "The airport does not have any arrival flights.";
                    menu.DisplayMessage(NO_ARRIVAL_FLIGHTS);
                }
            }

            // It the traveller has already booked an arrival flight, show this error message.
            else
            {
                const string ALREADY_HAS_ARRIVAL_FLIGHT = "You already have an arrival flight. You can not book another";
                menu.DisplayError(ALREADY_HAS_ARRIVAL_FLIGHT);
            }
        }

        /// <summary>
        /// Allows a traveller to book a departure flight.
        /// </summary>
        protected void ProcessBookDepartureFlight()
        {
            // Check if the traveller has already booked a departure flight. To avoid revealing unnecessary
            // information about the traveller. The existance of a depature time can be used as a proxy for
            // a departure booking.
            if (traveller.DepartureDateTime == null)
            {
                // Get all the departure flights from the database.
                List<DepartureFlight> departure_flights = GetDepartureFlights();

                // Check that departure flights exist.
                if (departure_flights.Count != 0)
                {
                    // Get the chosen flight from a menu.
                    DepartureFlight departure_flight = GetFlightMenu(departure_flights, traveller);

                    // Gets the users chosen seat.
                    Seat seat = GetSeat();

                    // Makes a booking using the chosen flight and seat.
                    Booking departure_booking = MakeBooking(departure_flight, seat);

                    // Adds the booking to the database and assigns it to the traveller.
                    database.AddBooking(departure_booking);
                    traveller.AddDepartureBooking(departure_booking);

                    // Congratulates the traveller.
                    menu.DisplayMessage(traveller.CongratulateDepartureBooking());
                }

                // If there's no departure flights available, show this error message.
                else
                {
                    const string NO_DEEPARTURE_FLIGHTS = "The airport does not have any departure flights.";
                    menu.DisplayMessage(NO_DEEPARTURE_FLIGHTS);
                }
            }

            // It the traveller has already booked a departure flight, show this error message.
            else
            {
                const string ALREADY_HAS_DEPARTURE_FLIGHT = "You already have a departure flight. You can not book another";
                menu.DisplayError(ALREADY_HAS_DEPARTURE_FLIGHT);
            }
        }

        /// <summary>
        /// Shows the flight details for a traveller.
        /// </summary>
        protected void ProcessSeeFlightDetails()
        {
            menu.DisplayMessage($"Showing flight details for {traveller.Name}:");

            // If the user has an arrival flight, show the arrival booking.
            if (traveller.ArrivalDateTime != null)
            {
                menu.DisplayMessage(traveller.ShowArrivalBooking());
            }

            // If the user has a departure flight, show the departure booking.
            if (traveller.DepartureDateTime != null)
            {
                menu.DisplayMessage(traveller.ShowDepartureBooking());
            }
        }

        /// <summary>
        /// Gets the traveller to choose a flight.
        /// </summary>
        /// <param name="arrival_flights">List of flight options.</param>
        /// <param name="traveller">The traveller trying to book the flight.</param>
        /// <returns>A chosen arrival flight.</returns>
        protected ArrivalFlight GetFlightMenu(List<ArrivalFlight> arrival_flights, Traveller traveller)
        {
            // A menu to ask the traveller to choose an available flight.
            const string ARRIVAL_FLIGHT_MENU_MSG = "Please enter the arrival flight:";
            int flight_choice = menu.GetFlightOption(ARRIVAL_FLIGHT_MENU_MSG, arrival_flights);

            // Assigning the choice to the instance of that flight.
            ArrivalFlight arrival_flight = arrival_flights[flight_choice];

            // While the chosen arrival flight is after the travellers departure flight,
            // get them to choose again.
            while (traveller.DepartureDateTime != null && traveller.DepartureDateTime <= arrival_flight.DateTime)
            {
                // Show this error message.
                const string DATE_TIME_TOO_LATE = "The arrival time must be before the departure time";
                menu.DisplayTryAgainError(DATE_TIME_TOO_LATE);

                // Ask them to choose a flight again, and re-assign their choice. 
                flight_choice = menu.GetFlightOption(ARRIVAL_FLIGHT_MENU_MSG, arrival_flights);
                arrival_flight = arrival_flights[flight_choice];
            }

            // Return the valid arrival flight choice.
            return arrival_flight;
        }

        /// <summary>
        /// Gets the traveller to choose a flight.
        /// </summary>
        /// <param name="departure_flights">List of flight options.</param>
        /// <param name="traveller">The traveller trying to book the flight.</param>
        /// <returns>A chosen departure flight.</returns>
        protected DepartureFlight GetFlightMenu(List<DepartureFlight> departure_flights, Traveller traveller)
        {

            // A menu to ask the traveller to choose an available flight.
            const string DEPARTURE_FLIGHT_MENU_MSG = "Please enter the departure flight:";
            int flight_choice = menu.GetFlightOption(DEPARTURE_FLIGHT_MENU_MSG, departure_flights);

            // Assigning the choice to the instance of that flight.
            DepartureFlight departure_flight = departure_flights[flight_choice];

            // While the chosen departure flight is before the travellers arrival flight,
            // get them to choose again.
            while (traveller.ArrivalDateTime != null && traveller.ArrivalDateTime >= departure_flight.DateTime)
            {
                // Show this error message.
                const string DATE_TIME_TOO_LATE = "The departure time must be after the arrival time";
                menu.DisplayTryAgainError(DATE_TIME_TOO_LATE);

                // Ask them to choose a flight again, and re-assign their choice. 
                flight_choice = menu.GetFlightOption(DEPARTURE_FLIGHT_MENU_MSG, departure_flights);
                departure_flight = departure_flights[flight_choice];
            }

            // Return the valid departure flight choice.
            return departure_flight;
        }

        /// <summary>
        /// Makes a booking with the chosen flight and seat.
        /// </summary>
        /// <param name="arrival_flight">The chosen flight.</param>
        /// <param name="seat">The chosen seat.</param>
        /// <returns>Returns an arrival booking.</returns>
        protected virtual Booking MakeBooking(ArrivalFlight arrival_flight, Seat seat)
        {
            // Makes a new booking using the flight and seat.
            Booking arrival_booking = new Booking(arrival_flight, seat);

            // If that seat on that flight is already taken, enter this loop.
            while (SeatIsTaken(arrival_booking))
            {
                // Show this error message.
                const string SEAT_IS_OCCUPIED = "Seat is already occupied";
                menu.DisplayTryAgainError(SEAT_IS_OCCUPIED);

                // Ask the traveller to choose another seat.
                seat = GetSeat();

                // Reassigning the seat value in the booking so that it can be checked again.
                arrival_booking = new Booking(arrival_flight, seat);
            }

            // Return the available booking.
            return arrival_booking;
        }

        /// <summary>
        /// Makes a booking with the chosen flight and seat.
        /// </summary>
        /// <param name="departure_flight">The chosen flight.</param>
        /// <param name="seat">The chosen seat.</param>
        /// <returns>Returns a departure booking.</returns>
        protected virtual Booking MakeBooking(DepartureFlight departure_flight, Seat seat)
        {
            // Makes a new booking using the flight and seat.
            Booking departure_booking = new Booking(departure_flight, seat);

            // If that seat on that flight is already taken, enter this loop.
            while (SeatIsTaken(departure_booking))
            {
                // Show this error message.
                const string SEAT_IS_OCCUPIED = "Seat is already occupied";
                menu.DisplayTryAgainError(SEAT_IS_OCCUPIED);

                // Ask the traveller to choose another seat.
                seat = GetSeat();

                // Reassigning the seat value in the booking so that it can be checked again.
                departure_booking = new Booking(departure_flight, seat);
            }

            // Return the available booking.
            return departure_booking;
        }
        
        /// <summary>
        /// Checks if a booking has already been made for a seat on a flight.
        /// </summary>
        /// <param name="new_booking">The new booking trying to be made.</param>
        /// <returns>True if the seat is already taken, false otherwise.</returns>
        protected bool SeatIsTaken(Booking new_booking)
        {
            // A boolean inidicating if an identical booking has been found or not.
            bool seat_is_taken = false;

            // Checks all bookings a readonly list of bookings from the database.
            foreach (Booking booking in database.ReadOnlyBookingsList)
            {
                // Using the implemented operator overloading, if two bookings are the
                // same, then set the value to true.
                if (booking == new_booking)
                {
                    seat_is_taken = true;
                }
            }

            // Return the result.
            return seat_is_taken;
        }

        /// <summary>
        /// Creates a seat.
        /// </summary>
        /// <returns>A new instance of a seat.</returns>
        protected Seat GetSeat()
        {
            int row;
            char column;

            // Get the options from the traveller.
            menu.GetSeatOptionsMenu(out row, out column);
            
            // Create the seat and return it.
            Seat seat = new Seat(row, column);
            return seat;
        }
    }
}