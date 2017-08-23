using System;
using System.Collections.Generic;
using Windows.Foundation;

namespace Grapher
{

    public class Graph
    {

        public List<String>                     nodes;
        public List<Tuple<int, int, String>>    edges;

        public Graph()
        {
            nodes = new List<String>();
            edges = new List<Tuple<int, int, String>>();
        }

        public void EmplaceNode(String text)
        {
            nodes.Add(text);
        }

        public void EmplaceEdge(String from, String to, String label)
        {

            var from_index = nodes.FindIndex(
                (x) =>
                {
                    return x == from;
                }
            );

            var to_index = nodes.FindIndex(
                (x) =>
                {
                    return x == to;
                }
            );

            edges.Add(new Tuple<int, int, String>(from_index, to_index, label));

        }

        public List<Point[]> candidates;

    }

}
