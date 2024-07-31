using System.Data.SqlClient;

namespace TestSQL;

class Program
{
    static void Main(string[] args)
    {
        const string connectionString =
            "Server=localhost,1433\\Catalog=Test;Database=Test;User=SA;Password=Zaratustra2020;";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            try
            {
                HasRows(con);   
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
