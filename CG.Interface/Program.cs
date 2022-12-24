using CG.CqrsCrud;
using CG.Domain.Entities;

Console.WriteLine("Starting");

Console.WriteLine("Enter the command path: ");
var commandPath = Console.ReadLine();

Console.WriteLine("Enter the command namespace: ");
var commandNameSpace = Console.ReadLine();

Console.WriteLine("Enter the query path: ");
var queryPath = Console.ReadLine();

Console.WriteLine("Enter the query namespace: ");
var queryNameSpace = Console.ReadLine();

var generator = new CodeGenerator<User>(commandPath, commandNameSpace, queryPath, queryNameSpace);
generator.Generate();


var generator2 = new CodeGenerator<Book>(commandPath, commandNameSpace, queryPath, queryNameSpace);
generator2.Generate();