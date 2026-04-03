using System;

namespace BNE_Airport_App
{
    /// <summary>
    /// This class represents a plane that can be associated with one arrival and one departure flight only.
    /// It links arrival and departure flights together so when a flight manager delays a flight, the corresponding
    /// flight that is also relying on this plane gets it's date and time delayed too.
    /// 
    /// Limitations- for the purpose of this assessment, only delaying the arrival flight delays the departure flight
    /// by the same amount. However, if a plane were to have a return trip planned to another airport, and their
    /// departure time from Brisbane was delayed, their subsequent arrival time back in Brisbane would also need to
    /// be delayed. This hasn't been accounted for in this program, but would simply require a check to see if the
    /// scheduled departure time is before the arrival time, and if the turnaround time at the other airport is less
    /// than that of the delayed departure.
    /// 
    /// 
    /// Responsibility: To represent a plane on a booking.
    /// </summary>
    public class Plane
    {
        /// <summary>
        /// The airline code of the plane.
        /// </summary>
        private string airline_code;

        /// <summary>
        /// The plane id of the plane.
        /// </summary>
        private string plane_id;

        /// <summary>
        /// The arrival date and time of the plane.
        /// </summary>
        private DateTime arrival_date_time;

        /// <summary>
        /// The departure date and time of the plane.
        /// </summary>
        private DateTime departure_date_time;

        /// <summary>
        /// A constructor for the plane.
        /// </summary>
        /// <param name="airline_code">The airline code.</param>
        /// <param name="plane_id">The plane id.</param>
        public Plane(string airline_code, string plane_id)
        {
            this.airline_code = airline_code;
            this.plane_id = plane_id;
        }

 
        /// <summary>
        /// A getter that returns the generic plane id.
        /// </summary>
        public string PlaneID
        {
            get { return airline_code + plane_id; }
        }
        
        /// <summary>
        /// A getter that returns the specifc arrival plane id.
        /// </summary>
        public string ArrivalID
        {
            get { return airline_code + plane_id + "A"; }
        }

        /// <summary>
        /// A getter that returns the specifc departure plane id.
        /// </summary>
        public string DepartureID
        {
            get { return airline_code + plane_id + "D"; }
        }

        /// <summary>
        /// A getter that returns the arrival date and time.
        /// </summary>
        public DateTime ArrivalDateTime
        {
            get { return arrival_date_time; }
        }

        /// <summary>
        /// A getter that returns the departure date and time.
        /// </summary>
        public DateTime DepartureDateTime
        {
            get { return departure_date_time; }
        }

        /// <summary>
        /// Assigns an arrival date and time to the plane.
        /// </summary>
        /// <param name="arrival_date_time">The assigned arrival date and time.</param>
        public void AddArrivalDateTime(DateTime arrival_date_time)
        {
            this.arrival_date_time = arrival_date_time;
        }

        /// <summary>
        /// Assigns a departure date and time to the plane.
        /// </summary>
        /// <param name="departure_date_time"></param>
        public void AddDepartureDateTime(DateTime departure_date_time)
        {
            this.departure_date_time = departure_date_time;
        }

        /// <summary>
        /// Delays the arrival date and time of the plane. The departure time is delayed the same amount.
        /// </summary>
        /// <param name="minutes_delayed">The number of minutes delayed.</param>
        public void DelayArrivalTime(int minutes_delayed)
        {
            arrival_date_time = arrival_date_time.AddMinutes(minutes_delayed);
            departure_date_time = departure_date_time.AddMinutes(minutes_delayed);
        }

        /// <summary>
        /// Delays the departure date and time of the plane.
        /// </summary>
        /// <param name="minutes_delayed">The number of minutes delayed.</param>
        public void DelayDepartureTime(int minutes_delayed)
        {
            departure_date_time = departure_date_time.AddMinutes(minutes_delayed);
        }

        /// <summary>
        /// Returns a string indicating that this plane already has an arrival flight.
        /// </summary>
        /// <returns>A message to say that this plane is not available.</returns>
        public string ArrivalFlightAlreadyExists()
        {
            return $"Plane {ArrivalID} has already been assigned to an arrival flight";
        }

        /// <summary>
        /// Returns a string indicating that this plane already has an departure flight.
        /// </summary>
        /// <returns>A message to say that this plane is not available.</returns>
        public string DepartureFlightAlreadyExists()
        {
            return $"Plane {DepartureID} has already been assigned to a departure flight";
        }
    }
}