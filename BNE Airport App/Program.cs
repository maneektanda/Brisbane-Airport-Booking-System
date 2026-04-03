using System;
using System.Globalization;
using System.Collections.ObjectModel;

namespace BNE_Airport_App
{
    /// <summary>
    /// This is the entry into the entire program.
    /// 
    /// A flight management and booking system for Brisbane airport. The program allows a user to create different accounts, each with different functionalities and priveleges.
    /// All users can view their details and change their passwords.
    /// A manager can create, delay, and view all flights.
    /// A traveller can book an available seat on a flight.
    /// A frequent flyer can book any seat on a flight.
    /// 
    /// Responsibility: Run the entire program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main program.
        /// </summary>
        /// <param name="args">An array of command line arguments. Not used.</param>
        static void Main(string[] args)
        {
            // Creates the main controller and calls the Run method to start the program.
            AppController app = new AppController();
            app.Run();
        }
    }
}