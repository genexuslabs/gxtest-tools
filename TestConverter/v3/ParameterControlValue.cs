﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    public class ParameterControlValue : ParameterValue
    {
        [XmlElement("GXControlGUID")]
        public string ControlId { get; set; }

        public override string ToString()
        {
            return $"ParameterControlValue[{ControlId}]";
        }

        private ParameterControlData controlData = null;

        public void AddControlData(IReadOnlyDictionary<string, ParameterControlData> dataStore)
        {
            if (controlData != null)
                throw new Exception($"Trying to add control data for value '{this}' over existing data '{controlData}'");

            ParameterControlData data = dataStore[ControlId];
            if (data == null)
                throw new Exception($"Trying to add null control data for value '{this}'");

            controlData = data;
        }
    }
}