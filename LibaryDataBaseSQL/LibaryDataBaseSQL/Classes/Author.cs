using System.Data.SqlClient;

public class Author {
    public void DisplayAuthorNames() {

        string connectionString = @"SERVER = 192.168.23.201,1433\SQLEXPRESS; DATABASE = Bib; USER ID = Adel; PASSWORD = Passw0rd";
       
        using (SqlConnection connection = new SqlConnection(connectionString)) {
            connection.Open();
            
            string sql = "SELECT * FROM authors";
            using (SqlCommand command = new SqlCommand(sql, connection)) {
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        string authorName = reader.GetString(0);
                        Console.WriteLine(authorName);
                    }
                }
            }
        }
    }
    public string ?FullName{ get; set; }

}
