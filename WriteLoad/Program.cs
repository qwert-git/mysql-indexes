using System.Diagnostics;
using Dapper;
using MySql.Data.MySqlClient;

string connstring = "server=127.0.0.1;uid=root;pwd=pass;database=Test";

using var connection = new MySqlConnection(connstring);

var settings = connection.QueryFirst<dynamic>("SHOW GLOBAL VARIABLES LIKE 'innodb_flush_log_at_trx_commit';");
Console.WriteLine(settings);

var sw = Stopwatch.StartNew(); 

for (int i = 0; i < 10000; i++)
{
    var insertSql = $"INSERT INTO Users (Name, Age, DateOfBirth) VALUES (\"{Faker.Name.FullName()}\", {Faker.RandomNumber.Next(80)}, '{RandomDay()}')";
    await connection.ExecuteAsync(insertSql);
}

sw.Stop();
Console.WriteLine($"Elapsed Time: {sw.ElapsedMilliseconds} ms");

static string RandomDay()
{
    var start = new DateTime(1995, 1, 1);
    int range = (DateTime.Today - start).Days;           
    
    return start.AddDays(new Random().Next(range)).ToString("yyyy-MM-dd");
}