using System;
using System.Globalization;

namespace BNE_Airport_App
{
    /// <summary>
    /// A class for reading user input or displaying information to the user. It abstracts away the "View" portion of the program. 
    /// </summary>
    public class CmdLineUI
    {  
        public static string GetInput()
        {
            return Console.ReadLine() ?? "";
        }
        public static void DisplayString()
        {
            Console.WriteLine();
        }

        public static void DisplayString(string message)
        {
            Console.WriteLine(message);
        }

        public static void DisplayString(User user)
        {
            Console.WriteLine(user.ToString());
        }

        public static void DisplayString(Flight flight)
        {
            Console.WriteLine(flight.ToString());
        }

        public static void DisplayError(string message)
        {
            Console.WriteLine("#####");
            Console.WriteLine($"# Error - {message}.");
            Console.WriteLine("#####");
        }

        public static void DisplayTryAgainError(string message)
        {
            Console.WriteLine("#####");
            Console.WriteLine($"# Error - {message}.");
            Console.WriteLine("# Please try again.");
            Console.WriteLine("#####");
        }

        public static int GetMenuChoice(string message)
        {
            Console.WriteLine(message);
            string input = GetInput();
            if (InputValidation.IntIsValid(input))
            {
                return Convert.ToInt32(input);
            }
            else
            {
                return -1;
            }
        }

        public static int GetFlightMenuChoice(string message, int list_length)
        {
            const int MIN_VALUE = 1;
            Console.WriteLine(message);
            string input = GetInput();
            while (!InputValidation.IntIsValid(input) || Convert.ToInt32(input) < MIN_VALUE || Convert.ToInt32(input) > list_length)
            {
                const string WRONG_CHOICE = "Supplied value is out of range";
                DisplayTryAgainError(WRONG_CHOICE);
                Console.WriteLine(message);
                input = GetInput();
            }
            int choice = Convert.ToInt32(input);
            return choice;
        }

        public static int GetMinutesDelayed()
        {
            const string INVALID_MINUTES = "Supplied minutes are invalid";
            string input = GetInput();
            while (!InputValidation.IntIsValid(input))
            {
                DisplayTryAgainError(INVALID_MINUTES);
                Console.WriteLine(AppConsts.ASK_FOR_MINUTES_DELAYED);
                input = GetInput();
            }
            int minutes_delayed = Convert.ToInt32(input);
            return minutes_delayed;
        }

        public static string GetName()
        {
            const string INVALID_NAME = "Supplied name is invalid";
            string input = GetInput();
            while (!InputValidation.NameIsValid(input))
            {
                DisplayTryAgainError(INVALID_NAME);
                Console.WriteLine(AppConsts.ASK_FOR_NAME);
                input = GetInput();
            }
            return input;
        }

        public static int GetAge()
        {
            const string INVALID_VALUE = "Supplied value is invalid";
            const string INVALID_AGE = "Supplied age is invalid";
            string input = GetInput();
            while (!InputValidation.IntIsValid(input) || !InputValidation.AgeIsValid(input))
            {
                if (!InputValidation.IntIsValid(input))
                {
                    DisplayTryAgainError(INVALID_VALUE);
                }
                else
                {
                    DisplayTryAgainError(INVALID_AGE);
                }
                Console.WriteLine(AppConsts.ASK_FOR_AGE);
                input = GetInput();
            }
            int age = Convert.ToInt32(input);
            return age;
        }

        public static string GetMobile()
        {
            const string INVALID_MOBILE = "Supplied mobile number is invalid";
            string input = GetInput();
            while (!InputValidation.MobileIsValid(input))
            {
                DisplayTryAgainError(INVALID_MOBILE);
                Console.WriteLine(AppConsts.ASK_FOR_MOBILE);
                input = GetInput();
            }
            return input;
        }

        public static string GetPassword()
        {
            string input = GetInput();
            while (!InputValidation.PasswordIsValid(input))
            {
                DisplayTryAgainError(AppConsts.INVALID_PASSWORD);
                Console.WriteLine(AppConsts.ASK_FOR_PASSWORD);
                Console.WriteLine(AppConsts.PASSWORD_REQUIREMENTS);
                input = GetInput();
            }
            return input;
        }

        public static void VerifyPassword(User user)
        {
            const string INCORRECT_PASSWORD = "Incorrect Password";
            string input = GetInput();
            while (!InputValidation.PasswordIsValid(input) || !InputValidation.PasswordIsCorrect(user, input))
            {
                if (!InputValidation.PasswordIsValid(input))
                {
                    DisplayTryAgainError(AppConsts.INVALID_PASSWORD);
                }
                else
                {
                    DisplayError(INCORRECT_PASSWORD);
                }
                Console.WriteLine(AppConsts.ASK_FOR_PASSWORD);
                input = GetInput();
            }
        }

