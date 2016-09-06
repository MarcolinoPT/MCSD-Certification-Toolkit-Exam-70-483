namespace ConsoleApplication16.Chapter_6_Working_with_Delegates_Events_and_Exceptions
{
    using System;

    public class Working_with_Delegates
    {
        private delegate float FunctionDelegate(float x);
        private FunctionDelegate TheFunction;

        // y = 12 * Sin(3 * x) / (1 + |x|)
        private static float Function1(float x)
        {
            return (float)(12 * Math.Sin(3 * x) / (1 + Math.Abs(x)));
        }

        public void RunFunction1()
        {
            TheFunction = Function1;
            var handler = TheFunction;
            if (handler != null)
                Console.WriteLine(handler(1.23f));
        }

        //Action del1 = Method1;
        //Action del2 = Method2;
        //Action del3 = del1 + del2 + del1;

        class Person
        {
            public string Name;
            // A method that returns a string.
            public delegate string GetStringDelegate();
            // A static method.
            public static string StaticName()
            {
                return "Static";
            }
            // Return this instance's Name.
            public string GetName()
            {
                return Name;
            }
            // Variables to hold GetStringDelegates.
            public GetStringDelegate StaticMethod;
            public GetStringDelegate InstanceMethod;
        }

        public void RunStaticAndInstanceDelegates()
        {
            // Make some Persons.
            var alice = new Person() { Name = "Alice" };
            var bob = new Person() { Name = "Bob" };
            // Make Alice's InstanceMethod variable refer to her own GetName method.
            alice.InstanceMethod = alice.GetName;
            alice.StaticMethod = Person.StaticName;
            // Make Bob's InstanceMethod variable refer to Alice's GetName method.
            bob.InstanceMethod = alice.GetName;
            bob.StaticMethod = Person.StaticName;
            // Demonstrate the methods.
            var result = string.Empty;
            result += "Alice's InstanceMethod returns: " + alice.InstanceMethod() +
            Environment.NewLine;
            result += "Bob's InstanceMethod returns: " + bob.InstanceMethod() +
            Environment.NewLine;
            result += "Alice's StaticMethod returns: " + alice.StaticMethod() +
            Environment.NewLine;
            result += "Bob's StaticMethod returns: " + bob.StaticMethod();
            Console.WriteLine(result);
        }

        class Employee : Person { }

        // A delegate that returns a Person.
        private delegate Person ReturnPersonDelegate();
        private ReturnPersonDelegate ReturnPersonMethod;
        // A method that returns an Employee.
        private Employee ReturnEmployee()
        {
            return new Employee();
        }
        // A delegate that takes an Employee as a parameter.
        private delegate void EmployeeParameterDelegate(Employee employee);
        private EmployeeParameterDelegate EmployeeParameterMethod;
        // A method that takes a Person as a parameter.
        private void PersonParameter(Person person)
        {
        }
        // Initialize delegate variables.
        public void RunCovarianceAndContravarianceDelegates()
        {
            // Use covariance to set ReturnPersonMethod = ReturnEmployee.
            ReturnPersonMethod = ReturnEmployee;
            // Use contravariance to set EmployeeParameterMethod = PersonParameter.
            EmployeeParameterMethod = PersonParameter;
        }
    }
}