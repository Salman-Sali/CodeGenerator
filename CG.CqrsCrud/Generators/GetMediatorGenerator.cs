using CG.CqrsCrud.Attributes.Commons;
using System.Reflection;

namespace CG.CqrsCrud.Generators
{
    public static class GetMediatorGenerator<T>
    {
        public static bool GenerateGetMediator(string plural, string commandNameSpace, string queryPath)
        {
            List<string> command = new List<string>();
            command.Add("using MediatR;");
            command.Add("");
            command.Add($"namespace {commandNameSpace}.{plural};");
            command.Add("");
            command.Add($"public class Get{typeof(T).Name}Query : IRequest<Get{typeof(T).Name}QueryResponse>");
            command.Add("{");
            string primaryKey = typeof(T)
                .GetProperties()
                .Where(x => x.GetCustomAttributes().Where(a => a is PrimaryKeyAttribute).Any()).First().Name;

            string primaryKeyType = typeof(T)
                .GetProperties()
                .Where(x => x.GetCustomAttributes().Where(a => a is PrimaryKeyAttribute).Any()).First().PropertyType.Name;

            command.Add($"\tpublic {primaryKeyType} {primaryKey} {{ get; set;}}");
            command.Add("}");
            File.WriteAllLines($"{queryPath}\\Get{typeof(T).Name}Query.cs", command);

            List<string> commandHandler = new List<string>();
            commandHandler.Add("using MediatR;");
            commandHandler.Add("");
            commandHandler.Add($"namespace {commandNameSpace}.{plural};");
            commandHandler.Add("");
            commandHandler.Add($"public class Get{typeof(T).Name}QueryHandler : IRequestHandler<Get{typeof(T).Name}Query, Get{typeof(T).Name}QueryResponse>");
            commandHandler.Add("{");
            commandHandler.Add($"\tprivate readonly I{typeof(T).Name}Repository _{typeof(T).Name.ToLower()}Repository;");
            commandHandler.Add($"\tpublic Get{typeof(T).Name}QueryHandler(I{typeof(T).Name}Repository {typeof(T).Name.ToLower()}Repository)");
            commandHandler.Add("\t{");
            commandHandler.Add($"\t\t_{typeof(T).Name.ToLower()}Repository = {typeof(T).Name.ToLower()}Repository;");
            commandHandler.Add("\t}");
            commandHandler.Add("\t");
            commandHandler.Add($"\tpublic async Task<Get{typeof(T).Name}QueryResponse> Handle(Get{typeof(T).Name}Query request, CancellationToken cancellationToken)");
            commandHandler.Add("\t{");
            commandHandler.Add($"\t\treturn await _{typeof(T).Name.ToLower()}Repository.Get{typeof(T).Name}(request.{primaryKey});");
            commandHandler.Add("\t}");
            commandHandler.Add("}");
            File.WriteAllLines($"{queryPath}\\Get{typeof(T).Name}Query.Handler.cs", commandHandler);

            List<string> response = new List<string>();
            response.Add($"namespace {commandNameSpace}.{plural};");
            response.Add("");
            response.Add($"public class Get{typeof(T).Name}QueryResponse");
            response.Add("{");
            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                if (info.SetMethod == null)
                {
                    continue;
                }
                response.Add($"\tpublic {info.PropertyType.Name} {info.Name} {{ get; set;}}");
            }
            response.Add("}");

            File.WriteAllLines($"{queryPath}\\Get{typeof(T).Name}Query.Response.cs", response);

            return true;
        }
    }
}
