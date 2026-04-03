using System;

namespace BNE_Airport_App
{
    /// <summary>
    /// A class for defining const vairables to be used throughout the program.
    /// </summary>
    public static class AppConsts
    {
        public const string HEADER = """
        ==========================================
        =  Welcome to Brisbane Domestic Airport  =
        ==========================================
        """;

        public const string INVALID_CHOICE_ERROR = "Supplied choice is invalid";

        public const string ASK_FOR_NAME = "Please enter in your name:";

        public const string ASK_FOR_AGE = "Please enter in your age between 0 and 99:";

        public const string ASK_FOR_MOBILE = "Please enter in your mobile number:";

        public const string ASK_FOR_EMAIL = "Please enter in your email:";
        public const string INVALID_EMAIL = "Supplied email is invalid";

        public const string ASK_FOR_PASSWORD = "Please enter in your password:";

        public const string PASSWORD_REQUIREMENTS = """
        Your password must:
        -be at least 8 characters long 
        -contain a number
        -contain a lowercase letter
        -contain an uppercase letter
        """;

        public const string INVALID_PASSWORD = "Supplied password is invalid";

        public const string ASK_FOR_FREQUENT_FLYER_NUMBER = "Please enter in your frequent flyer number between 100000 and 999999:";

        public const string ASK_FOR_FREQUENT_FLYER_POINTS = "Please enter in your current frequent flyer points between 0 and 1000000:";

        public const string ASK_FOR_STAFF_ID = "Please enter in your staff id between 1000 and 9000:";

        public const string ASK_FOR_FLIGHT_ID = "Please enter in your flight id between 100 and 900:";

        public const string ASK_FOR_PLANE_ID = "Please enter in your plane id between 0 and 9:";

        public const string ASK_FOR_MINUTES_DELAYED = "Please enter in your minutes delayed:";

        public const string DATETIME_FORMAT = "HH:mm dd/MM/yyyy";
        public const string USER_MENU_MSG = "Please make a choice from the menu below:";
        public enum AirlineCodes
        {
            JST,
            QFA,
            RXA,
            VOZ,
            FRE
        }

        public const string JETSTAR = "Jetstar";
        public const string QANTAS = "Qantas";
        public const string REGIONAL_EXPRESS = "Regional Express";
        public const string VIRGIN = "Virgin";
        public const string FLY_PELICAN = "Fly Pelican";

        public const string SYDNEY = "Sydney";
        public const string MELBOURNE = "Melbourne";
        public const string ROCKHAMPTON = "Rockhampton";
        public const string ADELAIDE = "Adelaide";
        public const string PERTH = "Perth";
        public static Dictionary<string, int> FrequentFlyerPoints = new Dictionary<string, int>
        {
            {SYDNEY, 1200},
            {MELBOURNE, 1750},
            {ROCKHAMPTON, 1400},
            {ADELAIDE, 1950},
            {PERTH, 3375}
        };

        public const string SEE_PERSONAL_DETAILS_STR = "See my details";
        public const string CHANGE_PASSWORD_STR = "Change password";
        public const string BOOK_ARRIVAL_FLIGHT_STR = "Book an arrival flight";
        public const string BOOK_DEPARTURE_FLIGHT_STR = "Book a departure flight";
        public const string SEE_FLIGHT_DETAILS_STR = "See flight details";
        public const string LOGOUT_STR = "Logout";

        public const int SEE_PERSONAL_DETAILS_INT = 0;
        public const int CHANGE_PASSWORD_INT = 1;
        public const int BOOK_ARRIVAL_FLIGHT_INT = 2;
        public const int BOOK_DEPARTURE_FLIGHT_INT = 3;
        public const int SEE_FLIGHT_DETAILS_INT = 4;
    }
}