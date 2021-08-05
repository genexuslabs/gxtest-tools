using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace GeneXus.GXtest.Tools.TestConverter.v3
{
    [XmlRoot("DSElements", Namespace = "http://tempuri.org/DSElements.xsd")]
    public class TestCase
    {
        [XmlElement("TCToXML")]
        public GeneralData GeneralData { get; set; }

        [XmlElement("Paginas")]
        public List<Node> Nodes { get; set; }

        [XmlElement("Aristas")]
        public List<Edge> Edges { get; set; }

        [XmlElement("Comandos Elemento")]
        public List<Command> Commands { get; set; }

        [XmlElement("ParameterAbstract")]
        public List<Parameter> Parameters { get; set; }

        [XmlElement("Param Literal")]
        public List<ParameterLiteralValue> LiteralValues { get; set; }

        [XmlElement("ParametersBoolean")]
        public List<ParameterBooleanValue> BooleanValues { get; set; }

        [XmlElement("ParametersGXObject")]
        public List<ParameterControlValue> ControlValues { get; set; }

        [XmlElement("ControlExportTC")]
        public List<ParameterControlData> ControlData { get; set; }

        public static TestCase DeserializeFromXML(string xmlFilePath)
        {
            // verify file exists
            if (!File.Exists(xmlFilePath))
            {
                Console.Error.WriteLine($"Source XML file does not exist '{xmlFilePath}'");
                return null;
            }

            TestCase testCase = null;
            using (var fileStream = File.Open(xmlFilePath, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TestCase));
                testCase = (TestCase)serializer.Deserialize(fileStream);
            }

            testCase.AfterSerialize();
            return testCase;
        }

        private void AfterSerialize()
        {
            IndexElements();
            ConnectNodes();
            LoadCommands();
            LoadCommandParameters();
            LoadCommandParameterValues();
        }


        private Dictionary<string, Element> elementsMap = new Dictionary<string, Element>();
        private void IndexElements()
        {
            Nodes.ForEach(node => IndexElement(node.Id, node));
            Edges.ForEach(edge => IndexElement(edge.Id, edge));
        }

        private void IndexElement(string id, Element element)
        {
            try
            {
                elementsMap.Add(id, element);
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception($"Element id cannot be null", ex);
            }
            catch (ArgumentException ex)
            {
                throw new Exception($"Duplicate element id '{id}'", ex);
            }
        }

        public Element GetElement(string id)
        {
            return elementsMap[id];
        }

        private void ConnectNodes()
        {
            Edges.ForEach(edge =>
            {
                // Make the edge know its source and target nodes
                edge.SourceNode = GetElement(edge.SourceNodeId) as Node;
                edge.TargetNode = GetElement(edge.TargetNodeId) as Node;

                // Make the source node know this outbound edge
                edge.SourceNode.AddEdge(edge);
            });
        }

        private Dictionary<string, ParameterControlData> controlDataMap = new Dictionary<string, ParameterControlData>();
        private void IndexControlData()
        {
            ControlData.ForEach(data => controlDataMap.Add(data.ControlId, data));
        }
        public ParameterControlData GetControlData(string controlId)
        {
            return controlDataMap[controlId];
        }

        private void LoadCommands()
        {
            Commands.ForEach(command => GetElement(command.ParentId).AddCommand(command));
        }

        private void LoadCommandParameters()
        {
            Parameters.ForEach(parm => GetElement(parm.ParentId).AddCommandParameter(parm));
        }

        private void AddParameterValue(ParameterValue val)
        {
            GetElement(val.ParentId).AddCommandParameterValue(val, this);
        }

        private void LoadCommandParameterValues()
        {
            LiteralValues.ForEach(val => AddParameterValue(val));
            BooleanValues.ForEach(val => AddParameterValue(val));

            IndexControlData();
            ControlValues.ForEach(val => {
                val.AddControlData(controlDataMap);
                AddParameterValue(val);
            });
        }

    }
}
