using System;
using System.Collections.ObjectModel;

namespace BNE_Airport_App
{
    /// <summary>
    /// The main controller for the entire app. handles login and user creation.
    /// 
    /// Any changing of data has been kept in this controller and not in any of the view classes such as the
    /// BNEAirportMenus or CmdLineUI class.
    /// 
    /// Responsibility: Handle the main menu.
    /// </summary>
    public class AppController
    {
        /// <summary>
        /// A database to store data.
        /// </summary>
        private Database database;

        /// <summary>
        /// The menu class to help interact with the users.
        /// </summary>
        private BNEAirportMenus menu;

        /// <summary>
        /// A constructor for the controller.
        /// </summary>
        public AppController()
        {
            database = new Database();
            menu = new BNEAirportMenus();
        }

        /// <summary>
        /// A method that keeps the main menu running unless the user wants to exit.
        /// </summary>
        public void Run()
        {
            menu.DisplayMessage(AppConsts.HEADER);

            bool keep_going = true;

            while (keep_going)
            {
                keep_going = ProcessMainMenu();
            }
        }

        /// <summary>
        /// Shows the main menu and handles the logic of what a user wants to do.
        /// </summary>
        /// <returns>Executes the appropriate methods to create users, login or exit.</returns>
        private bool ProcessMainMenu()
        {
            // Define some constants for readability.
            const string LOGIN_STR = "Login as a registered user";
            const string REGISTER_STR = "Register as a new user";
            const string EXIT_STR = "Exit";
            const string EXIT_MESSAGE = "Thank you. Safe travels.";

            // A list of options.
            List<string> options = new List<string>();
            options.Add(LOGIN_STR);
            options.Add(REGISTER_STR);
            options.Add(EXIT_STR);

            // Switch case values.
            const int LOGIN_INT = 0;
            const int REGISTER_INT = 1;
            const int EXIT_INT = 2;

            // Get the option from the user.
            int option = menu.DisplayMainMenu(options);

            switch (option)
            {
                case LOGIN_INT:
                    // Login a user.
                    ProcessLoginUser();
                    break;
                case REGISTER_INT:
                    // Register a user.
                    ProcessRegisterUser();
                    break;
                case EXIT_INT:
                    // Exit the app.
                    menu.DisplayMessage(EXIT_MESSAGE);
                    return false;
                    break;
                default:
                    // Display error message.
                    menu.DisplayError(AppConsts.INVALID_CHOICE_ERROR);
                    break;
            }

            return true;
        }

        /// <summary>
        /// A method to register a new user.
        /// </summary>
        private void ProcessRegisterUser()
        {
            // Define some constants for readability.
            const string TRAVELLER_STR = "A standard traveller";
            const string FREQUENT_FLYER_STR = "A frequent flyer";
            const string FLIGHT_MANAGER_STR = "A flight manager";

            // A list of options.
            List<string> options = new List<string>();
            options.Add(TRAVELLER_STR);
            options.Add(FREQUENT_FLYER_STR);
            options.Add(FLIGHT_MANAGER_STR);

            // Switch case values.
            const int TRAVELLER_INT = 0;
            const int FREQUENT_FLYER_INT = 1;
            const int FLIGHT_MANAGER_INT = 2;

            const string USER_MENU_MSG = "Which user type would you like to register?";

            // Get the option from the user.
            int option = menu.DisplayNumberedMenu(USER_MENU_MSG, options);

            switch (option)
            {
                case TRAVELLER_INT:
                    // Create a traveller.
                    ProcessCreateTraveller();
                    break;
                case FREQUENT_FLYER_INT:
                    // Create a frquent flyer.
                    ProcessCreateFrequentFlyer();
                    break;
                case FLIGHT_MANAGER_INT:
                    // Create a flight manager.
                    ProcessCreateFlightManager();
                    break;
                default:
                    // Show error message.
                    menu.DisplayError(AppConsts.INVALID_CHOICE_ERROR);
                    break;
            }
        }

