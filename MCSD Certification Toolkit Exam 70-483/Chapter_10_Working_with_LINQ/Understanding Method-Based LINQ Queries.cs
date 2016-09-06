namespace ConsoleApplication16.Chapter_10_Working_with_LINQ
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Understanding_Method_Based_LINQ_Queries
    {
        public static void Example1()
        {
            int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var evenNumbers = myArray.Where((i) => i % 2 == 0);
            foreach (var evenNumber in evenNumbers)
            {
                Console.WriteLine(evenNumber);
            }
        }
        public class Filtering
        {
            public static void Example1()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                var evenNumbers = myArray.Where((i) => i % 2 == 0 && i > 5);
                foreach (var evenNumber in evenNumbers)
                {
                    Console.WriteLine(evenNumber);
                }
            }
            public static void Example2()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                var evenNumbers = myArray.Where((i) => i % 2 == 0).Where((i) => i > 5);
                foreach (var evenNumber in evenNumbers)
                {
                    Console.WriteLine(evenNumber);
                }
            }
            static bool IsEvenAndGT5(int i)
            {
                return (i % 2 == 0 && i > 5);
            }
            public static void Example3()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                var evenNumbers = myArray.Where((i) => IsEvenAndGT5(i));
                foreach (var evenNumber in evenNumbers)
                {
                    Console.WriteLine(evenNumber);
                }
            }
        }
        public class Ordering
        {
            public static void Example1()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                var evenNumbers = myArray.Where((i) => i % 2 == 0).OrderByDescending((i) => i);
                foreach (var evenNumber in evenNumbers)
                {
                    Console.WriteLine(evenNumber);
                }
            }
            class Hometown
            {
                public string City { get; set; }
                public string State { get; set; }
            }
            public static void Example2()
            {
                List<Hometown> hometowns = new List<Hometown>()
                {
                    new Hometown() { City = "Philadelphia", State = "PA" },
                    new Hometown() { City = "Ewing", State = "NJ" },
                    new Hometown() { City = "Havertown", State = "PA" },
                    new Hometown() { City = "Fort Washington", State = "PA" },
                    new Hometown() { City = "Trenton", State = "NJ" }
                };
                var orderedHometowns = hometowns.OrderBy((h) => h.State).ThenBy((h) => h.City);
                foreach (var homeTown in orderedHometowns)
                {
                    Console.WriteLine(homeTown.City + ", " + homeTown.State);
                }
            }
        }
        class Employee
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int StateId { get; set; }
        }
        class State
        {
            public int StateId { get; set; }
            public string StateName { get; set; }
        }
        public class Projection
        {
            class Person
            {
                public string FirstName { get; set; }
                public string LastName { get; set; }
                public string Address1 { get; set; }
                public string City { get; set; }
                public string State { get; set; }
                public string Zip { get; set; }
            }
            public static void Example1()
            {
                List<Person> people = new List<Person>()
                {
                    new Person()
                    {
                        FirstName = "John",
                        LastName = "Smith",
                        Address1 = "First St",
                        City = "Havertown",
                        State = "PA",
                        Zip = "19084"
                    },
                    new Person()
                    {
                        FirstName = "Jane",
                        LastName = "Doe",
                        Address1 = "Second St",
                        City = "Ewing",
                        State = "NJ",
                        Zip = "08560"
                    },
                    new Person()
                    {
                        FirstName = "Jack",
                        LastName = "Jones",
                        Address1 = "Third St",
                        City = "Ft Washington",
                        State = "PA",
                        Zip = "19034"
                    }
                };
                var lastNames = people.Select((p) => p.LastName);
                foreach (string lastName in lastNames)
                {
                    Console.WriteLine(lastName);
                }
            }
            public static void Example2()
            {
                List<Person> people = new List<Person>()
                {
                    new Person()
                    {
                        FirstName = "John",
                        LastName = "Smith",
                        Address1 = "First St",
                        City = "Havertown",
                        State = "PA",
                        Zip = "19084"
                    },
                    new Person()
                    {
                        FirstName = "Jane",
                        LastName = "Doe",
                        Address1 = "Second St",
                        City = "Ewing",
                        State = "NJ",
                        Zip = "08560"
                    },
                    new Person()
                    {
                        FirstName = "Jack",
                        LastName = "Jones",
                        Address1 = "Third St",
                        City = "Ft Washington",
                        State = "PA",
                        Zip = "19034"
                    }
                };
                var names = people.Select((p) => new { p.FirstName, p.LastName });
                foreach (var name in names)
                {
                    Console.WriteLine(name.FirstName + ", " + name.LastName);
                }
            }
            public static void Example3()
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
                        FirstName = "John",
                        LastName = "Smith",
                        StateId = 1
                    }
                };
                List<State> states = new List<State>()
                {
                    new State()
                    {
                        StateId = 1,
                        StateName = "PA"
                    },
                    new State()
                    {
                    StateId = 2,
                    StateName = "NJ"
                    }
                };
                var employeeByState = employees.SelectMany((e) => states.Where((s) => e.StateId == s.StateId).Select((s) => new { e.LastName, s.StateName }));
                foreach (var employee in employeeByState)
                {
                    Console.WriteLine(employee.LastName + ", " + employee.StateName);
                }
            }
        }
        public class Joining
        {
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
                        FirstName = "John",LastName = "Smith",
                        StateId = 1
                    }
                };
                List<State> states = new List<State>()
                {
                    new State()
                    {
                        StateId = 1,
                        StateName = "PA"
                    },
                    new State()
                    {
                        StateId = 2,
                        StateName = "NJ"
                    }
                };
                var employeeByState = employees.Join(states, (e) => e.StateId, (s) => s.StateId, (e, s) => new { e.LastName, s.StateName });
                foreach (var employee in employeeByState)
                {
                    Console.WriteLine(employee.LastName + ", " + employee.StateName);
                }
            }
        }
        public class Outer_Join
        {
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
                    },
                    new Employee()
                    {
                        FirstName = "Sue",
                        LastName = "Smith",
                        StateId = 3
                    }
                };
                List<State> states = new List<State>()
                {
                    new State()
                    {
                        StateId = 1,
                        StateName = "PA"
                    },
                    new State()
                    {
                        StateId = 2,
                        StateName = "NJ"
                    }
                };
                var employeeByState = employees.GroupJoin(states,
                    e => e.StateId,
                    s => s.StateId,
                    (e, employeeGroup) => employeeGroup.Select(s => new
                    {
                        LastName = e.LastName,
                        StateName = s.StateName
                    }).DefaultIfEmpty(new
                    {
                        LastName = e.LastName,
                        StateName = ""
                    })).SelectMany(employeeGroup => employeeGroup);
                foreach (var employee in employeeByState)
                {
                    Console.WriteLine(employee.LastName + ", " + employee.StateName);
                }
            }
        }
        public class Composite_Keys
        {
            class Hometown
            {
                public string City { get; set; }
                public string State { get; set; }
                public string CityCode { get; set; }
            }
            class Employee
            {
                public string FirstName { get; set; }
                public string LastName { get; set; }
                public string City { get; set; }
                public string State { get; set; }
            }
            public static void Example1()
            {
                List<Employee> employees = new List<Employee>()
                {
                    new Employee()
                    {
                        FirstName = "John",
                        LastName = "Smith",
                        City = "Havertown",
                        State = "PA"
                    },
                    new Employee()
                    {
                        FirstName = "Jane",
                        LastName = "Doe",
                        City = "Ewing",
                        State = "NJ"
                    },
                    new Employee()
                    {
                        FirstName = "Jack",
                        LastName = "Jones",
                        City = "Fort Washington",
                        State = "PA"
                    }
                };
                List<Hometown> hometowns = new List<Hometown>()
                {
                    new Hometown()
                    {
                        City = "Havertown",
                        State = "PA",
                        CityCode = "1234"
                    },
                    new Hometown()
                    {
                        City = "Ewing",
                        State = "NJ",
                        CityCode = "5678"
                    },
                    new Hometown()
                    {
                        City = "Fort Washington",
                        State = "PA",
                        CityCode = "9012"
                    }
                };
                var employeeByState = employees.Join(hometowns,
                    e => new { City = e.City, State = e.State },
                    h => new { City = h.City, State = h.State },
                    (e, h) => new { e.LastName, h.CityCode });
                foreach (var employee in employeeByState)
                {
                    Console.WriteLine(employee.LastName + ", " + employee.CityCode);
                }
            }
        }
        public class Grouping
        {
            class Employee
            {
                public string FirstName { get; set; }
                public string LastName { get; set; }
                public string City { get; set; }
                public string State { get; set; }
            }
            static List<Employee> employees = new List<Employee>()
            {
                new Employee()
                {
                    FirstName = "John",
                    LastName = "Smith",
                    City = "Havertown",
                    State = "PA"
                },
                new Employee()
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    City = "Ewing",
                    State = "NJ"
                },
                new Employee()
                {
                    FirstName = "Jack",
                    LastName = "Jones",
                    City = "Fort Washington",
                    State = "PA"
                }
            };
            public static void Example1()
            {
                var employeesByState = employees.GroupBy(e => e.State);
                foreach (var employeeGroup in employeesByState)
                {
                    Console.WriteLine(employeeGroup.Key + ": " + employeeGroup.Count());
                    foreach (var employee in employeeGroup)
                    {
                        Console.WriteLine(employee.LastName + ", " + employee.State);
                    }
                }
            }
            public static void Example2()
            {
                var employeesByState = employees.GroupBy(e => new { e.City, e.State });
                foreach (var employeeGroup in employeesByState)
                {
                    Console.WriteLine(employeeGroup.Key + ": " + employeeGroup.Count());
                    foreach (var employee in employeeGroup)
                    {
                        Console.WriteLine(employee.LastName + ", " + employee.State);
                    }
                }
            }
        }
        public class Aggregate_Functions
        {
            public static void Count()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int count = myArray.Where(i => i % 2 == 0).Count();
                Console.WriteLine(count);
                //Alternatively, you could write the query expression as follows if you want to defer the execution of
                //the query:
                //var evenNumbers = from i in myArray
                //                  where i % 2 == 0
                //                  select i;
                //count = evenNumbers.Count();
            }
            public static void Average()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                double average = myArray.Where(i => i % 2 == 0).Average();
                Console.WriteLine(average);
            }
            public static void Sum()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int sum = myArray.Where(i => i % 2 == 0).Sum();
                Console.WriteLine(sum);
            }
            public static void Min()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int min = myArray.Where(i => i % 2 == 0).Min();
                Console.WriteLine(min);
            }
            public static void Max()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int max = myArray.Where(i => i % 2 == 0).Max();
                Console.WriteLine(max);
            }
            public static void First()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int first = myArray.Where(i => i % 2 == 0).First();
                Console.WriteLine(first);
            }
            public static void Last()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int last = myArray.Where(i => i % 2 == 0).Last();
                Console.WriteLine(last);
            }
        }
        public class Concatenation
        {
            class Person
            {
                public string FirstName { get; set; }
                public string LastName { get; set; }
                public string Address1 { get; set; }
                public string City { get; set; }
                public string State { get; set; }
                public string Zip { get; set; }
            }
            static List<Employee> employees = new List<Employee>()
            {
                new Employee()
                {
                    FirstName = "John",
                    LastName = "Smith"
                },
                new Employee()
                {
                    FirstName = "Jane",
                    LastName = "Doe"
                },
                new Employee()
                {
                    FirstName = "Jack",
                    LastName = "Jones"
                }
            };
            public static void Example1()
            {
                List<Employee> employees2 = new List<Employee>()
                {
                    new Employee()
                    {
                        FirstName = "Bill",
                        LastName = "Peters"
                    },
                    new Employee()
                    {
                        FirstName = "Bob",
                        LastName = "Donalds"
                    },
                    new Employee()
                    {
                        FirstName = "Chris",
                        LastName = "Jacobs"
                    }
                };
                var combinedEmployees = employees.Concat(employees2);
                foreach (var employee in combinedEmployees)
                {
                    Console.WriteLine(employee.LastName);
                }
            }
            public static void Example2()
            {
                List<Person> people = new List<Person>()
                {
                    new Person()
                    {
                        FirstName = "Bill",
                        LastName = "Peters"
                    },
                    new Person()
                    {
                        FirstName = "Bob",
                        LastName = "Donalds"
                    },
                    new Person()
                    {
                        FirstName = "Chris",
                        LastName = "Jacobs"
                    }
                };
                var combinedEmployees = employees.Select(e => new { Name = e.LastName }).Concat(people.Select(p => new { Name = p.LastName }));
                foreach (var employee in combinedEmployees)
                {
                    Console.WriteLine(employee.Name);
                }
            }
        }
        public class Skip_and_Take
        {
            static List<Employee> employees = new List<Employee>()
            {
                new Employee()
                {
                    FirstName = "John",
                    LastName = "Smith"
                },
                new Employee()
                {
                    FirstName = "Jane",
                    LastName = "Doe"
                },
                new Employee()
                {
                    FirstName = "Jack",
                    LastName = "Jones"
                }
            };
            public static void Example1()
            {
                var newEmployees = employees.Skip(1);
                foreach (var employee in newEmployees)
                {
                    Console.WriteLine(employee.LastName + ", " + employee.FirstName);
                }
            }
            public static void Example2()
            {
                var newEmployees = employees.Take(2);
                foreach (var employee in newEmployees)
                {
                    Console.WriteLine(employee.LastName + ", " + employee.FirstName);
                }
            }
            public static void Example3()
            {
                var newEmployees = employees.Skip(20).Take(10);
                foreach (var employee in newEmployees)
                {
                    Console.WriteLine(employee.LastName + ", " + employee.FirstName);
                }
            }
        }
        public class Distinct
        {
            public static void Example1()
            {
                int[] myArray = new int[] { 1, 2, 3, 1, 2, 3, 1, 2, 3 };
                var distinctArray = myArray.Distinct();
                foreach (int i in distinctArray)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}