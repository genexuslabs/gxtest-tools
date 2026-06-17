// Copyright 2021 GeneXus S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using GeneXus.GXtest.Tools.TestConvert.BL.v3;
using System.Text;

namespace GeneXus.GXtest.Tools.TestConvert.BL.Generation.Parameters
{
    public static class ParameterHelper
    {
        public static string GetParameterCode(Parameter parm)
        {
            var builder = new StringBuilder();
            builder.AppendParameter(parm);
            return builder.ToString();
        }

        public static void AppendParameter(this StringBuilder builder, Parameter parm)
        {
            ParameterGenerator generator = CreateGenerator(parm);
            generator.Generate(builder);
        }
        private static ParameterGenerator CreateGenerator(Parameter parm)
        {
            switch (parm.Type)
            {
                case ParmType.Literal:
                    return new LiteralParm(parm);
                case ParmType.Control:
                    return new ControlParm(parm);
                case ParmType.Boolean:
                    return new BooleanParm(parm);
                case ParmType.Variable:
                    return new VariableParm(parm);
                default:
                    return new NotImplementedParm(parm);
            }
        }

        public static int GetNumericValue(Command command, int parmIndex) => GetNumericValue(command.Parameters[parmIndex]);

        public static int GetNumericValue(Parameter parm)
        {
            string numberAsString = StringHelper.RemoveQuotes(ParameterHelper.GetParameterCode(parm));

            if (!int.TryParse(numberAsString, out int number))
                return 0;

            return number;
        }

        public static string GetStringValue(Command command, int parmIndex) => GetStringValue(command.Parameters[parmIndex]);

        public static string GetStringValue(Parameter parm)
        {
            return GetParameterCode(parm);
        }
    }
}
