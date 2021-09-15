using System;
using System.Collections.Generic;

namespace GeneXus.GXtest.Tools.TestConverter.Generation
{
    public enum Verbosity
    {
        Quiet = 0, // The most minimal output
        Minimal = 1, // Relatively little output
        Normal = 2, // Standard output.This should be the default if verbosity level is not set
        Detailed = 3, // Relatively verbose, but not exhaustive
        Diagnostic = 4, // The most verbose and informative verbosity
    }

    internal class GenerationOptions
    {
        public class Variable
        {
            public Variable(string name = "", string value = "")
            {
                Name = name;
                Value = value;
            }

            public string Name { get; }
            public string Value { get; }
        }

        private static readonly GenerationOptions generalOptions = new();

        public static GenerationOptions General => generalOptions;

        public Verbosity Verbosity { get; set; } = Verbosity.Normal;

        public bool BlankLineAfterElement { get; set; } = true;
        public bool GenerateEndMethod { get; set; } = true;

        private readonly Dictionary<string, Variable> variables = new();

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
                if (string.IsNullOrEmpty(substitution))
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
            if (parts.Length != 2)
                return false;

            string name = parts[0].Trim();
            string value = parts[1].Trim();
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
                return false;

            variable = new Variable(name, value);
            return true;
        }
    }
}
