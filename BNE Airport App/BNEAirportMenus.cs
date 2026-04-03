using System;

namespace BNE_Airport_App
{
    /// <summary>
    /// A class for creating all of the user menus in the program.
    /// </summary>
    public class BNEAirportMenus
    {
        public string GetInput()
        {
            return CmdLineUI.GetInput();
        }

        public void DisplayMessage(string message)
        {
            CmdLineUI.DisplayString(message);
        }

        public void DisplayMessage(User user)
        {
            CmdLineUI.DisplayString(user);
        }

        public void DisplayMessage(Flight flight)
        {
            CmdLineUI.DisplayString(flight);
        }

        public void DisplayError(string message)
        {
            CmdLineUI.DisplayError(message);
        }

        public void DisplayTryAgainError(string message)
        {
            CmdLineUI.DisplayTryAgainError(message);
        }

        public int DisplayMainMenu(List<string> options)
        {
            const string MAIN_MENU_MSG = "Please make a choice from the menu below:";

            CmdLineUI.DisplayString();

            int option = GetOption(MAIN_MENU_MSG, options);

            return option;
        }

        public int DisplayNumberedMenu(string menu_message, List<string> options)
        {
            int option = GetOption(menu_message, options);

            return option;
        }

        public int DisplayNumberedMenuWithoutPeriod(string menu_message, List<string> options)
        {
            int option = GetOptionWithoutPeriod(menu_message, options);

            return option;
        }

        public int DisplayUserMenu(string menu_title, string menu_message, List<string> options)
        {
            CmdLineUI.DisplayString();
            CmdLineUI.DisplayString(menu_title);

            int option = GetOption(menu_message, options);

            return option;
        }

        public static int GetOption(string title, List<string> options)
        {
            if (options.Count <= 0)
            {
                return -1;
            }

            CmdLineUI.DisplayString(title);

            for (int i = 0; i < options.Count; i++)
            {
                CmdLineUI.DisplayString($"{i + 1}. {options[i]}.");
            }

            int option = CmdLineUI.GetMenuChoice($"Please enter a choice between 1 and {options.Count}:");

            return option - 1;
        }

        public static int GetOptionWithoutPeriod(string title, List<string> options)
        {
            if (options.Count <= 0)
            {
                return -1;
            }

            CmdLineUI.DisplayString(title);

            for (int i = 0; i < options.Count; i++)
            {
                CmdLineUI.DisplayString($"{i + 1}. {options[i]}");
            }

            int option = CmdLineUI.GetMenuChoice($"Please enter a choice between 1 and {options.Count}:");

            return option - 1;
        }

        public void CreateUserMenu(string menu_title, out string name, out int age, out string mobile)
        {
            CmdLineUI.DisplayString(menu_title);
            CmdLineUI.DisplayString(AppConsts.ASK_FOR_NAME);
            name = CmdLineUI.GetName();
            CmdLineUI.DisplayString(AppConsts.ASK_FOR_AGE);
            age = CmdLineUI.GetAge();
            CmdLineUI.DisplayString(AppConsts.ASK_FOR_MOBILE);
            mobile = CmdLineUI.GetMobile();
        }

        public void CreatePasswordMenu(out string password)
        {
            CmdLineUI.DisplayString(AppConsts.ASK_FOR_PASSWORD);
            CmdLineUI.DisplayString(AppConsts.PASSWORD_REQUIREMENTS);
            password = CmdLineUI.GetPassword();
        }
        public void CreateFrequentFlyerMenu(out string frequent_flyer_number, out int frequent_flyer_points)
        {
            CmdLineUI.DisplayString(AppConsts.ASK_FOR_FREQUENT_FLYER_NUMBER);
            frequent_flyer_number = CmdLineUI.GetFrequentFlyerNumber();
            CmdLineUI.DisplayString(AppConsts.ASK_FOR_FREQUENT_FLYER_POINTS);
            frequent_flyer_points = CmdLineUI.GetFrequentFlyerPoints();
        }

        public void CreateFlightManagerMenu(out string staff_id)
        {
            CmdLineUI.DisplayString(AppConsts.ASK_FOR_STAFF_ID);
            staff_id = CmdLineUI.GetStaffID();
        }

        public string DisplayChangePasswordMenu(User user)
        {
            const string ASK_FOR_CURRENT_PASSWORD = "Please enter your current password.";
            const string ASK_FOR_NEW_PASSWORD = "Please enter your new password.";

            CmdLineUI.DisplayString(ASK_FOR_CURRENT_PASSWORD);
            CmdLineUI.VerifyOldPassword(user);
            CmdLineUI.DisplayString(ASK_FOR_NEW_PASSWORD);
            return CmdLineUI.GetPassword();
        }


        public void GetAirlineNameAndCodeMenu(out string airline_name, out string airline_code)
        {
            List<string> airline_name_options = new List<string>();
            airline_name_options.Add(AppConsts.JETSTAR);
            airline_name_options.Add(AppConsts.QANTAS);
            airline_name_options.Add(AppConsts.REGIONAL_EXPRESS);
            airline_name_options.Add(AppConsts.VIRGIN);
            airline_name_options.Add(AppConsts.FLY_PELICAN);

            const int FIRST_LIST_OPTION = 0;

            const string AIRLINES_MENU_MSG = "Please enter the airline:";

            int airline_name_choice = DisplayNumberedMenuWithoutPeriod(AIRLINES_MENU_MSG, airline_name_options);
            while (airline_name_choice < FIRST_LIST_OPTION || airline_name_choice > airline_name_options.Count - 1)
            {
                CmdLineUI.DisplayTryAgainError(AppConsts.INVALID_CHOICE_ERROR);
                airline_name_choice = DisplayNumberedMenuWithoutPeriod(AIRLINES_MENU_MSG, airline_name_options);
            }

            airline_name = airline_name_options[airline_name_choice];
            airline_code = Enum.GetName(typeof(AppConsts.AirlineCodes), airline_name_choice);
        }