        /// <summary>
        /// Creates a traveller and adds them to the database and congratulate them.
        /// 
        /// Duplicate email check is kept in the controller to avoid passing the database outside of the controller.
        /// </summary>
        private void ProcessCreateTraveller()
        {
            // Out variables for creating a traveller.
            string name, mobile, email, password;
            int age;
            const string CREATE_TRAVELLER_MENU_TITLE = "Registering as a traveller.";

            // Ask for input for name age and mobile.
            menu.CreateUserMenu(CREATE_TRAVELLER_MENU_TITLE, out name, out age, out mobile);

            // Gets the users email.
            GetEmail(out email);

            // Gets the password.
            menu.CreatePasswordMenu(out password);

            // Creates a traveller, adds them to the database, and congratulates them.
            Traveller traveller = new Traveller(name, age, mobile, email, password);
            database.AddUser(traveller);
            menu.DisplayMessage(traveller.CongratulateUser());
        }

        /// <summary>
        /// Creates a frequent flyer and adds them to the database and congratulate them.
        /// 
        /// Duplicate email check is kept in the controller to avoid passing the database outside of the controller.
        /// </summary>
        private void ProcessCreateFrequentFlyer()
        {
            // Out variables for creating a traveller.
            string name, mobile, email, password, frequent_flyer_number;
            int age, frequent_flyer_points;
            const string CREATE_FREQUENT_FLYER_MENU_TITLE = "Registering as a frequent flyer.";

            // Ask for input for name age and mobile.
            menu.CreateUserMenu(CREATE_FREQUENT_FLYER_MENU_TITLE, out name, out age, out mobile);

            // Gets the users email.
            GetEmail(out email);

            // Gets the password.
            menu.CreatePasswordMenu(out password);

            // Gets frequent flyer details.
            menu.CreateFrequentFlyerMenu(out frequent_flyer_number, out frequent_flyer_points);

            // Creates a frequent flyer, adds them to the database and congratulates them.
            FrequentFlyer frequent_flyer = new FrequentFlyer(name, age, mobile, email, password, frequent_flyer_number, frequent_flyer_points);
            database.AddUser(frequent_flyer);
            menu.DisplayMessage(frequent_flyer.CongratulateUser());
        }

        /// <summary>
        /// Creates a flight manager and adds them to the database and congratulate them.
        /// 
        /// Duplicate email check is kept in the controller to avoid passing the database outside of the controller.
        /// </summary>
        private void ProcessCreateFlightManager()
        {
            // Out variables for creating a traveller.
            string name, mobile, email, password, staff_id;
            int age;
            const string CREATE_FLIGHT_MANAGER_MENU_TITLE = "Registering as a flight manager.";

            // Ask for input for name age and mobile.
            menu.CreateUserMenu(CREATE_FLIGHT_MANAGER_MENU_TITLE, out name, out age, out mobile);

            // Gets the users email.
            GetEmail(out email);

            // Gets the password.
            menu.CreatePasswordMenu(out password);

            // Gets the staff id.
            menu.CreateFlightManagerMenu(out staff_id);

            // Creates a flight manager, adds them to the database and congratulates them.
            FlightManager flight_manager = new FlightManager(name, age, mobile, email, password, staff_id);
            database.AddUser(flight_manager);
            menu.DisplayMessage(flight_manager.CongratulateUser());
        }

        /// <summary>
        /// Logs in a user and runs the appropriate controller.
        /// </summary>
        private void ProcessLoginUser()
        {
            // Gets a logged in user after verifying their details.
            dynamic logged_in_user = DisplayUserLoginMenu();

            // Checks the user returned from the DisplayUserLoginMenu() method is not null.
            if (logged_in_user != null)
            {
                switch (logged_in_user)
                {
                    // If they're a frequent flyer, create the controller and call the Run() method.
                    case FrequentFlyer:
                        FrequentFlyerController frequent_flyer_controller = new FrequentFlyerController(logged_in_user, database);
                        frequent_flyer_controller.Run();
                        break;
                    
                    // If they're a traveller, create the controller and call the Run() method.
                    case Traveller:
                        TravellerController traveller_controller = new TravellerController(logged_in_user, database);
                        traveller_controller.Run();
                        break;
                    
                    // If they're a flight manager, create the controller and call the Run() method.
                    case FlightManager:
                        FlightManagerController flight_manager_controller = new FlightManagerController(logged_in_user, database);
                        flight_manager_controller.Run();
                        break;
                    
                    // Shows ann error if non of these options are matched.
                    default:
                        menu.DisplayError(AppConsts.INVALID_CHOICE_ERROR);
                        break;
                }
            }
        }

