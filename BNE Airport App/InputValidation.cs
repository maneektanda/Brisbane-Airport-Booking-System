using System;
using System.Text.RegularExpressions;
using System.Globalization;

namespace BNE_Airport_App
{
    /// <summary>
    /// Methods in this class validate user inputs and return a boolean value depending on the validity.
    /// 
    /// Responsibility: Check user inputs.
    /// </summary>
    public class InputValidation
    {
        /// <summary>
        /// Checks if an input is an integer.
        /// </summary>
        /// <param name="integer">The input to be validated.</param>
        /// <returns>True if the input is an integer, false otherwise.</returns>
        public static bool IntIsValid(string integer)
        {
            if (int.TryParse(integer, out _))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if an input is a valid name.
        /// </summary>
        /// <param name="name">The input to be validated.</param>
        /// <returns>True if the input is a valid name, flase otherwise.</returns>
        public static bool NameIsValid(string name)
        {
            // Defining constants that represent the REGEX patterns to match.

            // This pattern ensures the input contains at least one letter.
            const string LETTER_PATTERN = "[a-zA-Z]";

            // This pattern ensures the input contains only letters, spaces, apostrophe's and dashes.
            const string NAME_PATTERN = "^[a-zA-Z '-]+$";

            // Input must match both patterns to be valid.
            if (Regex.IsMatch(name, LETTER_PATTERN) && Regex.IsMatch(name, NAME_PATTERN))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if an input is a valid age.
        /// </summary>
        /// <param name="age">The input to be validated.</param>
        /// <returns>True if the age is valid, false otherwise.</returns>
        public static bool AgeIsValid(string age)
        {
            // Defining some constants for readability.
            const int MIN_AGE = 0;
            const int MAX_AGE = 99;

            // Converts the input to an Int32 type and checks to see if it falls within the valid age range.
            int Age = Convert.ToInt32(age);
            if (Age >= MIN_AGE && Age <= MAX_AGE)
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        /// <summary>
        /// Checks if an input is a valid mobile number.
        /// </summary>
        /// <param name="mobile">The input to be validated.</param>
        /// <returns>True if the mobile is valid, false otherwise.</returns>
        public static bool MobileIsValid(string mobile)
        {
            // Defining some constants for readability.
            const string MOBILE_STARTING_VALUE = "0";
            const int MOBILE_LENGTH = 10;

            // If the input is an integer, of the correct length, and starts with 0, the input is valid.
            if (IntIsValid(mobile) && mobile.Length == MOBILE_LENGTH && mobile.StartsWith(MOBILE_STARTING_VALUE))
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        /// <summary>
        /// Checks if an input is a valid email.
        /// </summary>
        /// <param name="email">The input to be validated.</param>
        /// <returns>True if the email is valid, false otherwise.</returns>
        public static bool EmailIsValid(string email)
        {
            // This pattern ensures the input contains an "@" symbol that isn't at the start or end of the input.
            const string EMAIL_PATTERN = "^[a-zA-Z0-9._]+@{1}[a-zA-Z0-9._]+$";

            // Input must match the REGEX pattern to be valid.
            if (Regex.IsMatch(email, EMAIL_PATTERN))
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        /// <summary>
        /// Checks if an input is a valid password.
        /// The password must be at least 8 characters long, and have at least one uppercase
        /// letter, lowercase letter, and a number.
        /// </summary>
        /// <param name="password">The input to be validated.</param>
        /// <returns>True if the password is valid, false otherwise.</returns>
        public static bool PasswordIsValid(string password)
        {
            // Defining some constants for readability.
            const int MIN_LENGTH = 8;
            const int MIN_LOWERCASE = 1;
            const int MIN_UPPERCASE = 1;
            const int MIN_NUMS = 1;
            List<char> INTS = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            int LowerCase_Counter = 0;
            int UpperCase_Counter = 0;
            int Nums_Counter = 0;

            // Check the length first.
            if (password.Length >= MIN_LENGTH)
            {
                foreach (char character in password)
                {
                    // Count the lowercase letters.
                    if (Char.IsLower(character))
                    {
                        LowerCase_Counter++;
                    }
                    // Count the uppercase letters.
                    if (Char.IsUpper(character))
                    {
                        UpperCase_Counter++;
                    }
                    // Count the numbers.
                    if (INTS.Contains(character))
                    {
                        Nums_Counter++;
                    }
                }
                // If there are enough upper and lowercase letters and numbers, the password is valid.
                if (LowerCase_Counter >= MIN_LOWERCASE && UpperCase_Counter >= MIN_UPPERCASE && Nums_Counter >= MIN_NUMS)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if user has entered their correct password.
        /// </summary>
        /// <param name="user">The user who's password we are checking.</param>
        /// <param name="password">The users input to check.</param>
        /// <returns>True if the password is correct, false otherwise.</returns>
        public static bool PasswordIsCorrect(User user, string password)
        {
            if (user.Password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the input is a valid frequent flyer number.
        /// </summary>
        /// <param name="FFNumber">The input to check.</param>
        /// <returns>True if the number is valid, false otherwise.</returns>
        public static bool FrequentFlyerNumberIsValid(string FFNumber)
        {
            // Defining some constants for readability.
            const int MIN_FF_NUM = 100000;
            const int MAX_FF_NUM = 999999;

            // If the input is a number, and falls within the specified upper and lower bounds, it is valid.
            if (int.TryParse(FFNumber, out int FFNum) && FFNum >= MIN_FF_NUM && FFNum <= MAX_FF_NUM)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the input is a valid frequent flyer points amount.
        /// </summary>
        /// <param name="FFPoints">The input to check.</param>
        /// <returns>True if the number is valid, false otherwise.</returns>
        public static bool FrequentFlyerPointsIsValid(string FFPoints)
        {
            // Defining some constants for readability.
            const int MIN_FF_POINTS = 0;
            const int MAX_FF_POINTS = 1000000;

            // If the input is a number, and falls within the specified upper and lower bounds, it is valid.
            if (int.TryParse(FFPoints, out int numPoints) && numPoints >= MIN_FF_POINTS && numPoints <= MAX_FF_POINTS)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the input is a valid staff id.
        /// </summary>
        /// <param name="StaffID">The input to check.</param>
        /// <returns>True if the staff id is valid, false otherwise.</returns>
        public static bool StaffIDIsValid(string StaffID)
        {
            // Defining some constants for readability.
            const int MIN_STAFF_ID = 1000;
            const int MAX_STAFF_ID = 9000;

            // If the input is a number, and falls within the specified upper and lower bounds, it is valid.
            if (int.TryParse(StaffID, out int staffID) && staffID >= MIN_STAFF_ID && staffID <= MAX_STAFF_ID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the input is a valid flight id.
        /// </summary>
        /// <param name="flightID">The input to check.</param>
        /// <returns>True if the flight id is valid, false otherwise.<</returns>
        public static bool FlightIDIsValid(string flightID)
        {
            // Defining some constants for readability.
            const int MIN_FLIGHT_NUM = 100;
            const int MAX_FLIGHT_NUM = 900;

            // If the input is a number, and falls within the specified upper and lower bounds, it is valid.
            if (int.TryParse(flightID, out int flightNum) && flightNum >= MIN_FLIGHT_NUM && flightNum <= MAX_FLIGHT_NUM)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the input is a valid plane id.
        /// </summary>
        /// <param name="planeID">The input to check.</param>
        /// <returns>True if the plane id is valid, false otherwise.</returns>
        public static bool PlaneIDIsValid(string planeID)
        {
            // Defining some constants for readability.
            const int MIN_PLANE_NUM = 0;
            const int MAX_PLANE_NUM = 9;

            // If the input is a number, and falls within the specified upper and lower bounds, it is valid.
            if (int.TryParse(planeID, out int planeNum) && planeNum >= MIN_PLANE_NUM && planeNum <= MAX_PLANE_NUM)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the input is a valid date and time.
        /// </summary>
        /// <param name="date_time">The input to check.</param>
        /// <returns>True if the date and time is valid, false otherwise.</returns>
        public static bool DateTimeFormatIsValid(string date_time)
        {
            // Uses the specified format in the AppConsts class to verify the format of the input, returns true if they match.
            if (DateTime.TryParseExact(date_time, AppConsts.DATETIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the input is a valid seat row.
        /// </summary>
        /// <param name="seat_row">The input to check.</param>
        /// <returns>True if the seat row is valid, false otherwise.</returns>
        public static bool SeatRowIsValid(string seat_row)
        {
            // Defining some constants for readability.
            const int MIN_SEAT_ROW = 1;
            const int MAX_SEAT_ROW = 10;

            // If the input is a number, and falls within the specified upper and lower bounds, it is valid.
            if (int.TryParse(seat_row, out int num_seat_row) && num_seat_row >= MIN_SEAT_ROW && num_seat_row <= MAX_SEAT_ROW)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the input is a valid seat column.
        /// </summary>
        /// <param name="seat_column">The input to check.</param>
        /// <returns>True if the seat column is valid, false otherwise.</returns>
        public static bool SeatColumnIsValid(string seat_column)
        {
            // Defining some constants for readability.
            // This string pattern ensure the input only contains the letters from "A" to "D".
            const string SEAT_COLUMN_PATTERN = "[a-dA-D]";
            const int MAX_INPUT_LENGTH = 1;

            // If the input is of the right pattern and length, it is valid.
            if (Regex.IsMatch(seat_column, SEAT_COLUMN_PATTERN) && seat_column.Length == MAX_INPUT_LENGTH)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}