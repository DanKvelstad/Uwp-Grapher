using Grapher.Algorithms;
using System;
using System.Linq;

namespace Grapher.Models
{
    class Grid
    {

        private int         dimensions;
        private int[]       grid;
        Tuple<int, int>[]   edges_array;

        public Point[]      Candidate;
        public int          Intersection_count;

        public Grid(GraphModel graph)
        {

//            dimensions = (int)Math.Ceiling(Math.Sqrt(graph.Nodes.Count));
//
//            grid = new int[dimensions * dimensions];
//            for (int i = 0; i < grid.Length; i++)
//            {
//                grid[i] = i;
//            }
//
//            edges_array = new Tuple<int, int>[graph.Edges.Count];
//            for(int i = 0; i < graph.Edges.Count; i++)
//            {
//
//                var from_index = graph.Nodes.FindIndex(
//                    (x) =>
//                    {
//                        return x.Label == graph.Edges[i].Source;
//                    }
//                );
//
//                var to_index = graph.Nodes.FindIndex(
//                    (x) =>
//                    {
//                        return x.Label == graph.Edges[i].Target;
//                    }
//                );
//
//                edges_array[i] = new Tuple<int, int>(
//                    from_index,
//                    to_index
//                );
//
//            }
//
//            Candidate = new Point[graph.Nodes.Count];
//
//            Process();

        }
        
        public int PermutationsCount()
        {
            int n = dimensions * dimensions;
            return n == 0 ? 1 : Enumerable.Range(1, n).Aggregate((acc, x) => acc * x);
        }

        private void Process()
        {

            Intersection_count = 0;

            for (int i = 0; i < Candidate.Length; i++)
            {
                Candidate[i].X = grid[i] % dimensions;
                Candidate[i].Y = grid[i] / dimensions;
            }

            for (int i = 0; i < edges_array.Length; i++)
            {
                for (int j = i + 1; j < edges_array.Length; j++)
                {
                    if (
                         LinearAlgebra.Intersection(
                             Candidate[edges_array[i].Item1],
                             Candidate[edges_array[i].Item2],
                             Candidate[edges_array[j].Item1],
                             Candidate[edges_array[j].Item2]
                         )
                     )
                    {
                        Intersection_count++;
                    }
                }
            }

        }

        public bool Next()
        {

            int n = grid.Length;
            int k = -1;

            for (int i = 1; i < n; i++)
            {
                if (grid[i - 1] < grid[i])
                { 
                    k = i - 1;
                }
            }

            if (k == -1)
            {
                for (int i = 0; i < n; i++)
                {
                    grid[i] = i;
                }
                Intersection_count = int.MaxValue;
                Candidate = null;
                return false;
            }
            
            int l = k + 1;

            for (int i = l; i < n; i++)
            {
                if (grid[k] < grid[i])
                {
                    l = i;
                }
            }
            
            int t   = grid[k];
            grid[k] = grid[l];
            grid[l] = t;

            Array.Reverse(grid, k + 1, grid.Length - (k + 1));

            Process();

            return true;

        }

    }
}