        /// <summary>
        /// Gets a users email and checks that it exists and is valid. Because it requires database checks it is kept in the controller.
        /// </summary>
        /// <param name="email">The retrieved email.</param>
        private void GetEmail(out string email)
        {
            // Define a constant for readability.
            const string DUPLICATE_EMAIL = "Email already registered";

            // Asks for the email and gets input from the user.
            menu.DisplayMessage(AppConsts.ASK_FOR_EMAIL);
            string input = menu.GetInput();

            // While the email is invalid or already exists in the database, display the relevant error and ask the user again.
            while (!InputValidation.EmailIsValid(input) || EmailExists(input))
            {
                // If email is invalid, show the invalid error message.
                if (!InputValidation.EmailIsValid(input))
                {
                    menu.DisplayTryAgainError(AppConsts.INVALID_EMAIL);
                }

                // If email already exists, show the duplicate error message.
                else
                {
                    menu.DisplayTryAgainError(DUPLICATE_EMAIL);
                }

                // Ask for the email again.
                menu.DisplayMessage(AppConsts.ASK_FOR_EMAIL);
                input = menu.GetInput();
            }

            // Out a valid email.
            email = input;
        }

        /// <summary>
        /// Checks if the email already exists in the database.
        /// </summary>
        /// <param name="email">The email to check.</param>
        /// <returns>True if the email exists, false otherwise.</returns>
        private bool EmailExists(string email)
        {
            // A read only list of users from the database.
            List<User> users = new List<User>(database.ReadOnlyUsersList);

            // Checks all users in the list to see if their email matches. 
            // Once a match is found, update the email_exists variable.
            bool email_exists = false;
            foreach (User user in users)
            {
                if (user.Email == email)
                {
                    email_exists = true;
                }
            }
            return email_exists;
        }

        /// <summary>
        /// Displays the user login menu
        /// </summary>
        /// <returns>The user that has logged in.</returns>
        private User DisplayUserLoginMenu()
        {
            // A read only list of users from the database.
            ReadOnlyCollection<User> users = database.ReadOnlyUsersList;

            // Define consts for readability and display menu title.
            const string LOGIN_MENU_TITLE = "Login Menu.";
            const string NO_REGISTRATIONS_ERROR = "There are no people registered";
            menu.DisplayMessage(LOGIN_MENU_TITLE);

            // If no users, show error.
            if (users.Count == 0)
            {
                menu.DisplayError(NO_REGISTRATIONS_ERROR);
                return null;
            }
            // Otherwise, get the email and check the password matches.
            else
            {
                // Ask for and get the email.
                menu.DisplayMessage(AppConsts.ASK_FOR_EMAIL);
                string email = GetRegisteredEmail();

                // Get the associated user of that email.
                User registered_user = GetUserWithEmail(email);

                // Ask for and get the associated password.
                menu.DisplayMessage(AppConsts.ASK_FOR_PASSWORD);
                CmdLineUI.VerifyPassword(registered_user);


                // Welcome back the user and return the instance of that user.
                menu.DisplayMessage($"Welcome back {registered_user.Name}.");
                return registered_user;
            }
        }

        /// <summary>
        /// Get the email from a user.
        /// </summary>
        /// <returns>The email of a registered user.</returns>
        private string GetRegisteredEmail()
        {
            // Define a constant for readability.
            const string EMAIL_NOT_REGISTERED = "Email is not registered";
            
            // Get an email from the user, while that email is invalid or not saved in the database, keep asking the user for their email.
            string input = menu.GetInput();
            while (!InputValidation.EmailIsValid(input) || !EmailExists(input))
            {
                // If the email isn't valid, show this error.
                if (!InputValidation.EmailIsValid(input))
                {
                    menu.DisplayTryAgainError(AppConsts.INVALID_EMAIL);
                }

                // If the email doesn't exist in the database, show this message.
                else
                {
                    menu.DisplayError(EMAIL_NOT_REGISTERED);
                }

                // Ask again for the email.
                menu.DisplayMessage(AppConsts.ASK_FOR_EMAIL);
                input = menu.GetInput();
            }
            // Returns the email.
            return input;
        }

        /// <summary>
        /// Gets the instance of a user from their email.
        /// </summary>
        /// <param name="email">The inputted email.</param>
        /// <returns>The isntance of a user.</returns>
        private User? GetUserWithEmail(string email)
        {
            // A read only list of users from the database.
            ReadOnlyCollection<User> users = database.ReadOnlyUsersList;

            // Check all the users to see if their email matches the input email.
            // If it does, return that user, otherwise return null.
            foreach (User user in users)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }
            return null;
        }
    }
}