using System;

namespace BNE_Airport_App
{
    /// <summary>
    /// An absrtact base class to represent flights that can be created and booked. Flights have an associated plane, only two flights
    /// share a plane, one arrival and one departure flight.
    /// 
    /// Responsibility: To represent a flight.
    /// </summary>
    public abstract class Flight
    {
        /// <summary>
        /// The airline name.
        /// </summary>
        protected string airline_name;

        /// <summary>
        /// The airline code.
        /// </summary>
        protected string airline_code;

        /// <summary>
        /// The associated city.
        /// </summary>
        protected string city;

        /// <summary>
        /// The flight id.
        /// </summary>
        protected string flight_id;

        /// <summary>
        /// The plane assigned to this flight.
        /// </summary>
        protected Plane plane;

        /// <summary>
        /// A getter for the generic plane id associated with an arrival and departure flight.
        /// </summary>
        public string PlaneID
        {
            get { return plane.PlaneID; }
        }

        /// <summary>
        /// A getter for the city.
        /// </summary>
        public string City
        {
            get { return city; }
        }

        /// <summary>
        /// A getter so that arrival and departure flight date and times can be accessed.
        /// </summary>
        public abstract DateTime DateTime
        {
            get;
        }

        /// <summary>
        /// A getter so that the specific arrival or departure plane id can be accessed.
        /// </summary>
        public abstract string UniquePlaneID
        {
            get;
        }


        /// <summary>
        /// A constructor for a flight.
        /// </summary>
        /// <param name="airline_name">The airline name.</param>
        /// <param name="airline_code">The airline code.</param>
        /// <param name="city">The associated city.</param>
        /// <param name="flight_id">The flight id.</param>
        /// <param name="plane">The assigned plane.</param>
        public Flight(string airline_name, string airline_code, string city, string flight_id, Plane plane)
        {
            this.airline_name = airline_name;
            this.airline_code = airline_code;
            this.city = city;
            this.flight_id = airline_code + flight_id;
            this.plane = plane;
        }

        /// <summary>
        /// A message to acknowledge the flight has been created.
        /// </summary>
        /// <returns>A string confirming the flights creation.</returns>
        public abstract string AcknowledgeFlight();

        /// <summary>
        /// A message congratulating the booking of a flight.
        /// </summary>
        /// <returns>A string congratulating a traveller.</returns>
        public abstract string CongratulateBooking();

        /// <summary>
        /// A message showing the specifc booked flight details.
        /// </summary>
        /// <returns>A string with specific flight details.</returns>
        public abstract string ShowBooking();

        /// <summary>
        /// A message show details about the flight.
        /// </summary>
        /// <returns>A string showing all details about the flight.</returns>
        public abstract string ToString();
    }
}