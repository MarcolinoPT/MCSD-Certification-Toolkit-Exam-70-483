namespace ConsoleApplication16.Chapter_9_Working_with_Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Woking_with_Data_Collections
    {
        public class Arrays
        {
            public static void Example1()
            {
                int[] mySet = new int[5];
                mySet[0] = 1;
                mySet[1] = 2;
                mySet[2] = 3;
                mySet[3] = 4;
                mySet[4] = 5;
            }

            public static void Example2()
            {
                int[,] mySet = new int[3, 2];
                mySet[0, 0] = 1;
                mySet[0, 1] = 2;
                mySet[1, 0] = 3;
                mySet[1, 1] = 4;
                mySet[2, 0] = 5;
                mySet[2, 1] = 6;

                /* The preceding code created a two-dimensional array with three elements in the first dimension
                 * and two elements in the second dimension. Conceptually, this is like having a table with rows and
                 * columns.
                 */
            }
        }

        public class ArrayLists
        {
            public static void Example1()
            {
                ArrayList myList = new ArrayList();
                myList.Add(1);
                myList.Add("hello world");
                myList.Add(new DateTime(2012, 01, 01));
            }

            public static void Example2()
            {
                ArrayList myList = new ArrayList();
                myList.Add(4);
                myList.Add(1);
                myList.Add(5);
                myList.Add(3);
                myList.Add(2);
                myList.Sort();
                foreach (int i in myList)
                {
                    Console.WriteLine(i.ToString());
                }
            }

            class MyObject : IComparable
            {
                public int ID { get; set; }
                public int CompareTo(object obj)
                {
                    MyObject obj1 = obj as MyObject;
                    return this.ID.CompareTo(obj1.ID);
                }
            }

            public static void Example3()
            {
                ArrayList myList = new ArrayList();
                myList.Add(new MyObject() { ID = 4 });
                myList.Add(new MyObject() { ID = 1 });
                myList.Add(new MyObject() { ID = 5 });
                myList.Add(new MyObject() { ID = 3 });
                myList.Add(new MyObject() { ID = 2 });
                myList.Sort();
                int foundIndex = myList.BinarySearch(new MyObject() { ID = 4 });
                if (foundIndex >= 0)
                {
                    Console.WriteLine(((MyObject)myList[foundIndex]).ID.ToString());
                }
                else
                {
                    Console.WriteLine("Element not found");
                }
            }
        }

        public class Hashtables
        {
            public static void Example1()
            {
                Hashtable myHashtable = new Hashtable();
                myHashtable.Add(1, "one");
                myHashtable.Add("two", 2);
                myHashtable.Add(3, "three");
                Console.WriteLine(myHashtable[1].ToString());
                Console.WriteLine(myHashtable["two"].ToString());
                Console.WriteLine(myHashtable[3].ToString());
            }
        }

        public class Queues
        {
            public static void Example1()
            {
                Queue myQueue = new Queue();
                myQueue.Enqueue("first");
                myQueue.Enqueue("second");
                myQueue.Enqueue("third");
                int count = myQueue.Count;
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine(myQueue.Dequeue());
                }
            }
        }

        public class SortedLists
        {
            public static void Example1()
            {
                SortedList mySortedList = new SortedList();
                mySortedList.Add(3, "three");
                mySortedList.Add(2, "second");
                mySortedList.Add(1, "first");
                foreach (DictionaryEntry item in mySortedList)
                {
                    Console.WriteLine(item.Value);
                }
            }
        }

        public class Stacks
        {
            public static void Example1()
            {
                Stack myStack = new Stack();
                myStack.Push("first");
                myStack.Push("second");
                myStack.Push("third");
                int count = myStack.Count;
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine(myStack.Pop());
                }
            }
        }

        public class Dictionaries
        {
            class MyRecord
            {
                public int ID { get; set; }
                public string FirstName { get; set; }
                public string LastName { get; set; }
            }
            public static void Sample1()
            {
                Dictionary<int, MyRecord> myDictionary = new Dictionary<int, MyRecord>();
                myDictionary.Add(5, new MyRecord()
                {
                    ID = 5,
                    FirstName = "Bob",
                    LastName = "Smith"
                });
                myDictionary.Add(2, new MyRecord()
                {
                    ID = 2,
                    FirstName = "Jane",
                    LastName = "Doe"
                });
                myDictionary.Add(10, new MyRecord()
                {
                    ID = 10,
                    FirstName = "Bill",
                    LastName = "Jones"
                });
                Console.WriteLine(myDictionary[5].FirstName);
                Console.WriteLine(myDictionary[2].FirstName);
                Console.WriteLine(myDictionary[10].FirstName);
            }
        }

        public class Lists
        {
            public static void Example1()
            {
                List<int> myList = new List<int>();
                myList.Add(1);
                myList.Add(2);
                myList.Add(3);
            }
        }

        public class CustomCollections
        {
            class Person
            {
                public int PersonId { get; set; }
                public string FName { get; set; }
                public string LName { get; set; }
                public string Address { get; set; }
                public string City { get; set; }
                public string State { get; set; }
                public string ZipCode { get; set; }
            }

            class PersonCollection : CollectionBase
            {
                public void Add(Person person)
                {
                    List.Add(person);
                }
                public void Insert(int index, Person person)
                {
                    List.Insert(index, person);
                }
                public void Remove(Person person)
                {
                    List.Remove(person);
                }
                public Person this[int index]
                {
                    get
                    {
                        return (Person)List[index];
                    }
                    set
                    {
                        List[index] = value;
                    }
                }
            }

            public static void Example1()
            {
                PersonCollection persons = new PersonCollection();
                persons.Add(new Person()
                {
                    PersonId = 1,
                    FName = "John",
                    LName = "Smith"
                });
                persons.Add(new Person()
                {
                    PersonId = 2,
                    FName = "Jane",
                    LName = "Doe"
                });
                persons.Add(new Person()
                {
                    PersonId = 3,
                    FName = "Bill Jones",
                    LName = "Smith"
                });
                foreach (Person person in persons)
                {
                    Console.WriteLine(person.FName);
                }
            }
        }
    }
}