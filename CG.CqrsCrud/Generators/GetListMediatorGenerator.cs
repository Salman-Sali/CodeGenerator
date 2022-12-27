using CG.CqrsCrud.Attributes.Commons;
using System.Reflection;

namespace CG.CqrsCrud.Generators
{
    public static class GetListMediatorGenerator<T>
    {
        public static bool GenerateGetListMediator(string plural, string commandNameSpace, string commandPath)
        {
            List<string> command = new List<string>();
            command.Add("using MediatR;");
            command.Add("");
            command.Add($"namespace {commandNameSpace}.{plural};");
            command.Add("");
            command.Add($"public class GetList{typeof(T).Name}Query : IRequest<List<Get{typeof(T).Name}Response>>");
            command.Add("{");
            string primaryKey = typeof(T)
                .GetProperties()
                .Where(x => x.GetCustomAttributes().Where(a => a is PrimaryKeyAttribute).Any()).First().Name;

            string primaryKeyType = typeof(T)
                .GetProperties()
                .Where(x => x.GetCustomAttributes().Where(a => a is PrimaryKeyAttribute).Any()).First().PropertyType.Name;

            command.Add($"\tpublic {primaryKeyType} {primaryKey} {{ get; set;}}");
            command.Add("}");
            File.WriteAllLines($"{commandPath}\\GetList{typeof(T).Name}Query.cs", command);

            List<string> commandHandler = new List<string>();
            commandHandler.Add("using MediatR;");
            commandHandler.Add("");
            commandHandler.Add($"namespace {commandNameSpace}.{plural};");
            commandHandler.Add("");
            commandHandler.Add($"public class GetList{typeof(T).Name}QueryHandler : IRequestHandler<GetList{typeof(T).Name}Query, List<Get{typeof(T).Name}QueryResponse>>");
            commandHandler.Add("{");
            commandHandler.Add($"\tprivate readonly I{typeof(T).Name}Repository _{typeof(T).Name.ToLower()}Repository;");
            commandHandler.Add($"\tpublic GetList{typeof(T).Name}QueryHandler(I{typeof(T).Name}Repository {typeof(T).Name.ToLower()}Repository)");
            commandHandler.Add("\t{");
            commandHandler.Add($"\t\t_{typeof(T).Name.ToLower()}Repository = {typeof(T).Name.ToLower()}Repository;");
            commandHandler.Add("\t}");
            commandHandler.Add("\t");
            commandHandler.Add($"\tpublic async Task<List<Get{typeof(T).Name}QueryResponse>> Handle(GetList{typeof(T).Name}Query request, CancellationToken cancellationToken)");
            commandHandler.Add("\t{");
            commandHandler.Add($"\t\treturn await _{typeof(T).Name.ToLower()}Repository.GetAll{typeof(T).Name}(request.{primaryKey});");
            commandHandler.Add("\t}");
            commandHandler.Add("}");
            File.WriteAllLines($"{commandPath}\\GetList{typeof(T).Name}Query.Handler.cs", commandHandler);
            return true;
        }
    }
}