        public static string VerifyOldPassword(User user)
        {
            const string PASSWORD_NOT_MATCHING = "Entered password does not match existing password";
            const string ASK_FOR_CURRENT_PASSWORD = "Please enter your current password.";
            string input = GetInput();
            while (!InputValidation.PasswordIsCorrect(user, input))
            {
                DisplayTryAgainError(PASSWORD_NOT_MATCHING);
                Console.WriteLine(ASK_FOR_CURRENT_PASSWORD);
                input = GetInput();
            }
            return input;
        }

        public static string GetFrequentFlyerNumber()
        {
            const string INVALID_FREQUENT_FLYER_NUMBER = "Supplied frequent flyer number is invalid";
            string input = GetInput();
            while (!InputValidation.FrequentFlyerNumberIsValid(input))
            {
                DisplayTryAgainError(INVALID_FREQUENT_FLYER_NUMBER);
                Console.WriteLine(AppConsts.ASK_FOR_FREQUENT_FLYER_NUMBER);
                input = GetInput();
            }
            return input;
        }

        public static int GetFrequentFlyerPoints()
        {
            const string INVALID_FREQUENT_FLYER_POINTS = "Supplied current frequent flyer points is invalid";
            string input = GetInput();
            while (!InputValidation.FrequentFlyerPointsIsValid(input))
            {
                DisplayTryAgainError(INVALID_FREQUENT_FLYER_POINTS);
                Console.WriteLine(AppConsts.ASK_FOR_FREQUENT_FLYER_POINTS);
                input = GetInput();
            }
            int FrequentFlyerPoints = Convert.ToInt32(input);
            return FrequentFlyerPoints;
        }

        public static string GetStaffID()
        {
            const string INVALID_STAFF_ID = "Supplied staff id is invalid";
            string input = GetInput();
            while (!InputValidation.StaffIDIsValid(input))
            {
                DisplayTryAgainError(INVALID_STAFF_ID);
                Console.WriteLine(AppConsts.ASK_FOR_STAFF_ID);
                input = GetInput();
            }
            return input;
        }

        public static string GetFlightID()
        {
            const string INVALID_FLIGHT_ID = "Supplied flight id is invalid";
            string input = GetInput();
            while (!InputValidation.FlightIDIsValid(input))
            {
                DisplayTryAgainError(INVALID_FLIGHT_ID);
                Console.WriteLine(AppConsts.ASK_FOR_FLIGHT_ID);
                input = GetInput();
            }
            return input;
        }

        public static string GetPlaneID()
        {
            const string INVALID_PLANE_ID = "Supplied plane id is invalid";
            string input = GetInput();
            while (!InputValidation.PlaneIDIsValid(input))
            {
                DisplayTryAgainError(INVALID_PLANE_ID);
                Console.WriteLine(AppConsts.ASK_FOR_PLANE_ID);
                input = GetInput();
            }
            return input;
        }

        public static DateTime GetDateTime(string ask_for_date_time)
        {
            const string INVALID_DATE_TIME = "Supplied date and time format is not valid";
            string input = GetInput();
            while (!InputValidation.DateTimeFormatIsValid(input))
            {
                DisplayTryAgainError(INVALID_DATE_TIME);
                Console.WriteLine(ask_for_date_time);
                input = GetInput();
            }
            DateTime date_time = DateTime.ParseExact(input, AppConsts.DATETIME_FORMAT, CultureInfo.InvariantCulture);
            return date_time;
        }

        public static int GetSeatRow()
        {
            const string INVALID_SEAT_ROW = "Supplied seat row is invalid";
            const string ASK_FOR_SEAT_ROW = "Please enter in your seat row between 1 and 10:";
            Console.WriteLine(ASK_FOR_SEAT_ROW);
            string input = GetInput();
            while (!InputValidation.SeatRowIsValid(input))
            {
                DisplayTryAgainError(INVALID_SEAT_ROW);
                Console.WriteLine(ASK_FOR_SEAT_ROW);
                input = GetInput();
            }
            int row = Convert.ToInt32(input);
            return row;
        }

        public static char GetSeatColumn()
        {
            const string INVALID_SEAT_COLUMN = "Supplied seat column is invalid";
            const string ASK_FOR_SEAT_COLUMN = "Please enter in your seat column between A and D:";
            Console.WriteLine(ASK_FOR_SEAT_COLUMN);
            string input = GetInput();
            while (!InputValidation.SeatColumnIsValid(input))
            {
                DisplayTryAgainError(INVALID_SEAT_COLUMN);
                Console.WriteLine(ASK_FOR_SEAT_COLUMN);
                input = GetInput();
            }
            char column = Char.ToUpper(input[0]);
            return column;
        }
    }
}