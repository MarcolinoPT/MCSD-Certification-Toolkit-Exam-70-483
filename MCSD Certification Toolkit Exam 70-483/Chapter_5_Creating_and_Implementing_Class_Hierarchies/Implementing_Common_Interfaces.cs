namespace ConsoleApplication16.Chapter_5_Creating_and_Implementing_Class_Hierarchies
{
    using System;

    public class Implementing_Common_Interfaces
    {
        public class IEquatable_Example
        {
            public class Person : IEquatable<Person>
            {
                public string FirstName { get; set; }
                public string LastName { get; set; }

                public override bool Equals(object obj)
                {
                    return this.IsPersonEqual((Person)obj);
                }

                public bool Equals(Person other)
                {
                    return this.IsPersonEqual(other);
                }

                private bool IsPersonEqual(Person other)
                {
                    return this.FirstName.CompareTo(other.FirstName) == 0 && this.LastName.CompareTo(other.LastName) == 0;
                }

                public override int GetHashCode()
                {
                    return this.FirstName.GetHashCode() + this.LastName.GetHashCode();
                }

                public static bool operator ==(Person a, Person b)
                {
                    // If both are null, or both are same instance, return true.
                    if (System.Object.ReferenceEquals(a, b))
                    {
                        return true;
                    }

                    // If one is null, but not both, return false.
                    if (((object)a == null) || ((object)b == null))
                    {
                        return false;
                    }

                    // Return true if the fields match:
                    return a.FirstName == b.FirstName && a.LastName == b.LastName;
                }

                public static bool operator !=(Person a, Person b)
                {
                    return !(a == b);
                }
            }
        }
    }
}