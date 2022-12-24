using CG.CqrsCrud.Attributes.Commons;
using CG.CqrsCrud.Attributes.MediatorAttributes.Commands;
using System.IO;
using System.Reflection;

namespace CG.CqrsCrud
{
    public class CodeGenerator<T>
    {
        string plural;
        string commandPath;
        string queryPath;
        string commandPathNameSpace;
        string queryPathNameSpace;

        public CodeGenerator(string commandPath, string commandNameSpace, string queryPath, string queryNameSpace)
        {
            this.commandPathNameSpace = commandNameSpace;
            this.queryPathNameSpace = queryNameSpace;

            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(typeof(T));
            if (attrs.Where(x => x is PluralAttribute).Any())
            {
                PluralAttribute p = (PluralAttribute)attrs.Where(x => x is PluralAttribute).First();
                plural = p.GetPlural();
            }
            else
            {
                plural = typeof(T).Name + "s";
            }

            this.commandPath = commandPath + "\\" + plural;
            if (!Directory.Exists(this.commandPath))
            {
                Directory.CreateDirectory(this.commandPath);
            }

            this.queryPath = queryPath + "\\" + plural;
            if (!Directory.Exists(this.queryPath))
            {
                Directory.CreateDirectory(this.queryPath);
            }
        }

        public bool Generate()
        {
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(typeof(T));
            if (attrs.Where(x=> x is AddMediator).Any())
            {
                GenerateAddMediatorAsync();
            }

            if (attrs.Where(x => x is UpdateMediator).Any())
            {
                GenerateUpdateMediator();
            }

            if (attrs.Where(x => x is DeleteMediator).Any())
            {
                GenerateDeleteMediator();
            }

            return true;
        }

        private bool GenerateAddMediatorAsync()
        {
            List<string> command = new List<string>();
            command.Add("using MediatR;");
            command.Add("");
            command.Add($"namespace {commandPathNameSpace}.{plural};");
            command.Add("");
            command.Add($"public class Add{typeof(T).Name}Command : IRequest<bool>");
            command.Add("{");
            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                if(info.SetMethod == null)
                {
                    continue;
                }
                command.Add($"\tpublic {info.PropertyType.Name} {info.Name} {{ get; set;}}");
            }
            command.Add("}");
            File.WriteAllLines($"{commandPath}\\Add{typeof(T).Name}Command.cs", command);

            List<string> commandHandler = new List<string>();
            commandHandler.Add("using MediatR;");
            commandHandler.Add("");
            commandHandler.Add($"namespace {commandPathNameSpace}.{plural};");
            commandHandler.Add("");
            commandHandler.Add($"public class Add{typeof(T).Name}CommandHandler : IRequestHandler<Add{typeof(T).Name}Command, bool>");
            commandHandler.Add("{");
            commandHandler.Add($"\tprivate readonly I{typeof(T).Name}Repository _{typeof(T).Name.ToLower()}Repository");
            commandHandler.Add($"\tpublic Add{typeof(T).Name}CommandHandler(I{typeof(T).Name}Repository {typeof(T).Name.ToLower()}Repository)");
            commandHandler.Add("\t{");
            commandHandler.Add($"\t\t_{typeof(T).Name.ToLower()}Repository = {typeof(T).Name.ToLower()}Repository;");
            commandHandler.Add("\t}");
            commandHandler.Add("\t");
            commandHandler.Add($"\tpublic async Task<bool> Handle(Add{typeof(T).Name}Command request, CancellationToken cancellationToken)");
            commandHandler.Add("\t{");
            commandHandler.Add($"\t\tvar {typeof(T).Name.ToLower()} = new {typeof(T).Name}");
            commandHandler.Add("\t\t{");
            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                if (info.SetMethod == null)
                {
                    continue;
                }
                commandHandler.Add($"\t\t\t{info.Name} = request.{info.Name},");
            }            
            commandHandler.Add("\t\t}");
            commandHandler.Add("\t\t");
            commandHandler.Add($"\t\treturn await _{typeof(T).Name.ToLower()}Repository.Add{typeof(T).Name}({typeof(T).Name.ToLower()});");
            commandHandler.Add("\t}");
            commandHandler.Add("}");
            File.WriteAllLines($"{commandPath}\\Add{typeof(T).Name}Command.Handler.cs", commandHandler);

            return true;
        }

        private bool GenerateUpdateMediator()
        {

            return true;
        }

        private bool GenerateDeleteMediator()
        {

            return true;
        }
    }
}
