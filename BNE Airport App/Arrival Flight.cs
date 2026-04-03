using System;

namespace BNE_Airport_App
{
    /// <summary>
    /// A child class of the abstract flight class that represents an arrival flight.
    /// 
    /// The fields are the same, however the formatting of the information is different. For instance,
    /// the date and time is specifically the arrival date and time.
    /// 
    /// Responsibility: Represents an arrival flight.
    /// </summary>
    public class ArrivalFlight : Flight, IComparable<ArrivalFlight>
    {
        /// <summary>
        /// A constructor for an arrival flight.
        /// </summary>
        /// <param name="airline_name">The airline name.</param>
        /// <param name="airline_code">The airline code.</param>
        /// <param name="city">The arrival city.</param>
        /// <param name="flight_id">The flight id.</param>
        /// <param name="plane">The assigned plane.</param>
        public ArrivalFlight(string airline_name, string airline_code, string city, string flight_id, Plane plane)
            : base(airline_name, airline_code, city, flight_id, plane)
        {

        }

        /// <summary>
        /// A getter to return the associted planes arrival time for this flight.
        /// </summary>
        public override DateTime DateTime
        {
            get { return plane.ArrivalDateTime; }
        }

        /// <summary>
        /// A getter to return the associated planes arrival plane id for this flight.
        /// </summary>
        public override string UniquePlaneID
        {
            get { return plane.ArrivalID; }
        }

        /// <summary>
        /// Implementing IComparable to allow flights to be sorted based on their assigned planes arrival date and time.
        /// </summary>
        /// <param name="flight">The flight to compare to.</param>
        /// <returns>An integer that indicates the relative order of this flight and the compared flight.</returns>
        public int CompareTo(ArrivalFlight flight)
        {
            if (flight == null)
            {
                return 1;
            }
            else
            {
                return this.plane.ArrivalDateTime.CompareTo(flight.plane.ArrivalDateTime);
            }
        }

        /// <summary>
        /// Acknowledges the flight has been created.
        /// </summary>
        /// <returns>A string confirming the flights creation.</returns>
        public override string AcknowledgeFlight()
        {
            return $"Flight {flight_id} on plane {plane.ArrivalID} has been added to the system.";
        }

        /// <summary>
        /// Congratulates a traveller that they have booked this flight.
        /// </summary>
        /// <returns>A string with specific details about this flight.</returns>
        public override string CongratulateBooking()
        {
            return $"Congratulations. You have booked flight {flight_id} from {city} arriving at {plane.ArrivalDateTime.ToString(AppConsts.DATETIME_FORMAT)} and are seated in ";
        }

        /// <summary>
        /// Shows the specific booking that has been made on this flight.
        /// </summary>
        /// <returns>A string showing details of the booking.</returns>
        public override string ShowBooking()
        {
            return $"Flight {flight_id} from {city} arriving at {plane.ArrivalDateTime.ToString(AppConsts.DATETIME_FORMAT)} in seat ";
        }

        /// <summary>
        /// Shows specific information about the flight.
        /// </summary>
        /// <returns>A string showing flight information.</returns>
        public override string ToString()
        {
            return $"Flight {flight_id} operated by {this.airline_name} arriving at {plane.ArrivalDateTime.ToString(AppConsts.DATETIME_FORMAT)} from {city} on plane {plane.ArrivalID}.";
        }
    }
}