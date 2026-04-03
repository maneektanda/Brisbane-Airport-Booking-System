using System;
using System.Collections.ObjectModel;

namespace BNE_Airport_App
{
    /// <summary>
    /// A class that represents a frequent flyer controller. They have all the traveller functionality but they can also book any seat
    /// that they want, even if it's already booked. They also accumulate points for each flight they take.
    /// 
    /// Responsibility: Handles a logged in frequent flyer.
    /// </summary>
    public class FrequentFlyerController : TravellerController
    {
        /// <summary>
        /// The logged in frequent flyer.
        /// </summary>
        private FrequentFlyer frequent_flyer;

        /// <summary>
        /// A constructor for the controller.
        /// </summary>
        /// <param name="logged_in_frequent_flyer">The specific logged in frequent flyer.</param>
        /// <param name="database">The database with all the data.</param>
        public FrequentFlyerController(FrequentFlyer logged_in_frequent_flyer, Database database)
            : base(logged_in_frequent_flyer, database)
        {
            this.frequent_flyer = logged_in_frequent_flyer;

        }

        /// <summary>
        /// This method is called when the Run() method is called from the base class.
        /// Presents the frequent flyer with all their options.
        /// </summary>
        /// <returns>A list of functions that the traveller can select.</returns>
        protected override bool ProcessUserMenu()
        {
            // Define a const unique to this controller to be used in the below list.
            const string SEE_FREQUENT_FLYER_POINTS_STR = "See frequent flyer points";

            // A list of options to be selected.
            List<string> options = new List<string>();
            options.Add(AppConsts.SEE_PERSONAL_DETAILS_STR);
            options.Add(AppConsts.CHANGE_PASSWORD_STR);
            options.Add(AppConsts.BOOK_ARRIVAL_FLIGHT_STR);
            options.Add(AppConsts.BOOK_DEPARTURE_FLIGHT_STR);
            options.Add(AppConsts.SEE_FLIGHT_DETAILS_STR);
            options.Add(SEE_FREQUENT_FLYER_POINTS_STR);
            options.Add(AppConsts.LOGOUT_STR);

            // Consts to represent options selected that are again unique to this controller.
            const int SEE_FREQUENT_FLYER_POINTS_INT = 5;
            const int LOGOUT_INT = 6;

            const string FREQUENT_FLYER_MENU_TITLE = "Frequent Flyer Menu.";

            // Gets the option from the frequent flyer to determine what they want to do.
            int option = menu.DisplayUserMenu(FREQUENT_FLYER_MENU_TITLE, AppConsts.USER_MENU_MSG, options);

            switch (option)
            {
                // Shows their details.
                case AppConsts.SEE_PERSONAL_DETAILS_INT:
                    menu.DisplayMessage(frequent_flyer);
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

                // Allows them to view their points.
                case SEE_FREQUENT_FLYER_POINTS_INT:
                    ShowFrequentFlyerPoints();
                    break;

                // Logs the frequent flyer out.
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
        /// Shows the points of the frequent flyer, and any points they'll be getting from flights they 
        /// have booked.
        /// </summary>
        private void ShowFrequentFlyerPoints()
        {
            // Shows the current number of points.
            menu.DisplayMessage(frequent_flyer.GetFrequentFlyerPoints());

            // Define some booleans to increase readability of the if statements below.
            bool has_arrival_booking = frequent_flyer.ArrivalDateTime != null;
            bool has_departure_booking = frequent_flyer.DepartureDateTime != null;
            bool has_one_booking = frequent_flyer.ArrivalDateTime != null ^ frequent_flyer.DepartureDateTime != null;
            bool has_two_bookings = frequent_flyer.ArrivalDateTime != null && frequent_flyer.DepartureDateTime != null;

            // A running total to add new points to.
            int new_total = frequent_flyer.FrequentFlyerPoints;

            // If they have an arrival booking, show the associated points.
            if (has_arrival_booking)
            {
                // Get the flight and the associated points from the dictionary.
                const string ARRIVAL_POINTS = "Your points from your arrival flight will be :";
                int new_arrival_points = AppConsts.FrequentFlyerPoints[frequent_flyer.ArrivalCity];

                // Show the formatted points and add them to the total.
                menu.DisplayMessage($"{ARRIVAL_POINTS} {new_arrival_points:N0}.");
                new_total += new_arrival_points;
            }

            // If they have a departure booking, show the associated points.
            if (has_departure_booking)
            {
                // Get the flight and the associated points from the dictionary.
                const string DEPARTURE_POINTS = "Your points from your departure flight will be:";
                int new_departure_points = AppConsts.FrequentFlyerPoints[frequent_flyer.DepartureCity];

                // Show the formatted points and add them to the total.
                menu.DisplayMessage($"{DEPARTURE_POINTS} {new_departure_points:N0}.");
                new_total += new_departure_points;
            }

            // If they only have one booking, show the total and the singular version of the message.
            if (has_one_booking)
            {
                const string ONE_FLIGHT = "After completing your flight your new points will be:";
                menu.DisplayMessage($"{ONE_FLIGHT} {new_total:N0}.");
            }

            // If they have two booking, show the total and the plural version of the message.
            if (has_two_bookings)
            {
                const string TWO_FLIGHTS = "After completing your flights your new points will be:";
                menu.DisplayMessage($"{TWO_FLIGHTS} {new_total:N0}.");
            }

        }

        /// <summary>
        /// A method to override the base method of booking an arrival flight.
        /// Allows the frequent flyer to book any seat they want and automatically
        /// reassigns a new seat to the other booking.
        /// </summary>
        /// <param name="arrival_flight">The chosen flight.</param>
        /// <param name="seat">The chosen seat.</param>
        /// <returns>Returns an arrival booking.</returns>
        protected override Booking MakeBooking(ArrivalFlight arrival_flight, Seat seat)
        {
            // Makes a booking with the arrival flight and chosen seat.
            Booking arrival_booking = new Booking(arrival_flight, seat);

            // If the seat is taken, find that person a new seat on the same flight and update their booking.
            if (SeatIsTaken(arrival_booking))
            {
                // Finds the booking that is already in the database that the frequent flyer has now booked.
                Booking existing_booking = GetExistingBooking(arrival_booking);

                // Assigns a new seat to that booking.
                AssignNewSeat(existing_booking);
            }

            // Return the original booking.
            return arrival_booking;
        }
        
        /// <summary>
        /// A method to override the base method of booking a departure flight.
        /// Allows the frequent flyer to book any seat they want and automatically
        /// reassigns a new seat to the other booking.
        /// </summary>
        /// <param name="departure_flight">The chosen flight.</param>
        /// <param name="seat">The chosen seat.</param>
        /// <returns>Returns a departure booking.</returns>
        protected override Booking MakeBooking(DepartureFlight departure_flight, Seat seat)
        {
            // Makes a booking with the departure flight and chosen seat.
            Booking departure_booking = new Booking(departure_flight, seat);

            // If the seat is taken, find that person a new seat on the same flight and update their booking.
            if (SeatIsTaken(departure_booking))
            {
                // Finds the booking that is already in the database that the frequent flyer has now booked.
                Booking existing_booking = GetExistingBooking(departure_booking);

                // Assigns a new seat to that booking.
                AssignNewSeat(existing_booking);
            }

            // Return the original booking.
            return departure_booking;
        }
        

        /// <summary>
        /// Checks the number of mathcing bookings in the database.
        /// </summary>
        /// <param name="existing_booking">The booking you want to check.</param>
        /// <returns>Returns an integer of the number of matches.</returns>
        private int NumberOfMatchingBookings(Booking existing_booking)
        {
            // A counter for the number of bookings that match.
            int number_of_bookings = 0;

            // Check all bookings in the database.
            foreach (Booking booking in database.ReadOnlyBookingsList)
            {
                // If it matches the existing booking, increment the counter.
                if (booking == existing_booking)
                {
                    number_of_bookings++;
                }
            }

            // Return the value of the counter.
            return number_of_bookings;
        }

        /// <summary>
        /// Finds a matching booking and returns it.
        /// </summary>
        /// <param name="new_booking">The booking to find a match for.</param>
        /// <returns>Returns a booking that has now been booked by a frequent flyer.</returns>
        private Booking GetExistingBooking(Booking new_booking)
        {
            // Check all bookings in the database.
            foreach (Booking existing_booking in database.ReadOnlyBookingsList)
            {
                // If it matches the existing booking, return it.
                if (existing_booking == new_booking)
                {
                    return existing_booking;
                }
            }

            // Return null if no bookings match.
            return null;
        }

        /// <summary>
        /// Assigns a new seat to a booking when a frequent flyer takes their seat.
        /// </summary>
        /// <param name="existing_booking">The booking to be updated.</param>
        private void AssignNewSeat(Booking existing_booking)
        {
            // There are 40 total seats on a plane, therefore max possible alternatives after losing your seat is 39.
            // Because this booking already exists in the database, it will always return a match with itself.
            const int MAX_POSSIBLE_ALTERNATIVES = 39;
            const int MAX_POSSIBLE_MATCHES = 1;

            // Increment the seat.
            existing_booking.IncrementSeat();

            // A counter for the number of alternatives checked. starts at one because the seat was incremented in the line above.
            int number_of_alternatives_checked = 1;

            // If the new booking has more than one match, meaning it found itself and at least one more booking that are equa in
            // the database, then increment the seat again as well as the counter. Keep doing this until you find an available seat
            // or until you've checked all other seats on the plane.
            while (NumberOfMatchingBookings(existing_booking) > MAX_POSSIBLE_MATCHES && number_of_alternatives_checked <= MAX_POSSIBLE_ALTERNATIVES)
            {
                existing_booking.IncrementSeat();
                number_of_alternatives_checked++;
            }
        }
    }
}