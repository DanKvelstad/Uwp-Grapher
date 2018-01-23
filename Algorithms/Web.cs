using Grapher.Models;
using Grapher.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grapher.Algorithms
{

    public static class Web
    {

        public static int MapGridToNodes(List<NodeModel> nodes, Point[] grid)
        {
            int index = nodes[0].Grid.Count;
            for (int i=0; i<nodes.Count; i++)
            {
                nodes[i].Grid.Add(grid[i]);
            }
            return index;
        }

        public static Task<int> CalculateNodeGrids(
            GraphModel          graphModel,
            PermutationArray    permutationArray,
            int                 start,
            int                 count,
            int                 intersection_count_best
        )
        {
            return Task.Run(
                new Func<int>(
                    () =>
                    {
                        var end = start + count;
                        for (int i = start; i < end; i++)
                        {
                            var index = permutationArray.MapGridToNodes(graphModel.Nodes.ToList());
                            var intersection_count_now = 0;
                            for (int j = 0; j < graphModel.Edges.Count; j++)
                            {
                                for (int k = j + 1; k < graphModel.Edges.Count; k++)
                                {
                                    if (LinearAlgebra.Intersection(
                                            graphModel.Edges[j], graphModel.Edges[k], index
                                        )
                                    )
                                    {
                                        intersection_count_now++;
                                    }
                                }
                            }
                            if (intersection_count_now > intersection_count_best)
                            {
                                // this can be optimized by removing a range
                                foreach (var node in graphModel.Nodes)
                                {
                                    node.Grid.RemoveAt(index);
                                }
                            }
                            else if (intersection_count_now < intersection_count_best)
                            {
                                if (0 < index)
                                {
                                    foreach (var node in graphModel.Nodes)
                                    {
                                        node.Grid.RemoveRange(0, index - 1);
                                    }
                                }
                                intersection_count_best = intersection_count_now;
                            }
                            if (!permutationArray.Permutate())
                            {
                                return intersection_count_best;
                            }
                        }
                        return intersection_count_best;
                    }
                )
            );
        }

        public static Task CalculateCostsOfRange(
            List<Tuple<uint, int>>  costs, 
            List<EdgeModel>         edges, 
            int                     start, 
            int                     count
        )
        {
            return Task.Run(
                new Action(
                    () =>
                    {
                        var end = Math.Min(start + count, edges.Count);
                        for (int i = start; i < end; i++)
                        {
                            uint cost = 0;
                            foreach (var edge in edges)
                            {

                                var sp = edge.Source;
                                var tp = edge.Target;

                                var dx = sp.X - tp.X;
                                var dy = sp.Y - tp.Y;
                                cost += (uint)Math.Round(Math.Sqrt(dx * dx + dy * dy));

                            }
                            costs.Add(new Tuple<uint, int>(cost, i));
                        }
                    }
                )
            );
        }

        public static Task OrderGridAccordingToCost(List<Tuple<uint, int>> costs, List<NodeModel> nodes)
        {
            return Task.Run(
                new Action(
                    () =>
                    {
                        costs.Sort(
                            (a, b) =>
                            {
                                if (a.Item1 < b.Item1)
                                {
                                    return -1;
                                }
                                else if (a.Item1 > b.Item1)
                                {
                                    return 1;
                                }
                                else
                                {
                                    return 0;
                                }
                            }
                        );
                        foreach (var node in nodes)
                        {
                            var newGrid = new List<Point>(node.Grid.Count);
                            foreach (var cost in costs)
                            {
                                newGrid.Add(node.Grid[cost.Item2]);
                            }
                            node.Grid = newGrid;
                        }
                    }
                )
            );
        }
        
    }

}
