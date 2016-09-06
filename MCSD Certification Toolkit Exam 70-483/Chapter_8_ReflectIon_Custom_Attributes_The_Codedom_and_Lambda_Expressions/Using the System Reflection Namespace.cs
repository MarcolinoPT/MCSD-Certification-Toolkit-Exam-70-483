namespace ConsoleApplication16.Chapter_8_ReflectIon_Custom_Attributes_The_Codedom_and_Lambda_Expressions
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Reflection;

    public class Using_the_System_Reflection_Namespace
    {
        public class Assembly_Class
        {
            public static void Example1()
            {
                var myAssembly = Assembly.Load("System.Data, Version=4.0.0.0, Culture=neutral,PublicKeyToken = b77a5c561934e089");
                Console.WriteLine("CodeBase: {0}", myAssembly.CodeBase);
                Console.WriteLine("FullName: {0}", myAssembly.FullName);
                Console.WriteLine("GlobalAssemblyCache: {0}", myAssembly.GlobalAssemblyCache);
                Console.WriteLine("ImageRuntimeVersion: {0}", myAssembly.ImageRuntimeVersion);
                Console.WriteLine("Location: {0}", myAssembly.Location);
            }

            public static void Example2_GetExecutingAssembly_GetTypes()
            {
                var myAssembly = Assembly.GetExecutingAssembly();
                var myAssemblysTypes = myAssembly.GetTypes();
                foreach (Type myType in myAssemblysTypes)
                {
                    Console.WriteLine(string.Format("myType Name: {0}", myType.Name));
                }
            }

            public static void Example3_GetModules()
            {
                var myAssembly = Assembly.Load("System.Data, Version=4.0.0.0, Culture=neutral,PublicKeyToken = b77a5c561934e089");
                var myAssemblysModules = myAssembly.GetModules();
                foreach (Module myModule in myAssemblysModules)
                {
                    Console.WriteLine(string.Format("myModule Name: {0}", myModule.Name));
                }
            }

            public static void Example4_CreateInstance()
            {
                var myAssembly = Assembly.Load("System.Data, Version=4.0.0.0, Culture=neutral,PublicKeyToken = b77a5c561934e089");
                /* The  CreateInstance method will not throw an exception if you pass in a type
                 * name that is not found in the assembly. The return value will be null, so be sure to
                 * check that the name is correct.
                 */
                var dt = (DataTable)myAssembly.CreateInstance("System.Data.DataTable");
                Console.WriteLine("Number of rows: {0}", dt.Rows.Count);
            }

            public static void Example5_GetReferencedAssemblies()
            {
                var myAssembly = Assembly.Load("System.Data, Version=4.0.0.0, Culture=neutral,PublicKeyToken = b77a5c561934e089");
                var referencedAssemblyNames = myAssembly.GetReferencedAssemblies();
                foreach (AssemblyName assemblyName in referencedAssemblyNames)
                {
                    Console.WriteLine(string.Format("Assembly Name: {0}", assemblyName.Name));
                    Console.WriteLine(string.Format("Assembly Version: {0}", assemblyName.Version));
                }
            }
        }

        public class The_System_Type_Class
        {
            public static void Example1()
            {
                var myType = typeof(int);
                // Or you can use the GetType() method on an instance of the type:
                var myIntVariable = 0;
                myType = myIntVariable.GetType();
            }

            public static void Example2()
            {
                const int myIntVariable = 0;
                var myType = myIntVariable.GetType();
                Console.WriteLine("AssmeblyQualifiedName: {0}", myType.AssemblyQualifiedName);
                Console.WriteLine("FullName: {0}", myType.FullName);
                Console.WriteLine("IsValueType: {0}", myType.IsValueType);
                Console.WriteLine("Name: {0}", myType.Name);
                Console.WriteLine("Namespace: {0}", myType.Namespace);
            }

            public static void Example3_GetArrayRank()
            {
                var myIntArray = new int[5, 6, 7];
                var myIntArrayType = myIntArray.GetType();
                Console.WriteLine("Array Rank: {0}", myIntArrayType.GetArrayRank());
            }

            public static void Example4_GetConstructors()
            {
                var myDataTable = new DataTable();
                var myDataTableType = myDataTable.GetType();
                var myDataTableConstructors = myDataTableType.GetConstructors();
                for (int i = 0; i <= myDataTableConstructors.Length - 1; i++)
                {
                    var constructorInfo = myDataTableConstructors[i];
                    Console.WriteLine("\nConstructor #{0}", i + 1);
                    var parameters = constructorInfo.GetParameters();
                    Console.WriteLine("Number Of Parameters: {0}", parameters.Length);
                    foreach (ParameterInfo parameter in parameters)
                    {
                        Console.WriteLine("Parameter Name: {0}", parameter.Name);
                        Console.WriteLine("Parameter Type: {0}",
                        parameter.ParameterType.Name);
                    }
                }
            }

            private enum MyCustomEnum
            {
                Red = 1,
                White = 2,
                Blue = 3
            }

            public static void Example5_GetEnumNames()
            {
                var myCustomEnumType = typeof(MyCustomEnum);
                var enumNames = myCustomEnumType.GetEnumNames();
                foreach (string enumName in enumNames)
                {
                    Console.WriteLine("Name: {0}", enumName);
                }
            }

            public static void Example6_GetEnumValues()
            {
                var myCustomEnumType = typeof(MyCustomEnum);
                var enumValues = myCustomEnumType.GetEnumValues();
                foreach (object enumValue in enumValues)
                {
                    Console.WriteLine("Enum Value: {0}", enumValue.ToString());
                }
            }

            public static void Example7_GetEnumName()
            {
                var myCustomEnumType = typeof(MyCustomEnum);
                for (int i = 1; i <= 3; i++)
                {
                    var enumName = myCustomEnumType.GetEnumName(i);
                    Console.WriteLine(string.Format("{0}: {1}", enumName, i));
                }
            }

            class ReflectionExample
            {
                private string privateField = "Hello";
                public string publicField = "Goodbye";
                internal string internalfield = "Hola";
                protected string protectedField = "Adios";
                static string staticField = "Bonjour";
                public string PrivateField
                {
                    get { return privateField; }
                }
                public double Multiply(double x, double y)
                {
                    return x * y;
                }
                /* This method takes an instance of a class and a  SqlDataReader as a parameter. It first checks if
                 * the  SqlDataReader has rows, and if it does it positions the cursor on the first record. Then it
                 * gets the type for  myClass using the  GetType method. Next, it loops around for each column in
                 * the  SqlDataReader . In the loop it first gets the column name and then tries to retrieve the property
                 * from the object with that name. If the property exists, the value of the property is set to the
                 * data in the column.
                 */
                public static bool LoadClassFromSQLDataReader(object myClass, SqlDataReader dr)
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        var typeOfClass = myClass.GetType();
                        for (int columnIndex = 0; columnIndex <= dr.FieldCount - 1; columnIndex++)
                        {
                            //Get the name of the column
                            var columnName = dr.GetName(columnIndex);
                            //Check if a property exists that matches that name.
                            var propertyInfo = typeOfClass.GetProperty(columnName);
                            if (propertyInfo != null)
                            {
                                //Set the value to the value in the SqlDataReader
                                propertyInfo.SetValue(myClass, dr.GetValue(columnIndex));
                            }
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            public static void Example8_GetFields()
            {
                var reflectionExample = new ReflectionExample();
                var reflectionExampleType = typeof(ReflectionExample);
                var fields = reflectionExampleType.GetFields(BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.NonPublic |
                BindingFlags.FlattenHierarchy);
                foreach (FieldInfo field in fields)
                {
                    var fieldValue = field.GetValue(reflectionExample);
                    Console.WriteLine("Field Name: {0}, Value: {1}", field.Name, fieldValue.ToString());
                }
            }

            public static void Example9_SetValue()
            {
                var reflectionExample = new ReflectionExample();
                var reflectionExampleType = typeof(ReflectionExample);
                reflectionExampleType.GetField("privateField", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(reflectionExample, "My New Value");
                Console.WriteLine("Private Field Value: {0}", reflectionExample.PrivateField);
            }

            public static void Example10_GetMethod()
            {
                var reflectionExample = new ReflectionExample();
                var reflectionExampleType = typeof(ReflectionExample);
                var methodInfo = reflectionExampleType.GetMethod("Multiply");
                var returnValue = (double)methodInfo.Invoke(reflectionExample, new object[] { 4, 5 });
                Console.WriteLine("Return Value: {0}", returnValue);
            }

            public static void Example11_GetMethod()
            {
                var reflectionExample = new ReflectionExample();
                var reflectionExampleType = typeof(ReflectionExample);
                var returnValue = (double)reflectionExampleType.InvokeMember("Multiply",
                BindingFlags.InvokeMethod,
                null,
                reflectionExample,
                new object[] { 4, 5 });
                Console.WriteLine(string.Format("Return Value: {0}", returnValue));
            }

            class Real_World_Scenario
            {
                class Person
                {
                    public int PersonId { get; set; }
                    public string FirstName { get; set; }
                    public string LastName { get; set; }
                    public string Address { get; set; }
                    public string City { get; set; }
                    public string State { get; set; }
                    public string ZipCode { get; set; }
                }

                public bool GetPerson(int personId)
                {
                    //Open the connection to the database.
                    var cn = new SqlConnection("Server=(local);Database=Reflection;Trusted_Connection=True;");
                    cn.Open();
                    //Retrieve the record.
                    var cmd = new SqlCommand(string.Format("SELECT * FROM Person WHERE PersonId = {0}", personId), cn);
                    var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    return ReflectionExample.LoadClassFromSQLDataReader(this, dr);
                }
            }
        }
    }
}