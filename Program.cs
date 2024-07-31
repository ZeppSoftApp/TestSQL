using System.Data.SqlClient;

namespace TestSQL;

class Program
{
    static void Main(string[] args)
    {
        const string connectionString =
            "Server=localhost,1433\\Catalog=Test;Database=Test;User=SA;Password=Zaratustra2020;";
        // "Data Source=localhost,1433;Initial Catalog=Test;User Id=sa; Password=Zaratustra2020;"
        //  + "Integrated Security=true";


        using (SqlConnection con = new SqlConnection(connectionString))
        {
           // con.Open();
           // SqlCommand objSqlCommand = new SqlCommand("SELECT * FROM products", con);
            try
            {
                HasRows(con);   
                //var i = objSqlCommand.ExecuteNonQuery();dd111
            }
            catch (Exception ex) 
            { 
                con.Close();
            }
        }


    }
    static void HasRows(SqlConnection connection)
    {
        using (connection)
        {
            SqlCommand command = new(
              "SELECT * FROM products;",
              connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //Console.WriteLine("{0}\t{1}", reader.GetInt32(0),
                    //  reader.GetString(1));
                    //Console.WriteLine(reader.GetDecimal(0));

                    //Console.WriteLine($"Price = {reader.GetSqlValue(0)?.ToString()}, Product_name = {reader.GetSqlValue(1)?.ToString()}");

                    var price = reader.GetSqlValue(0);
                    var productName = reader.GetSqlValue(1);

                    Console.WriteLine($"Price = {price?.ToString()}, Product_name = {productName?.ToString()}");

                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
        }
    }
}
