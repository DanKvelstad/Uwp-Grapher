using Grapher.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Grapher.Services
{
    public class SimpleGridPlacementService
    {

        public IEnumerable<NodeModel> Nodes
        {
            private get;
            set;
        }

        public void Nodes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            uint i = 0;
            uint dimensions = (uint)Math.Ceiling(Math.Sqrt(Nodes.Count()));
            foreach (var node in Nodes)
            {
                node.Grid = new Point()
                {
                    X = i % dimensions,
                    Y = i / dimensions
                };
                i++;
            }
        }


    }
}
