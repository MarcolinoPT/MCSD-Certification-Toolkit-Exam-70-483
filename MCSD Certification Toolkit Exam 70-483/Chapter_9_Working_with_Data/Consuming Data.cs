namespace ConsoleApplication16.Chapter_9_Working_with_Data
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class Consuming_Data
    {
        public class Working_with_ADO_NET
        {
            public void Connection()
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id = myUsername; Password = myPassword;";
                cn.Open();
            }

            public void Command_ExecuteNonQuery()
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id = myUsername; Password = myPassword;";
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Person (FirstName, LastName) " +
                "VALUES ('Joe', 'Smith')";
                cmd.ExecuteNonQuery();
                cn.Close();
            }

            public void Command_ExecuteNonQuery_StoredProcedure()
            {
                /* CREATE PROCEDURE PersonInsert
                 * @FirstName varchar(50),
                 * @LastName varchar(50)
                 * AS
                 * BEGIN
                 * INSERT INTO PERSON (FirstName, LastName) VALUES (@FirstName, @LastName)
                 * END
                 */
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id = myUsername; Password = myPassword;";
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PersonInsert";
                cmd.Parameters.Add(new SqlParameter("@FirstName", "Joe"));
                cmd.Parameters.Add(new SqlParameter("@LastName", "Smith"));
                cmd.ExecuteNonQuery();
            }

            public void Command_ExecuteReader()
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id = myUsername; Password = myPassword;";
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Person";
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Console.WriteLine(string.Format("First Name: {0} , Last Name: {1}", dr["FirstName"], dr["LastName"]));
                    }
                }
                dr.Close();
                cn.Close();
            }

            public void Command_ExecuteScalar()
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id = myUsername; Password = myPassword;";
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT COUNT(*) FROM Person";
                object obj = cmd.ExecuteScalar();
                Console.WriteLine(string.Format("Count: {0}", obj.ToString()));
                cn.Close();
            }

            public void Command_ExecuteXmlReader()
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id = myUsername; Password = myPassword;";
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Person FOR XML AUTO, XMLDATA";
                System.Xml.XmlReader xml = cmd.ExecuteXmlReader();
                cn.Close();

                /* <Schema name="Schema1" xmlns="urn:schemas-microsoft-com:xml-data" xmlns:dt="urn:schemas-microsoft-com:datatypes">
                 *  <ElementType name="Person" content="empty" model="closed">
                 *      <AttributeType name="PersonId" dt:type="i4"/>
                 *      <AttributeType name="FirstName" dt:type="string"/>
                 *      <AttributeType name="LastName" dt:type="string"/>
                 *      <AttributeType name="Address" dt:type="string"/>
                 *      <AttributeType name="City" dt:type="string"/>
                 *      <AttributeType name="State" dt:type="string"/>
                 *      <AttributeType name="ZipCode" dt:type="string"/>
                 *      <attribute type="PersonId"/>
                 *      <attribute type="FirstName"/>
                 *      <attribute type="LastName"/>
                 *      <attribute type="Address"/>
                 *      <attribute type="City"/>
                 *      <attribute type="State"/>
                 *      <attribute type="ZipCode"/>
                 *  </ElementType>
                 * </Schema>
                 * <Person xmlns="x-schema:#Schema1" PersonId="1" FirstName="John" LastName="Smith" Address="123 First Street" City="Philadelphia" State="PA" ZipCode="19111"/>
                 */
            }

            public void DataSet_Example()
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id = myUsername; Password = myPassword;";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Person", cn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Person");
                cn.Close();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Console.WriteLine(string.Format("First Name: {0} , Last Name: {1}", row["FirstName"], row["LastName"]));
                }
            }

            public void DataAdapter_Add_record_to_a_table()
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id = myUsername; Password = myPassword;";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Person", cn);
                //Create the insert command
                SqlCommand insert = new SqlCommand();
                insert.Connection = cn;
                insert.CommandType = CommandType.Text;
                insert.CommandText = "INSERT INTO Person (FirstName, LastName) VALUES (@FirstName,@LastName)";
                //Create the parameters
                insert.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 50, "FirstName"));
                insert.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 50, "LastName"));
                //Associate the insert command with the DataAdapter.
                da.InsertCommand = insert;
                //Get the data.
                DataSet ds = new DataSet();
                da.Fill(ds, "Person");
                //Add a new row.
                DataRow newRow = ds.Tables[0].NewRow();
                newRow["FirstName"] = "Jane";
                newRow["LastName"] = "Doe";
                ds.Tables[0].Rows.Add(newRow);
                //Update the database.
                da.Update(ds.Tables[0]);
                cn.Close();
            }

            public void DataAdapter_Update_and_delete_records()
            {
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id = myUsername; Password = myPassword;";
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Person", cn);
                //Create the update command
                SqlCommand update = new SqlCommand();
                update.Connection = cn;
                update.CommandType = CommandType.Text;
                update.CommandText = "UPDATE Person SET FirstName = @FirstName, LastName = @LastName WHERE PersonId = @PersonId";
                //Create the parameters
                update.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 50, "FirstName"));
                update.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 50, "LastName"));
                update.Parameters.Add(new SqlParameter("@PersonId", SqlDbType.Int, 0, "PersonId"));
                //Create the delete command
                SqlCommand delete = new SqlCommand();
                delete.Connection = cn;
                delete.CommandType = CommandType.Text;
                delete.CommandText = "DELETE FROM Person WHERE PersonId = @PersonId";
                //Create the parameters
                SqlParameter deleteParameter = new SqlParameter("@PersonId", SqlDbType.Int, 0, "PersonId");
                deleteParameter.SourceVersion = DataRowVersion.Original;
                delete.Parameters.Add(deleteParameter);
                //Associate the update and delete commands with the DataAdapter.
                da.UpdateCommand = update;
                da.DeleteCommand = delete;
                //Get the data.
                DataSet ds = new DataSet();
                da.Fill(ds, "Person");
                //Update the first row
                ds.Tables[0].Rows[0]["FirstName"] = "Jack";
                ds.Tables[0].Rows[0]["LastName"] = "Johnson";
                //Delete the second row.
                ds.Tables[0].Rows[1].Delete();
                //Updat the database.
                da.Update(ds.Tables[0]);
                cn.Close();
            }
        }
    }
}