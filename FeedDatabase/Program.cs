using System.Diagnostics;
using MySql.Data.MySqlClient;

string connstring = "server=127.0.0.1;uid=root;pwd=pass;database=Test";

using var connection = new MySqlConnection(connstring);

connection.Open();

using var tr = connection.BeginTransaction();

const int UserNumber = 400000;

var sw = Stopwatch.StartNew(); 
for (int i = 0; i < UserNumber; i++)
{
    string query = $"INSERT INTO Users (Name, Age, DateOfBirth) VALUES (\"{Faker.Name.FullName()}\", {Faker.RandomNumber.Next(80)}, '{RandomDay()}');";

    var cmd = new MySqlCommand(query, connection);

    await cmd.ExecuteNonQueryAsync();
}

tr.Commit();

sw.Stop();
Console.WriteLine($"Elapsed Time: {sw.ElapsedMilliseconds} ms");

static string RandomDay()
{
    var start = new DateTime(1995, 1, 1);
    int range = (DateTime.Today - start).Days;           
    
    return start.AddDays(new Random().Next(range)).ToString("yyyy-MM-dd");
}