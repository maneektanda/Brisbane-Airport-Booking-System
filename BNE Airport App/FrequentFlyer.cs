using System;

namespace BNE_Airport_App
{
    /// <summary>
    /// A class to represent a frequent flyer. They have all the funcitonality of a traveller, but they accumulate
    /// frequent flyer points with the bookings they make. They can also book any seat on a flight, even if it's
    /// already taken.
    /// 
    /// Responsibility: Represent a frequent flyer.
    /// </summary>
    public class FrequentFlyer : Traveller
    {
        /// <summary>
        /// The frequent flyers member number
        /// </summary>
        protected string frequent_flyer_num;

        /// <summary>
        /// The number of frequent flyer points.
        /// </summary>
        protected int frequent_flyer_points;


        /// <summary>
        /// A constructor for a frequent flyer.
        /// </summary>
        /// <param name="name">The frequent flyers name.</param>
        /// <param name="age">The frequent flyers age.</param>
        /// <param name="mobile">The frequent flyers mobile.</param>
        /// <param name="email">The frequent flyers email.</param>
        /// <param name="password">The frequent flyers password.</param>
        public FrequentFlyer(string name, int age, string mobile, string email, string password, string frequent_flyer_num, int frequent_flyer_points)
            : base(name, age, mobile, email, password)
        {
            this.frequent_flyer_num = frequent_flyer_num;
            this.frequent_flyer_points = frequent_flyer_points;
        }

        /// <summary>
        /// A getter for the arrival city of their booking to work out the associated points.
        /// </summary>
        public string ArrivalCity
        {
            get { return arrival_booking.City; }
        }

        /// <summary>
        /// A getter for the departure city of their booking to work out the associated points.
        /// </summary>
        public string DepartureCity
        {
            get { return departure_booking.City; }
        }

        /// <summary>
        /// A getter to return the number of points.
        /// </summary>
        public int FrequentFlyerPoints
        {
            get { return frequent_flyer_points; }
        }

        /// <summary>
        /// Create a string version of the frequent flyer that is human readable.
        /// </summary>
        /// <returns>A string representation of the frequent flyer.</returns>
        public override string ToString()
        {
            return base.ToString() + $"""

            Frequent flyer number: {frequent_flyer_num}
            Frequent flyer points: {frequent_flyer_points:N0}
            """;
        }

        /// <summary>
        /// Create a string to congratulate the frequent flyer.
        /// </summary>
        /// <returns>A string congratulating the frequent flyer.</returns>
        public override string CongratulateUser()
        {
            return $"Congratulations {name}. You have registered as a frequent flyer.";
        }

        /// <summary>
        /// Create a formatted string with the number of points.
        /// </summary>
        /// <returns>A string shoiwng the number of points.</returns>
        public string GetFrequentFlyerPoints()
        {
            return $"Your current points are: {frequent_flyer_points:N0}.";
        }

    }
}