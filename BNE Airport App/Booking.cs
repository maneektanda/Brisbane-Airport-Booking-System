using System;

namespace BNE_Airport_App
{
    /// <summary>
    /// Represents a unique booking on a flight by a Traveller. Contains relevent data and methods.
    /// 
    /// Responsibility: Representing a booking.
    /// </summary>
    public class Booking
    {
        // Fields to represent details about a booking.

        /// <summary>
        /// The flight that the booking is for.
        /// </summary>
        private Flight flight;

        /// <summary>
        /// The seat that is booked on the flight.
        /// </summary>
        private Seat seat;

        /// <summary>
        /// A getter that returns the relevant date and time associated with the booking.
        /// </summary>
        public DateTime DateTime
        {
            get { return flight.DateTime; }
        }

        /// <summary>
        /// A getter that returns the relevant city associated with the booking.
        /// </summary>
        public string City
        {
            get { return flight.City; }
        }

        /// <summary>
        /// A constructor for the booking.
        /// </summary>
        /// <param name="flight">The specific flight that the booking is for.</param>
        /// <param name="seat">The specific seat that has been booked on the flight.</param>
        public Booking(Flight flight, Seat seat)
        {
            this.flight = flight;
            this.seat = seat;
        }

        /// <summary>
        /// Overloading the equality operator to make it easy to check if two bookings are the same. This will help
        /// with determining if a booking already exists in the database. Comparing the ShowBooking() method of two bookings
        /// is a simple proxy for equality in this instance.
        /// 
        /// The inequality operator must then also be overloaded.
        /// </summary>
        /// <param name="booking1">The first booking to compare.</param>
        /// <param name="booking2">The second booking to compare.</param>
        /// <returns>True if the bookings are the same, false otherwise</returns>
        public static bool operator ==(Booking booking1, Booking booking2)
        {
            // If these values are the same, the two bookings are the same.
            if (booking1.ShowBooking() == booking2.ShowBooking())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(Booking booking1, Booking booking2)
        {
            // If these values are different, the two bookings are different.
            if (booking1.ShowBooking() != booking2.ShowBooking())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Congratulates a traveller for making this booking.
        /// </summary>
        /// <returns>A formatted string about the specific flight and seat of the booking.</returns>
        public string CongratulateBooking()
        {
            return $"{flight.CongratulateBooking() + seat.Value}.";
        }

        /// <summary>
        /// Shows details about the booking.
        /// </summary>
        /// <returns>A formatted string showing all relevant details about the flight and selected seat of the booking.</returns>
        public string ShowBooking()
        {
            return $"{flight.ShowBooking() + seat.Value}.";
        }

        /// <summary>
        /// Increments the seat of this booking by one seat.
        /// </summary>
        public void IncrementSeat()
        {
            seat++;
        }


    }
}