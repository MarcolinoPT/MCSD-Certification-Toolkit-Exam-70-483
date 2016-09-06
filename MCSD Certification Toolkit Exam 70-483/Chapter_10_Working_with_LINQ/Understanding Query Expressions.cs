namespace ConsoleApplication16.Chapter_10_Working_with_LINQ
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class Understanding_Query_Expressions
    {
        public static void Example1()
        {
            int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var evenNumbers = from i in myArray
                              where i % 2 == 0
                              select i;
            foreach (var number in evenNumbers)
            {
                Console.WriteLine(number);
            }
        }
        public static void Example2()
        {
            int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var evenNumbers = from i in myArray
                              where i % 2 == 0
                              select i;
            foreach (var number in evenNumbers)
            {
                Console.WriteLine(number);
            }
            myArray[1] = 12;
            foreach (var number in evenNumbers)
            {
                Console.WriteLine(number);
            }
        }
        public class Filtering
        {
            public static void Example1()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                var evenNumbers = from i in myArray
                                  where i % 2 == 0 && i > 5
                                  select i;
                foreach (var number in evenNumbers)
                {
                    Console.WriteLine(number);
                }
            }
            public static void Example2()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                var evenNumbers = from i in myArray
                                  where i % 2 == 0
                                  where i > 5
                                  select i;
                foreach (var number in evenNumbers)
                {
                    Console.WriteLine(number);
                }
            }
            public static void Example3()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                var evenNumbers = from i in myArray
                                  where IsEvenAndGT5(i)
                                  select i;
                foreach (int i in evenNumbers)
                {
                    Console.WriteLine(i);
                }
            }
            static bool IsEvenAndGT5(int i)
            {
                return (i % 2 == 0 && i > 5);
            }
            public class Ordering
            {
                public static void Example1()
                {
                    int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                    var evenNumbers = from i in myArray
                                      where i % 2 == 0
                                      orderby i descending
                                      select i;
                    foreach (var number in evenNumbers)
                    {
                        Console.WriteLine(number);
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
                    var orderHometowns = from town in hometowns
                                         orderby town.State ascending, town.City ascending
                                         select town;
                    foreach (var hometown in orderHometowns)
                    {
                        Console.WriteLine(hometown.City + ", " + hometown.State);
                    }
                }
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
                static List<Person> people = new List<Person>()
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
                public static void Example1()
                {
                    var lastNames = from p in people
                                    select p.LastName;
                    foreach (var lastName in lastNames)
                    {
                        Console.WriteLine(lastName);
                    }
                }
                public static void Example2()
                {
                    var names = from p in people
                                select new { p.FirstName, p.LastName };
                    foreach (var name in names)
                    {
                        Console.WriteLine(name);
                        Console.WriteLine(name.FirstName + ", " + name.LastName);
                    }
                }
                public static void Example3()
                {
                    var names = from p in people
                                select new { First = p.FirstName, Last = p.LastName };
                    foreach (var name in names)
                    {
                        Console.WriteLine(name);
                        Console.WriteLine(name.First + ", " + name.Last);
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
                            FirstName = "Jack",
                            LastName = "Jones",
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
                    var employeeByState = from e in employees
                                          join s in states
                                          on e.StateId equals s.StateId
                                          select new { e.LastName, e.FirstName };
                    foreach (var employee in employeeByState)
                    {
                        Console.WriteLine(employee);
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
                    var employeeByState = from e in employees
                                          join s in states
                                          on e.StateId equals s.StateId into employeeGroup
                                          from item in employeeGroup.DefaultIfEmpty(new State { StateId = 0, StateName = "" })
                                          select new { e.LastName, item.StateName };
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
                    var employeeByState = from e in employees
                                          join h in hometowns
                                          on new { City = e.City, State = e.State } equals new { City = h.City, State = h.State }
                                          select new { e.LastName, h.CityCode };
                    foreach (var employee in employeeByState)
                    {
                        Console.WriteLine(employee);
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
                    var employeesByState = from e in employees
                                           group e by e.State;
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
                    int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                    var groupedNumbers = from i in myArray
                                         group i by (i % 2 == 0 ? "Even" : "Odd");
                    foreach (var groupNumber in groupedNumbers)
                    {
                        Console.WriteLine(groupNumber.Key + ": " + groupNumber.Sum());
                        foreach (var number in groupNumber)
                        {
                            Console.WriteLine(number);
                        }
                    }
                }
                public static void Example3()
                {
                    int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                    var groupedNumbers = from i in myArray
                                         group i by (i % 2 == 0 ? "Even" : "Odd") into g
                                         select new { Key = g.Key, SumOfNumbers = g.Sum() };
                    foreach (var groupedNumber in groupedNumbers)
                    {
                        Console.WriteLine(groupedNumber.Key + ": " + groupedNumber.SumOfNumbers);
                    }
                }
            }
        }
        public class Aggregate_Functions
        {
            public static void Count()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int count = (from i in myArray
                             where i % 2 == 0
                             select i).Count();
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
                double average = (from i in myArray
                                  where i % 2 == 0
                                  select i).Average();
                Console.WriteLine(average);
            }
            public static void Sum()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int sum = (from i in myArray
                           where i % 2 == 0
                           select i).Sum();
                Console.WriteLine(sum);
            }
            public static void Min()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int min = (from i in myArray
                           where i % 2 == 0
                           select i).Min();
                Console.WriteLine(min);
            }
            public static void Max()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int max = (from i in myArray
                           where i % 2 == 0
                           select i).Max();
                Console.WriteLine(max);
            }
            public static void First()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int first = (from i in myArray
                             where i % 2 == 0
                             select i).First();
                Console.WriteLine(first);
            }
            public static void Last()
            {
                int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int last = (from i in myArray
                            where i % 2 == 0
                            select i).Last();
                Console.WriteLine(last);
            }
        }
    }
}