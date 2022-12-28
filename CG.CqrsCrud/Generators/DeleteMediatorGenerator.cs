using CG.CqrsCrud.Attributes.Commons;
using System.Reflection;

namespace CG.CqrsCrud.Generators
{
    public static class DeleteMediatorGenerator<T>
    {
        public static bool GenerateDeleteMediator(string plural, string commandNameSpace, string commandPath)
        {
            List<string> command = new List<string>();
            command.Add("using MediatR;");
            command.Add("");
            command.Add($"namespace {commandNameSpace}.{plural};");
            command.Add("");
            command.Add($"public class Delete{typeof(T).Name}Command : IRequest<bool>");
            command.Add("{");
            string primaryKey = typeof(T)
                .GetProperties()
                .Where(x => x.GetCustomAttributes().Where(a => a is PrimaryKeyAttribute).Any()).First().Name;

            string primaryKeyType = typeof(T)
                .GetProperties()
                .Where(x => x.GetCustomAttributes().Where(a => a is PrimaryKeyAttribute).Any()).First().PropertyType.Name;

            command.Add($"\tpublic {primaryKeyType} {primaryKey} {{ get; set;}}");
            command.Add("}");
            File.WriteAllLines($"{commandPath}\\Delete{typeof(T).Name}Command.cs", command);

            List<string> commandHandler = new List<string>();
            commandHandler.Add("using MediatR;");
            commandHandler.Add("");
            commandHandler.Add($"namespace {commandNameSpace}.{plural};");
            commandHandler.Add("");
            commandHandler.Add($"public class Delete{typeof(T).Name}CommandHandler : IRequestHandler<Delete{typeof(T).Name}Command, bool>");
            commandHandler.Add("{");
            commandHandler.Add($"\tprivate readonly I{typeof(T).Name}Repository _{typeof(T).Name.ToLower()}Repository;");
            commandHandler.Add($"\tpublic Delete{typeof(T).Name}CommandHandler(I{typeof(T).Name}Repository {typeof(T).Name.ToLower()}Repository)");
            commandHandler.Add("\t{");
            commandHandler.Add($"\t\t_{typeof(T).Name.ToLower()}Repository = {typeof(T).Name.ToLower()}Repository;");
            commandHandler.Add("\t}");
            commandHandler.Add("\t");
            commandHandler.Add($"\tpublic async Task<bool> Handle(Delete{typeof(T).Name}Command request, CancellationToken cancellationToken)");
            commandHandler.Add("\t{");
            commandHandler.Add($"\t\tvar {typeof(T).Name.ToLower()} = _{typeof(T).Name.ToLower()}Repository.Get{typeof(T).Name}(request.{primaryKey});");
            commandHandler.Add("\t\t");
            commandHandler.Add($"\t\tif({typeof(T).Name.ToLower()} == null)");
            commandHandler.Add("\t\t{");
            commandHandler.Add("\t\t\treturn false;");
            commandHandler.Add("\t\t}");
            commandHandler.Add($"\t\treturn await _{typeof(T).Name.ToLower()}Repository.Delete{typeof(T).Name}({typeof(T).Name.ToLower()});");
            commandHandler.Add("\t}");
            commandHandler.Add("}");
            File.WriteAllLines($"{commandPath}\\Delete{typeof(T).Name}Command.Handler.cs", commandHandler);
            File.WriteAllLines($"{commandPath}\\Delete{typeof(T).Name}Command.Validator.cs", ValidationGenerator<T>
                .Generate(plural, commandNameSpace, "Delete", typeof(T).GetProperties().Where(x => x.SetMethod != null && x.GetCustomAttributes().Where(a => a is PrimaryKeyAttribute).Any()).ToList()));
            return true;
        }
    }
}
