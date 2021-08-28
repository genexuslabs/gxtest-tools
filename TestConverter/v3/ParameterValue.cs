﻿namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class ParameterValue : ParameterComponent
    {
        private readonly ParmType type;

        public ParmType Type => type;

        protected ParameterValue(ParmType type)
        {
            this.type = type;
        }
    }
}
