namespace ConsoleApplication16.Chapter_9_Working_with_Data
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Runtime.Serialization.Json;
    using System.Xml.Serialization;

    public class Understanding_Serialization
    {
        public class Binary
        {
            [Serializable]
            public class Person
            {
                // [NonSerialized] Uncomment to not serialize field
                private int _id;
                // [XmlIgnore] Uncomment to not serialize Property
                public string FirstName;
                public string LastName;
                public void SetId(int id)
                {
                    _id = id;
                }
                [OnSerializing()]
                internal void OnSerializingMethod(StreamingContext context)
                {
                    FirstName = "Bob";
                }
                [OnSerialized()]
                internal void OnSerializedMethod(StreamingContext context)
                {
                    FirstName = "Serialize Complete";
                }
                [OnDeserializing()]
                internal void OnDeserializingMethod(StreamingContext context)
                {
                    FirstName = "John";
                }
                [OnDeserialized()]
                internal void OnDeserializedMethod(StreamingContext context)
                {
                    FirstName = "Deserialize Complete";
                }
            }

            public static void Example1()
            {
                Person person = new Person();
                person.SetId(1);
                person.FirstName = "Joe";
                person.LastName = "Smith";
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("Person.bin", FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, person);
                stream.Close();
                stream = new FileStream("Person.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                Person person2 = (Person)formatter.Deserialize(stream);
                stream.Close();
            }
        }

        public class XML
        {
            [Serializable]
            public class Person
            {
                private int _id;
                // [XmlIgnore] Uncomment to not serialize Property
                public string FirstName;
                public string LastName;
                public void SetId(int id)
                {
                    _id = id;
                }
                [OnSerializing()]
                internal void OnSerializingMethod(StreamingContext context)
                {
                    FirstName = "Bob";
                }
                [OnSerialized()]
                internal void OnSerializedMethod(StreamingContext context)
                {
                    FirstName = "Serialize Complete";
                }
                [OnDeserializing()]
                internal void OnDeserializingMethod(StreamingContext context)
                {
                    FirstName = "John";
                }
                [OnDeserialized()]
                internal void OnDeserializedMethod(StreamingContext context)
                {
                    FirstName = "Deserialize Complete";
                }
            }

            public static void Example2()
            {
                Person person = new Person();
                person.SetId(1);
                person.FirstName = "Joe";
                person.LastName = "Smith";
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));
                StreamWriter streamWriter = new StreamWriter("Person.xml");
                xmlSerializer.Serialize(streamWriter, person);
                streamWriter.Close();
                FileStream fs = new FileStream("Person.xml", FileMode.Open);
                person = (Person)xmlSerializer.Deserialize(fs);
            }
        }

        public class JSON
        {
            [DataContract]
            public class Person
            {
                [DataMember]
                private int _id;
                [DataMember]
                public string FirstName;
                [DataMember]
                public string LastName;
                public void SetId(int id)
                {
                    _id = id;
                }
                [OnSerializing()]
                internal void OnSerializingMethod(StreamingContext context)
                {
                    FirstName = "Bob";
                }
                [OnSerialized()]
                internal void OnSerializedMethod(StreamingContext context)
                {
                    FirstName = "Serialize Complete";
                }
                [OnDeserializing()]
                internal void OnDeserializingMethod(StreamingContext context)
                {
                    FirstName = "John";
                }
                [OnDeserialized()]
                internal void OnDeserializedMethod(StreamingContext context)
                {
                    FirstName = "Deserialize Complete";
                }
            }

            public static void Example1()
            {
                Person person = new Person();
                person.SetId(1);
                person.FirstName = "Joe";
                person.LastName = "Smith";
                Stream stream = new FileStream("Person.json", FileMode.Create);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));
                ser.WriteObject(stream, person);
                stream.Close();
                stream = new FileStream("Person.json", FileMode.Open);
                ser = new DataContractJsonSerializer(typeof(Person));
                person = (Person)ser.ReadObject(stream);
                stream.Close();
            }
        }

        public class Implementing_ISerializable
        {
            [Serializable]
            public class Person : ISerializable
            {
                private int _id;
                public string FirstName;
                public string LastName;
                public void SetId(int id)
                {
                    _id = id;
                }
                public Person() { }
                public Person(SerializationInfo info, StreamingContext context)
                {
                    FirstName = info.GetString("custom field 1");
                    LastName = info.GetString("custom field 2");
                }
                public void GetObjectData(SerializationInfo info, StreamingContext context)
                {
                    info.AddValue("custom field 1", FirstName);
                    info.AddValue("custom field 2", LastName);
                }
            }
        }
    }
}