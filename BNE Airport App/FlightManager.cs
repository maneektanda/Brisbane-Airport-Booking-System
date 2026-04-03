using System;

namespace BNE_Airport_App
{
    /// <summary>
    /// A class representing a flight manager. They can create, delay and view all flights.
    /// 
    /// Respnsibility: Represent a flight manager.
    /// </summary>
    public class FlightManager : User
    {
        /// <summary>
        /// The manager staff id.
        /// </summary>
        protected string staff_id;

        /// <summary>
        /// A constructor for a flight manager.
        /// </summary>
        /// <param name="name">The flight managers name.</param>
        /// <param name="age">The flight managers age.</param>
        /// <param name="mobile">The flight managers mobile.</param>
        /// <param name="email">The flight managers email.</param>
        /// <param name="password">The flight managers password.</param>
        public FlightManager(string name, int age, string mobile, string email, string password, string staff_id)
            : base(name, age, mobile, email, password)
        {
            this.staff_id = staff_id;
        }

        /// <summary>
        /// Create a string version of the flight manager that is human readable.
        /// </summary>
        /// <returns>A string representation of the flight manager.</returns>
        public override string ToString()
        {
            return base.ToString() + $"""
            
            Staff ID: {staff_id}
            """;
        }

        /// <summary>
        /// Create a string to congratulate the flight manager.
        /// </summary>
        /// <returns>A string congratulating the flight manager.</returns>
        public override string CongratulateUser()
        {
            return base.CongratulateUser() + "flight manager.";
        }
    }

}