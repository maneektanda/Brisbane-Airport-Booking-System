<h1 align="center">Brisbane Airport Booking System </h1>

A console-based airport booking system designed to simulate real-world reservation workflows for services at Brisbane Airport.
This project focuses on the programming and design principles listed below to systematically approach large and complex coding applications,
where extensibility and maintainability are key.

  - Object-Oriented Programming
  - SOLID Design Principles
  - Model View Controller Framework 


<h3><li>Class Overview</li></h3>

The Brisbane Airport Booking System has 2 main user types, Flight Manager and Traveller, which both inherit from the User abstract base class.

Flight Manager functionality-
  - Create Account
  - See Personal Details
  - Change Password
  - Create Arrival Flight
  - Create Departure Flight
  - Delay Arrival Flight
  - Delay Departure Flight
  - See All Flight Details
  - Login/Logout

Traveller functionality-
  - Create Account
  - See Personal Details
  - Change Password
  - Book Arrival Flight (select available seats only)
  - Book Departure Flight (select available seats only)
  - See Flight Details
  - Login/Logout

Frequent Flyers inherit from the Traveller class and have some additional functionality-
  - Show Frequent Flyer Points
  - Book Arrival Flight (select any seat)
  - Book Departure Flight (select any seat)
  - Accrue Frequent Flyer Points


<h3><li>Error Handling</li></h3>

  - Input validation (e.g emails contain the "@" symbol that isn't at the start or end)
  - Prevent conflicting reservations (e.g arrival flights must be before departure flights)
  - Graceful handling of invalid operations (e.g incorrect menu navigation input)


<h3><li>Key Learnings</li></h3>

  - C#
  - Object-Oriented Programming (Encapsulation, Abstraction, Inheritance, Polymorphism)
  - Structuring projects using MVC architecture
  - Method and operator overloading
  - Virtual and override polymorphic modifiers
  - Custom comparers
  - Access modifiers
  - Abstract base classes


<h3><li>Future Improvements</li></h3>

  - Add a graphical user interface
  - Integrate a real database (MySQL)
  - Add payment simulation


<h3><li>Getting Started</li></h3>

1. Clone the repository-<br>
```sh
git clone https://github.com/maneektanda/Brisbane-Airport-Booking-System.git
```
2. Run the project-<br>
```sh
dotnet run
```
