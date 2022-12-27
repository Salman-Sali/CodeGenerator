using CG.CqrsCrud;
using CG.Domain.Entities;

Console.WriteLine("Starting");
Console.WriteLine("MENU\n----\n1.Mediator request files for entity.");
var selection = int.Parse(Console.ReadLine());
switch (selection)
{
    case 1:
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

        break;

    default: 
        Console.WriteLine("Invalid choice.");
        break;
}



