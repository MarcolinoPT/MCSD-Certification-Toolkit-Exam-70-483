namespace ConsoleApplication16.Chapter_10_Working_with_LINQ
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class Utilizing_LINQ_to_XML
    {
        class Employee
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int StateId { get; set; }
        }
        public static void Example1()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee()
                {
                    FirstName = "John",
                    LastName = "Smith",
                    StateId = 1
                },
                new Employee()
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    StateId = 2
                },
                new Employee()
                {
                    FirstName = "Jack",
                    LastName = "Jones",
                    StateId = 1
                }
            };
            var xmlEmployees = new XElement("Root", from e in employees
                                                    select new XElement("Employee",
                                                        new XElement("FirstName", e.FirstName),
                                                        new XElement("LastName", e.LastName)));
            Console.WriteLine(xmlEmployees);
        }
    }
}