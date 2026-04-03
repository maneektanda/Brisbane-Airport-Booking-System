using System;

namespace BNE_Airport_App
{
    /// <summary>
    /// The most basic user type that can book and view flights.
    /// 
    /// Responsibility: Represent a traveller.
    /// </summary>
    public class Traveller : User
    {
        /// <summary>
        /// The travellers arrival booking.
        /// </summary>
        protected Booking? arrival_booking;

        /// <summary>
        /// The travellers departure booking.
        /// </summary>
        protected Booking? departure_booking;

        /// <summary>
        /// A constructor for a traveller.
        /// </summary>
        /// <param name="name">The travellers name.</param>
        /// <param name="age">The travellers age.</param>
        /// <param name="mobile">The travellers mobile.</param>
        /// <param name="email">The travellers email.</param>
        /// <param name="password">The travellers password.</param>
        public Traveller(string name, int age, string mobile, string email, string password)
            : base(name, age, mobile, email, password)
        {

        }
        
        /// <summary>
        /// A getter for the arrival date and time of their booking.
        /// </summary>
        public DateTime? ArrivalDateTime
        {
            get { return arrival_booking?.DateTime; }
        }

        /// <summary>
        /// A getter for the departure date and time of their booking.
        /// </summary>
        public DateTime? DepartureDateTime
        {
            get { return departure_booking?.DateTime; }
        }

        /// <summary>
        /// Adds an arrival booking to the traveller.
        /// </summary>
        /// <param name="arrival_booking">The booking to add.</param>
        public void AddArrivalBooking(Booking arrival_booking)
        {
            this.arrival_booking = arrival_booking;
        }

        /// <summary>
        /// Adds a departure booking to the traveller.
        /// </summary>
        /// <param name="departure_booking">The booking to add.</param>
        public void AddDepartureBooking(Booking departure_booking)
        {
            this.departure_booking = departure_booking;
        }

        /// <summary>
        /// Create a string to congratulate the traveller.
        /// </summary>
        /// <returns>A string congratulating the traveller.</returns>
        public override string CongratulateUser()
        {
            return base.CongratulateUser() + "traveller.";
        }

        /// <summary>
        /// Create a string to congratulate the traveller on their arrival booking..
        /// </summary>
        /// <returns>A string congratulating the traveller.</returns>
        public string CongratulateArrivalBooking()
        {
            return arrival_booking.CongratulateBooking();
        }

        /// <summary>
        /// Create a string to congratulate the traveller on their departure booking..
        /// </summary>
        /// <returns>A string congratulating the traveller.</returns>
        public string CongratulateDepartureBooking()
        {
            return departure_booking.CongratulateBooking();
        }

        /// <summary>
        /// Create a string to show the traveller their arrival booking..
        /// </summary>
        /// <returns>A string showing the arrival booking.</returns>
        public string ShowArrivalBooking()
        {
            return "Arrival Flight: " + arrival_booking.ShowBooking();
        }

        /// <summary>
        /// Create a string to show the traveller their departure booking..
        /// </summary>
        /// <returns>A string showing the departure booking.</returns>
        public string ShowDepartureBooking()
        {
            return "Departure Flight: " + departure_booking.ShowBooking();
        }

    }
}