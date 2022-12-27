using CG.CqrsCrud.Attributes.Commons;
using CG.CqrsCrud.Attributes.MediatorAttributes.Commands;
using CG.CqrsCrud.Attributes.MediatorAttributes.Queries;
using CG.CqrsCrud.Generators;
using System.IO;
using System.Reflection;

namespace CG.CqrsCrud
{
    public class CodeGenerator<T>
    {
        string plural;
        string commandPath;
        string queryPath;
        string commandNameSpace;
        string queryPathNameSpace;

        public CodeGenerator(string commandPath, string commandNameSpace, string queryPath, string queryNameSpace)
        {
            this.commandNameSpace = commandNameSpace;
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
                AddMediatorGenerator<T>.GenerateAddMediator(plural, commandNameSpace, commandPath);
            }

            if (attrs.Where(x => x is UpdateMediator).Any())
            {
                UpdateMediatorGenerator<T>.GenerateUpdateMediator(plural, commandNameSpace, commandPath);
            }

            if (attrs.Where(x => x is DeleteMediator).Any())
            {
                DeleteMediatorGenerator<T>.GenerateDeleteMediator(plural, commandNameSpace, commandPath);
            }

            if (attrs.Where(x => x is GetMediator).Any())
            {
                GetMediatorGenerator<T>.GenerateGetMediator(plural, queryPathNameSpace, queryPath);
            }

            if (attrs.Where(x => x is GetListMediator).Any())
            {
                GetListMediatorGenerator<T>.GenerateGetListMediator(plural, queryPathNameSpace, queryPath);
            }

            return true;
        }
    }
}
