// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using TestEfContains;

using var db = new MyDbContext();
// Create the database if it doesn't exist
db.Database.EnsureCreated();
var isTestDataExist = await db.Tests.AnyAsync(x => x.StockId == "1234");
if (!isTestDataExist)
{
    var testData = new Test { StockId = "1234", StockName = "這是一串中文" };
    db.Tests.Add(testData);
    await db.SaveChangesAsync();
}


var firstData = await db.Tests.FirstAsync();
PrintData(firstData, "test data:");

var queryName = "一串";
System.Diagnostics.Debug.WriteLine("------query 1---------");
var query1 = await db.Tests.Where(x => x.StockId == queryName || x.StockName.Contains(queryName)).FirstOrDefaultAsync();
if (query1 is null)
    Console.WriteLine("query1 is null");
else
    PrintData(query1, "query1:");

System.Diagnostics.Debug.WriteLine("------query 2---------");
var query2 = await db.Tests.Where(x => x.StockName.Contains(queryName) || x.StockId == queryName).FirstOrDefaultAsync();
if (query2 is null)
    Console.WriteLine("query2 is null");
else
    PrintData(query2, "query2:");

System.Diagnostics.Debug.WriteLine("------query 3---------");
var query3 = await db.Tests.Where(x => x.StockId == queryName || EF.Functions.Like(x.StockName, $"%{queryName}%")).FirstOrDefaultAsync();
if (query3 is null)
    Console.WriteLine("query3 is null");
else
    PrintData(query3, "query3:");

Console.ReadLine();

void PrintData(Test data, string prefix)
{
    Console.WriteLine($"{prefix}id:{data.StockId}, name: {data.StockName}");
}