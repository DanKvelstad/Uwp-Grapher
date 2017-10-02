using Grapher.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Grapher.Models
{
    class Grid
    {

        private int         dimensions;
        private int[]       grid;
        Tuple<int, int>[]   edges_array;

        public Point[]      candidate;
        public int          intersection_count;

        public Grid(Graph graph)
        {

            dimensions = (int)Math.Ceiling(Math.Sqrt(graph.nodes.Count));
            grid       = new int[dimensions * dimensions];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = i;
            }

            edges_array = new Tuple<int, int>[graph.edges.Count];
            for(int i = 0; i < graph.edges.Count; i++)
            {

                var from_index = graph.nodes.FindIndex(
                    (x) =>
                    {
                        return x == graph.edges[i].Item1;
                    }
                );

                var to_index = graph.nodes.FindIndex(
                    (x) =>
                    {
                        return x == graph.edges[i].Item2;
                    }
                );

                edges_array[i] = new Tuple<int, int>(
                    from_index,
                    to_index
                );

            }

            candidate = new Point[graph.nodes.Count];

            Process();

        }
        
        public int PermutationsCount()
        {
            int n = dimensions * dimensions;
            return n == 0 ? 1 : Enumerable.Range(1, n).Aggregate((acc, x) => acc * x);
        }

        private void Process()
        {

            intersection_count = 0;

            for (int i = 0; i < candidate.Length; i++)
            {
                candidate[i].X = grid[i] % dimensions;
                candidate[i].Y = grid[i] / dimensions;
            }

            for (int i = 0; i < edges_array.Length; i++)
            {
                for (int j = i + 1; j < edges_array.Length; j++)
                {
                    if (
                         LinearAlgebra.Intersection(
                             candidate[edges_array[i].Item1],
                             candidate[edges_array[i].Item2],
                             candidate[edges_array[j].Item1],
                             candidate[edges_array[j].Item2]
                         )
                     )
                    {
                        intersection_count++;
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
                intersection_count = int.MaxValue;
                candidate = null;
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