        public void GetCityMenu(string menu_message, out string city)
        {
            List<string> city_options = new List<string>();
            city_options.Add(AppConsts.SYDNEY);
            city_options.Add(AppConsts.MELBOURNE);
            city_options.Add(AppConsts.ROCKHAMPTON);
            city_options.Add(AppConsts.ADELAIDE);
            city_options.Add(AppConsts.PERTH);

            const int FIRST_LIST_OPTION = 0;

            int city_choice = DisplayNumberedMenuWithoutPeriod(menu_message, city_options);
            while (city_choice < FIRST_LIST_OPTION || city_choice > city_options.Count - 1)
            {
                CmdLineUI.DisplayTryAgainError(AppConsts.INVALID_CHOICE_ERROR);
                city_choice = DisplayNumberedMenuWithoutPeriod(menu_message, city_options);
            }
            city = city_options[city_choice];
        }

        public void CreateFlightMenu(string menu_message, string ask_for_date_time, out string airline_name, out string airline_code, out string city, out string flight_id, out string plane_id, out DateTime date_and_time)
        {
            GetAirlineNameAndCodeMenu(out airline_name, out airline_code);
            GetCityMenu(menu_message, out city);
            CmdLineUI.DisplayString(AppConsts.ASK_FOR_FLIGHT_ID);
            flight_id = CmdLineUI.GetFlightID();
            CmdLineUI.DisplayString(AppConsts.ASK_FOR_PLANE_ID);
            plane_id = CmdLineUI.GetPlaneID();
            CmdLineUI.DisplayString(ask_for_date_time);
            date_and_time = CmdLineUI.GetDateTime(ask_for_date_time);
        }

        public void DisplayFlights(List<ArrivalFlight> arrival_flights)
        {
            const string ARRIVAL_FLIGHTS_TITLE = "Arrival Flights:";
            const string NO_ARRIVAL_FLIGHTS = "There are no arrival flights.";
            const int NO_FLIGHTS = 0;
            DisplayMessage(ARRIVAL_FLIGHTS_TITLE);
            if (arrival_flights.Count != NO_FLIGHTS)
            {
                foreach (ArrivalFlight arrival_flight in arrival_flights)
                {
                    DisplayMessage(arrival_flight);
                }
            }
            else
            {
                DisplayMessage(NO_ARRIVAL_FLIGHTS);
            }
        }

        public void DisplayFlights(List<DepartureFlight> departure_flights)
        {
            const string DEPARTURE_FLIGHTS_TITLE = "Departure Flights:";
            const string NO_DEPARTURE_FLIGHTS = "There are no departure flights.";
            const int NO_FLIGHTS = 0;
            DisplayMessage(DEPARTURE_FLIGHTS_TITLE);
            if (departure_flights.Count != NO_FLIGHTS)
            {
                foreach (DepartureFlight departure_flight in departure_flights)
                {
                    DisplayMessage(departure_flight);
                }
            }
            else
            {
                DisplayMessage(NO_DEPARTURE_FLIGHTS);
            }
        }

        public int GetFlightOption(string title, List<ArrivalFlight> options)
        {
            CmdLineUI.DisplayString(title);

            for (int i = 0; i < options.Count; i++)
            {
                CmdLineUI.DisplayString($"{i + 1}. {options[i].ToString()}");
            }

            int option = CmdLineUI.GetFlightMenuChoice($"Please enter a choice between 1 and {options.Count}:", options.Count);

            return option - 1;
        }

        public int GetFlightOption(string title, List<DepartureFlight> options)
        {
            CmdLineUI.DisplayString(title);

            for (int i = 0; i < options.Count; i++)
            {
                CmdLineUI.DisplayString($"{i + 1}. {options[i].ToString()}");
            }

            int option = CmdLineUI.GetFlightMenuChoice($"Please enter a choice between 1 and {options.Count}:", options.Count);

            return option - 1;
        }

        public void DelayFlightMenu(List<ArrivalFlight> all_arrival_flights, out int arrival_flight, out int minutes_delayed)
        {
            const string DELAY_ARRIVAL_FLIGHT_MENU_MSG = "Please enter the arrival flight:";
            arrival_flight = GetFlightOption(DELAY_ARRIVAL_FLIGHT_MENU_MSG, all_arrival_flights);
            DisplayMessage(AppConsts.ASK_FOR_MINUTES_DELAYED);
            minutes_delayed = CmdLineUI.GetMinutesDelayed();
        }

        public void DelayFlightMenu(List<DepartureFlight> all_departure_flights, out int departure_flight, out int minutes_delayed)
        {
            const string DELAY_ARRIVAL_FLIGHT_MENU_MSG = "Please enter the departure flight:";
            departure_flight = GetFlightOption(DELAY_ARRIVAL_FLIGHT_MENU_MSG, all_departure_flights);
            DisplayMessage(AppConsts.ASK_FOR_MINUTES_DELAYED);
            minutes_delayed = CmdLineUI.GetMinutesDelayed();
        }

        public void GetSeatOptionsMenu(out int row, out char column)
        {
            row = CmdLineUI.GetSeatRow();
            column = CmdLineUI.GetSeatColumn();
        }
    }
}