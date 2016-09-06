namespace ConsoleApplication16.Chapter_8_ReflectIon_Custom_Attributes_The_Codedom_and_Lambda_Expressions
{
    using System;
    using System.Data.SqlClient;
    using System.Reflection;

    public class Read_and_Create_Custom_Attributes
    {
        public static void Example1_ReadAttributes()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyNames = assembly.GetReferencedAssemblies();
            foreach (AssemblyName assemblyName in assemblyNames)
            {
                Console.WriteLine("Assembly Name: {0}", assemblyName.FullName);
                var referencedAssembly = Assembly.Load(assemblyName.FullName);
                var attributes = referencedAssembly.GetCustomAttributes(false);
                foreach (object attribute in attributes)
                {
                    Console.WriteLine("Attribute Name: {0}", attribute.GetType().Name);
                    //Get the properties of this attribute
                    var properties = attribute.GetType().GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        Console.WriteLine("{0} : {1}", property.Name, property.GetValue(attribute));
                    }
                }
            }
        }

        [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
        class MyCustomAttribute : System.Attribute
        {
            public enum MyCustomAttributeEnum
            {
                Red,
                White,
                Blue
            }
            public bool Property1 { get; set; }
            public string Property2 { get; set; }
            public MyCustomAttributeEnum Property3 { get; set; }
        }

        [MyCustom(Property1 = true, Property2 = "Hello World", Property3 = MyCustomAttribute.MyCustomAttributeEnum.Red)]
        class MyTestClass { }

        public static void Example2_CreateAttributes()
        {
            var myTestClassType = typeof(MyTestClass);
            var attribute = (MyCustomAttribute)myTestClassType.GetCustomAttribute(typeof(MyCustomAttribute), false);
            Console.WriteLine("Property1: {0}", attribute.Property1);
            Console.WriteLine("Property2: {0}", attribute.Property2);
            Console.WriteLine("Property3: {0}", attribute.Property3);
        }

        class Real_World_Scenario
        {
            [System.AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
            class DataMappingAttribute : System.Attribute
            {

                public DataMappingAttribute(string columnName, string propertyName)
                {
                    this.ColumnName = columnName;
                    this.PropertyName = propertyName;
                }
                public string ColumnName { get; set; }
                public string PropertyName { get; set; }
            }

            [DataMapping("FirstName", "FName")]
            [DataMapping("LastName", "LName")]
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

                public static bool LoadClassFromSQLDataReader(object myClass, SqlDataReader sqlDataReader)
                {
                    if (sqlDataReader.HasRows)
                    {
                        sqlDataReader.Read();
                        var typeOfClass = myClass.GetType();
                        var dataMappingAttributes = typeOfClass.GetCustomAttributes(typeof(DataMappingAttribute), false);
                        for (int columnIndex = 0; columnIndex <= sqlDataReader.FieldCount - 1; columnIndex++)
                        {
                            //Get the name of the column.
                            var columnName = sqlDataReader.GetName(columnIndex);
                            //Check if a property exists that matches that name.
                            PropertyInfo propertyInfo = null;
                            //Check if an attribute exists that maps this column to a property.
                            foreach (DataMappingAttribute dataMappingAttribute in dataMappingAttributes)
                            {
                                if (dataMappingAttribute.ColumnName == columnName)
                                {
                                    propertyInfo = typeOfClass.GetProperty(dataMappingAttribute.PropertyName);
                                    break;
                                }
                            }
                            //The the property was mapped explicitely then try to find a
                            //property with the same name as the column.
                            if (propertyInfo == null)
                            {
                                propertyInfo = typeOfClass.GetProperty(columnName);
                            }
                            //If you found a property then set its value.
                            if (propertyInfo != null)
                            {
                                //Set the value to the value in the SqlDataReader
                                propertyInfo.SetValue(myClass, sqlDataReader.GetValue(columnIndex));
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
        }
    }
}