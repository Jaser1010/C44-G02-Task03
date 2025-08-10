using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task02
{
    #region Question 1: Employee Class and Supporting Enum

    // security privilege levels Question 1.
    public enum SecurityPrivileges
    {
        Guest,
        Developer,
        Secretary,
        DBA
    }

    public class Employee
    {
        private int _id;
        private string _name;
        private SecurityPrivileges _securityLevel;
        private decimal _salary;
        private HireDate _hireDate;
        private char _gender;

        #region ID
            public int ID
            {
                get { return _id; }
                private set { _id = value; }
            }
        #endregion

        #region Name
        public string Name
            {
                get { return _name; }
                set { _name = value; }
            }
        #endregion

        #region SecurityLevel
        public SecurityPrivileges SecurityLevel
            {
                get { return _securityLevel; }
                set { _securityLevel = value; }
            }
        #endregion

        #region Salary 
        public decimal Salary
            {
                get { return _salary; }
                set
                {
                    if (value < 0)
                        Console.WriteLine("Error: Salary cannot be negative.");
                    else
                        _salary = value;
                }
            }
        #endregion

        #region HireDate 
        public HireDate HireDate
            {
                get { return _hireDate; }
                set { _hireDate = value; }
            }
        #endregion

        #region Gender
        public char Gender
            {
                get { return _gender; }
                set
                {
                    char upperValue = char.ToUpper(value);
                    if (upperValue == 'M' || upperValue == 'F')
                        _gender = upperValue;
                    else
                        Console.WriteLine("Error: Gender must be 'M' or 'F'.");
                }
            }
        #endregion


        #region initialize a new Employee object.
        // initialize a new Employee object.
        public Employee(int id, string name, SecurityPrivileges securityLevel, decimal salary, HireDate hireDate, char gender)
            {
                this.ID = id;
                this.Name = name;
                this.SecurityLevel = securityLevel;
                this.Salary = salary;
                this.HireDate = hireDate;
                this.Gender = gender;
            }
        #endregion

        #region Overriding the default ToString method 
        // Overriding the default ToString method 
        public override string ToString()
            {
                string formattedString = String.Format(
                    "Employee Details:\n" +
                    "  ID: {0}\n" +
                    "  Name: {1}\n" +
                    "  Gender: {2}\n" +
                    "  Security Level: {3}\n" +
                    "  Salary: {4:C}\n" + // C format specifier for currency
                    "  Hire Date: {5}",
                    ID, Name, Gender, SecurityLevel, Salary, HireDate.ToString()
                );
                return formattedString;
            }
        #endregion

    }

    #endregion

    #region Question 2: HireDate Struct

    public struct HireDate
    {
        public int Day;
        public int Month;
        public int Year;

        // Constructor to initialize the HireDate.
        public HireDate(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }

        public override string ToString()
        {
            return $"{Day:D2}-{Month:D2}-{Year}";
        }
    }

    #endregion
    //========================================================================
    #region Part 1: Duration Class
    public class Duration
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        #region Constructor to initialize Duration with hours, minutes, and seconds.
            /// Constructor to initialize Duration with hours, minutes, and seconds.
            public Duration(int hours, int minutes, int seconds)
            {
                Hours = hours;
                Minutes = minutes;
                Seconds = seconds;
                Normalize();
            }
        #endregion

        #region Constructor to initialize Duration from a total number of seconds.
        /// Constructor to initialize Duration from a total number of seconds.
        public Duration(int totalSeconds)
            {
                if (totalSeconds < 0) totalSeconds = 0;
                Hours = totalSeconds / 3600;
                Minutes = (totalSeconds % 3600) / 60;
                Seconds = totalSeconds % 60;
            }
        #endregion

        #region Normalizes the time so that minutes and seconds are within the 0-59 range.
        /// Normalizes the time so that minutes and seconds are within the 0-59 range.
        private void Normalize()
            {
                int totalSeconds = ToTotalSeconds();
                Hours = totalSeconds / 3600;
                Minutes = (totalSeconds % 3600) / 60;
                Seconds = totalSeconds % 60;
            }
        #endregion

        #region Converts the duration to a total number of seconds.
        /// Converts the duration to a total number of seconds.
        private int ToTotalSeconds()
            {
                return Hours * 3600 + Minutes * 60 + Seconds;
            }

            public override string ToString()
            {
                return $"Hours: {Hours}, Minutes: {Minutes}, Seconds: {Seconds}";
            }
        #endregion

        #region Operator Overloading
        // --- Operator Overloading ---
        public static Duration operator +(Duration d1, Duration d2)
            {
                return new Duration(d1.ToTotalSeconds() + d2.ToTotalSeconds());
            }

            public static Duration operator +(Duration d, int seconds)
            {
                return new Duration(d.ToTotalSeconds() + seconds);
            }

            public static Duration operator +(int seconds, Duration d)
            {
                return new Duration(d.ToTotalSeconds() + seconds);
            }

            public static Duration operator ++(Duration d)
            {
                return new Duration(d.ToTotalSeconds() + 60); // Increase by one minute
            }

            public static bool operator >(Duration d1, Duration d2)
            {
                return d1.ToTotalSeconds() > d2.ToTotalSeconds();
            }

            public static bool operator <(Duration d1, Duration d2)
            {
                return d1.ToTotalSeconds() < d2.ToTotalSeconds();
            }

            public static implicit operator bool(Duration d)
            {
                return d.ToTotalSeconds() > 0;
            }

            public static explicit operator DateTime(Duration d)
            {
                // Creates a DateTime object representing today's date with the time from the Duration object.

                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, d.Hours, d.Minutes, d.Seconds);
            }

        #endregion

    }

    #endregion

    internal class Program
    {
        static void Main(string[] args)
        {
            #region Employee and HireDate Demonstration (Part 2)
                // 1. Create an array of Employees with size three.
                Employee[] empArr = new Employee[3];

                // 2. Instantiate three employees with different roles as specified.
                empArr[0] = new Employee(101, "Ahmed Ali", SecurityPrivileges.DBA, 75000m, new HireDate(15, 5, 2020), 'M');
                empArr[1] = new Employee(102, "Sara Said", SecurityPrivileges.Guest, 30000m, new HireDate(20, 8, 2022), 'F');
                // The third employee is a "security officer" with full permissions.
                // We can represent this by assigning the highest available privilege, DBA.
                empArr[2] = new Employee(103, "Mona Mohamed", SecurityPrivileges.DBA, 90000m, new HireDate(10, 1, 2021), 'F');

                Console.WriteLine("\n--- Displaying Employee Details from Array ---\n");

                // 3. Loop through the array and display the details of each employee.
                foreach (var employee in empArr)
                {
                    Console.WriteLine(employee);
                }

            #endregion

            Console.WriteLine("\n==================================================\n");

            #region Duration Class Demonstration (Part 1)

                Duration D1 = new Duration(1, 10, 15);
                Console.WriteLine($"D1: {D1.ToString()}"); // Hours: 1, Minutes : 10, Seconds :15

                Duration D2 = new Duration(3600);
                Console.WriteLine($"D2 (from 3600s): {D2.ToString()}"); // Hours: 1, Minutes: 0, Seconds:0

                Console.WriteLine("\n--- Operator Overloading Tests ---");

                Duration D3 = D1 + D2;
                Console.WriteLine($"D3 = D1 + D2 -> {D3}");

                D3 = D1 + 666;
                Console.WriteLine($"D3 = D1 + 666 -> {D3}");

                D1 = ++D1; 
                Console.WriteLine($"D1 = ++D1 -> {D1} (Increased by one minute)");

                Console.WriteLine($"Is D1 > D2? -> {D1 > D2}");

                if (D1)
                {
                    Console.WriteLine("If(D1) is true because D1 is not zero.");
                }

                DateTime dt = (DateTime)D1;
                Console.WriteLine($"Explicit conversion to DateTime: {dt}");

            #endregion
        }
    }
}
