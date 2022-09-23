using System.Diagnostics;
using Dapper;
using MySql.Data.MySqlClient;

string connstring = "server=127.0.0.1;uid=root;pwd=pass;database=Test";


const int UserMillionsNumber = 1 * 1000 * 1000 * 10;


using var connection = new MySqlConnection(connstring);

var settings = connection.QueryFirst<dynamic>("SHOW GLOBAL VARIABLES LIKE 'innodb_flush_log_at_trx_commit';");
Console.WriteLine(settings);

var sw = Stopwatch.StartNew(); 

var sqls = GetSqlsInBatches(UserMillionsNumber);
foreach (var sql in sqls)
{
    await connection.ExecuteAsync(sql);
}

sw.Stop();
Console.WriteLine($"Elapsed Time: {sw.ElapsedMilliseconds} ms");


IList<string> GetSqlsInBatches(int usersCount)
{
    var insertSql = "INSERT INTO Users (Name, Age, DateOfBirth) VALUES ";
    var batchSize = 1000;

    var sqlsToExecute = new List<string>();
    var numberOfBatches = (int)Math.Ceiling((double)usersCount / batchSize);

    for (int i = 0; i < numberOfBatches; i++)
    {
        var valuesToInsert = new List<string>();
        for(int j = 0; j < batchSize; j++)
         valuesToInsert.Add($"(\"{Faker.Name.FullName()}\", {Faker.RandomNumber.Next(80)}, '{RandomDay()}')");
        
        sqlsToExecute.Add(insertSql + string.Join(',', valuesToInsert));
    }

    return sqlsToExecute;
}

static string RandomDay()
{
    var start = new DateTime(1995, 1, 1);
    int range = (DateTime.Today - start).Days;           
    
    return start.AddDays(new Random().Next(range)).ToString("yyyy-MM-dd");
}