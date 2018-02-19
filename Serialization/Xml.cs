using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Grapher.Models;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Grapher.Serialization
{

    public static class Serializor
    {

        public static void SerializeAsXml(GraphModel ToSerialize, IOutputStream Output)
        {

            var doc = new XDocument();
            doc.Add(new XElement("graph"));
            var graph = doc.Element("graph");

            graph.Add(new XElement("nodes"));
            var nodes = graph.Element("nodes");
            foreach (var node in ToSerialize.NodeModels)
            {
                nodes.Add(
                    new XElement(
                        "node", 
                        new XAttribute("label", node.Label)
                    )
                );
            }

            graph.Add(new XElement("edges"));
            var edges = graph.Element("edges");
            foreach (var edge in ToSerialize.EdgeModels)
            {
                edges.Add(
                    new XElement(
                        "edge", 
                        new XAttribute("label",  edge.Label),
                        new XAttribute("source", edge.Source),
                        new XAttribute("target", edge.Target)
                    )
                );
            }

            //graph.Add(
            //    new XElement("candidate"),
            //    new XAttribute("index", 0)
            //);
            //var candidates = graph.Element("candidate");
            //var i = 0;
            //foreach (var element in ToSerialize.candidates.ElementAt(0))
            //{
            //    candidates.Add(
            //        new XElement(
            //            "coordinate",
            //            new XAttribute("node", ToSerialize.nodes.ElementAt(i++)),
            //            new XAttribute("x", element.X),
            //            new XAttribute("y", element.Y)
            //        )
            //    );
            //}
            
            doc.Save(Output.AsStreamForWrite());

        }

        private static GraphModel DeserializeAsXml(Stream Input)
        {

            try
            {

                var Result = new GraphModel();

                var doc = XDocument.Load(Input);
                var graph = doc.Element("graph");
                Result.Label = graph.Attribute("label").Value;
                foreach (var Element in graph.Element("nodes").Elements())
                {
                    Result.NodeModels.Add(
                        new NodeModel()
                        {
                            Label = Element.Attribute("label").Value
                        }
                    );
                }

                foreach (var edge in graph.Element("edges").Elements())
                {

                    var label        = edge.Attribute("label").Value;
                    var sourceString = edge.Attribute("source").Value;
                    var targetString = edge.Attribute("target").Value;

                    NodeGeometryModel sourceGeometry = null;
                    NodeGeometryModel targetGeometry = null;

                    foreach (var nodeModel in Result.NodeModels)
                    {
                        if (nodeModel.Label == sourceString)
                        {
                            sourceGeometry = nodeModel.Geometry;
                        }
                        if (nodeModel.Label == targetString)
                        {
                            targetGeometry = nodeModel.Geometry;
                        }
                    }

                    if (null != label && null != sourceGeometry && null != targetGeometry)
                    {
                        Result.EdgeModels.Add(
                            new EdgeModel()
                            {
                                Label          = label,
                                SourceGeometry = sourceGeometry,
                                TargetGeometry = targetGeometry
                            }
                        );
                    }

                }

                return Result;

            }
            catch (XmlException)
            {
                return null;
            }
            catch(ArgumentNullException)
            {
                return null;
            }
            
        }

        public static async Task<GraphModel> Deserialize(IStorageFile File)
        {

            if(".xml"==File.FileType)
            {
                return DeserializeAsXml(await File.OpenStreamForReadAsync());
            }
            else
            {
                return null;
            }

        }

    }

}
