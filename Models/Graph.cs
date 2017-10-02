using System;
using System.Collections.Generic;
using Windows.Foundation;

namespace Grapher
{

    public class Graph
    {

        public List<String>                     nodes;
        public List<Tuple<String, String, String>>    edges;

        public Graph()
        {
            nodes = new List<String>();
            edges = new List<Tuple<String, String, String>>();
        }

        public void EmplaceNode(String text)
        {
            nodes.Add(text);
        }

        public void EmplaceEdge(String from, String to, String label)
        {

            edges.Add(
                new Tuple<String, String, String>(
                    from, 
                    to, 
                    label
                )
            );

        }
        
    }

}
