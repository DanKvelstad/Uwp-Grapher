using Grapher.Models;
using System;
using System.Collections.Generic;

namespace Grapher
{

    public class GraphModel
    {

        public List<NodeModel> nodes;
        public List<EdgeModel> edges;

        public GraphModel()
        {
            nodes = new List<NodeModel>();
            edges = new List<EdgeModel>();
        }

        public void EmplaceNode(String text)
        {
            var Node = new NodeModel()
            {
                Label = text
            };
            nodes.Add(Node);
        }

        public void EmplaceEdge(String from, String to, String label)
        {
            var Edge = new EdgeModel()
            {
                Label  = label,
                Source = from,
                Target = to
            };
            edges.Add(Edge);
        }

    }

}
