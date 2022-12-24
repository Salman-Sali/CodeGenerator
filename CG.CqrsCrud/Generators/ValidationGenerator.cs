using System.Reflection;

namespace CG.CqrsCrud.Generators
{
    public static class ValidationGenerator<T>
    {
        public static List<string> Generate(string nameSpace, string operation)
        {
            List<string> validations = new List<string>();
            validations.Add("using MediatR;");
            validations.Add("using FluentValidation;");
            validations.Add("");
            validations.Add($"namespace {nameSpace}.{typeof(T).Name};");
            validations.Add("");
            validations.Add($"public class {operation}{typeof(T).Name}Command : AbstractValidator<{operation}{typeof(T).Name}Command>");
            validations.Add("{");
            validations.Add("\tpublic {operation}{typeof(T).Name}Command()");
            validations.Add("\t{");
            foreach (var prop in typeof(T).GetProperties())
            {
                foreach (var item in GenerateRule(prop))
                {
                    validations.Add(item);
                }                
                validations.Add("");

            }
            validations.Add("\t}");
            validations.Add("}");

            return validations;
        }

        private static List<string> GenerateRule(PropertyInfo prop)
        {
            if(prop.GetType() == typeof(string))
            {
                return StringRule(prop);
            }
            else if(prop.GetType() == typeof(int))
            {
                return NumericRule(prop);
            }
            return new List<string>();
        }

        private static List<string> StringRule(PropertyInfo prop)
        {
            List<string> validations = new List<string>();
            return validations;
        }

        private static List<string> NumericRule(PropertyInfo prop)
        {
            List<string> validations = new List<string>();
            return validations;
        }
    }
}
