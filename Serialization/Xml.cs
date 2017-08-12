using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Windows.Storage;

namespace Grapher.Serialization
{

    public static class Serializor
    {

        public static void SerializeAsXml(Graph ToSerialize, Stream Output)
        {

            var doc = new XDocument();
            doc.Add(new XElement("graph"));
            var graph = doc.Element("graph");

            graph.Add(new XElement("nodes"));
            var nodes = graph.Element("nodes");
            foreach (var node in ToSerialize.nodes)
            {
                nodes.Add(
                    new XElement(
                        "node", 
                        new XAttribute("label", node)
                    )
                );
            }

            graph.Add(new XElement("edges"));
            var edges = graph.Element("edges");
            foreach (var edge in ToSerialize.edges)
            {
                edges.Add(
                    new XElement(
                        "edge", 
                        new XAttribute("label",  edge.Item3),
                        new XAttribute("source", ToSerialize.nodes[edge.Item1]),
                        new XAttribute("target", ToSerialize.nodes[edge.Item2])
                    )
                );
            }

            graph.Add(
                new XElement("candidate"),
                new XAttribute("index", 0)
            );
            var candidates = graph.Element("candidate");
            var i = 0;
            foreach (var element in ToSerialize.candidates.ElementAt(0))
            {
                candidates.Add(
                    new XElement(
                        "coordinate",
                        new XAttribute("node", ToSerialize.nodes.ElementAt(i++)),
                        new XAttribute("x", element.X),
                        new XAttribute("y", element.Y)
                    )
                );
            }
            
            doc.Save(Output);

        }

        public static Graph Deserialize(Stream Input)
        {

            var Result = new Graph();

            var doc = XDocument.Load(Input);
            var graph = doc.Element("graph");
            foreach(var Element in graph.Element("nodes").Elements())
            {
                Result.EmplaceNode(Element.Attribute("label").Value);
            }

            foreach (var edge in graph.Element("edges").Elements())
            {
                Result.EmplaceEdge(
                    edge.Attribute("source").Value,
                    edge.Attribute("target").Value,
                    edge.Attribute("label").Value
                );
            }

            return Result;

        }

    }

}
