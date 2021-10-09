using GeneXus.GXtest.Tools.TestConvert.BL.Generation.Helpers;
using System;
using System.Collections.Generic;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation
{
    public enum Verbosity
    {
        Quiet = 0, // The most minimal output
        Minimal = 1, // Relatively little output
        Normal = 2, // Standard output.This should be the default if verbosity level is not set
        Detailed = 3, // Relatively verbose, but not exhaustive
        Diagnostic = 4, // The most verbose and informative verbosity
    }

    public class GenerationOptions
    {
        public class Variable
        {
            public Variable(string name = "", string value = "")
            {
                Name = NormalizeName(name);
                Value = value;
            }

            public string Name { get; }
            public string Value { get; }

            private static string NormalizeName(string rawName)
            {
                string name = rawName.Trim();

                if (name.StartsWith("&"))
                    name = name.Substring(1);

                return name;
            }

            public override bool Equals(object anotherObject)
            {
                //Check for null and compare run-time types.
                if ((anotherObject == null) || !this.GetType().Equals(anotherObject.GetType()))
                    return false;

                Variable anotherVar = (Variable)anotherObject;
                return (Name == anotherVar.Name) && (Value == anotherVar.Value);
            }

            public override int GetHashCode()
            {
                return HashCode
                    .Of(Name)
                    .And(Value);
            }

            public override string ToString()
            {
                return $"({Name ?? "[null]"}, {Value ?? "[null]"})";
            }
        }

        private static readonly GenerationOptions generalOptions = new GenerationOptions();

        public static GenerationOptions General => generalOptions;

        public Verbosity Verbosity { get; set; } = Verbosity.Normal;

        public bool BlankLineAfterElement { get; set; } = true;
        public bool GenerateEndMethod { get; set; } = true;

        private readonly Dictionary<string, Variable> variables = new Dictionary<string, Variable>(StringComparer.OrdinalIgnoreCase);

        public IDictionary<string, Variable> Variables => variables;

        public bool TryGetVariable(string name, out Variable variable)
        {
            return Variables.TryGetValue(name, out variable);
        }

        public void SetVariables(string substitutionList, bool clearPrevious = true)
        {
            char separator = ';';
            SetVariables(substitutionList.Split(separator), clearPrevious);
        }

        public void SetVariables(IEnumerable<string> substitutions, bool clearPrevious = true)
        {
            if (clearPrevious)
                variables.Clear();

            foreach (string substitution in substitutions)
            {
                if (string.IsNullOrWhiteSpace(substitution))
                    continue;

                if (!TryParseSubstitution(substitution, out Variable variable))
                    throw new Exception($"Wrong variable substitution. Expected format: <name>=<value>. Found {substitution}");

                variables[variable.Name] = variable;
            }
        }

        private static bool TryParseSubstitution(string substitution, out Variable variable)
        {
            variable = new Variable();

            char operatorChar = '=';
            string[] parts = substitution.Split(operatorChar);

            string name = parts[0].Trim();
            if (name.Length == 0)
                return false;

            // more than 1 operatorChar?
            if (parts.Length > 2)
                return false;

            string value = (parts.Length == 2) ? parts[1].Trim() : string.Empty;

            variable = new Variable(name, value);
            return true;
        }
    }
}
