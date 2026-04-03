using System;

namespace BNE_Airport_App
{
    /// <summary>
    /// An abstract base class to represent any user that can use the app. It contains any common fields and methods to all users.
    /// 
    /// Responsibility: To represent the most generic user type for others to inherrit from.
    /// </summary>
    public abstract class User
    {
        /// <summary>
        /// The users name.
        /// </summary>
        protected string name;

        /// <summary>
        /// The users age.
        /// </summary>
        protected int age;

        /// <summary>
        /// The users mobile.
        /// </summary>
        protected string mobile;

        /// <summary>
        /// The users email.
        /// </summary>
        protected string email;

        /// <summary>
        /// The users password.
        /// </summary>
        protected string password;

        /// <summary>
        /// A getter to return the users name.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// A getter to return the users email.
        /// </summary>
        public string Email
        {
            get { return email; }
        }

        /// <summary>
        /// A getter to return the users password.
        /// </summary>
        public string Password
        {
            get { return password; }
        }

        /// <summary>
        /// A constructor for a user.
        /// </summary>
        /// <param name="name">The users name.</param>
        /// <param name="age">The users age.</param>
        /// <param name="mobile">The users mobile.</param>
        /// <param name="email">The users email.</param>
        /// <param name="password">The users password.</param>
        public User(string name, int age, string mobile, string email, string password)
        {
            this.name = name;
            this.age = age;
            this.mobile = mobile;
            this.email = email;
            this.password = password;
        }

        /// <summary>
        /// Create a string version of the user that is human readable.
        /// </summary>
        /// <returns>A string representation of the user.</returns>
        public override string ToString()
        {
            return $"""
            Your details.
            Name: {name}
            Age: {age}
            Mobile phone number: {mobile}
            Email: {email}
            """;
        }

        /// <summary>
        /// Create a string to congratulate the user.
        /// </summary>
        /// <returns>A string congratulating the user.</returns>
        public virtual string CongratulateUser()
        {
            return $"Congratulations {name}. You have registered as a ";
        }

        /// <summary>
        /// Changes the users password.
        /// </summary>
        /// <param name="new_password">The users new password.</param>
        public void ChangePassword(string new_password)
        {
            password = new_password;
        }
    }
}