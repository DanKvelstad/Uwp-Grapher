//using Grapher.Models;
//using System;
//using System.Collections.Specialized;
//using System.Threading;

//namespace Grapher.Algorithms
//{

//    public class Web
//    {

//        public GraphModel Model
//        {
//            get
//            {
//                return model;
//            }
//            set
//            {

//                if (null != model)
//                {
//                    CancelTokenSource?.Cancel();
//                    model.NodeModels.CollectionChanged    -= Nodes_CollectionChanged;
//                    model.Dimensions.PropertyChanged -= GraphDimensions_PropertyChanged;
//                }

//                model = value;

//                if (null != model)
//                {
//                    model.Dimensions.PropertyChanged += GraphDimensions_PropertyChanged;
//                    model.NodeModels.CollectionChanged    += Nodes_CollectionChanged;
//                }

//                Calculate();

//            }
//        }
//        private GraphModel model;
        
//        private void GraphDimensions_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
//        {
//            switch (e.PropertyName)
//            {
//                case nameof(DimensionsModel.NodeWidth):
//                case nameof(DimensionsModel.NodeHeight):
//                    Calculate();
//                    break;
//            }
//        }

//        private void Nodes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
//        {
//            Calculate();
//        }

//        private Point[] CurrentLayout
//        {
//            get
//            {
//                return currentLayout;
//            }
//            set
//            {

//                currentLayout = value;

//                if (null != currentLayout)
//                {
//                    for (int i = 0; i < Model.NodeModels.Count; i++)
//                    {
//                        Model.NodeModels[i].Left = currentLayout[i].X * Model.Dimensions.NodeWidth +
//                                                   currentLayout[i].X * Model.Dimensions.EdgeWidth;
//                        Model.NodeModels[i].Top =  currentLayout[i].Y * Model.Dimensions.NodeHeight +
//                                                   currentLayout[i].Y * Model.Dimensions.EdgeHeight;
//                    }
//                }

//            }
//        }
//        private Point[] currentLayout;

//        public CancellationTokenSource CancelTokenSource
//        {
//            get
//            {
//                return cancelTokenSource;
//            }
//            set
//            {
//                cancelTokenSource = value;
//            }
//        }
//        public CancellationTokenSource cancelTokenSource;

//        public void Calculate()
//        {
//            // Task.Run(new Action(CalculateInternal), CancelTokenSource.Token);
//            try
//            {
//                while (CancelTokenSource?.Token.WaitHandle.WaitOne() ?? false)
//                {
//                    CancelTokenSource?.Cancel();
//                }
//                CancelTokenSource = new CancellationTokenSource();
//                CalculateInternal();
//            }
//            catch (OperationCanceledException)
//            {   CurrentLayout = null;
//            };

//            CancelTokenSource = null;

//        }

//        private void CalculateInternal()
//        {

//            CancelTokenSource.Token.ThrowIfCancellationRequested();

//            PermutationArray permutationArray;
//            {
//                uint dimensions = (uint)Math.Ceiling(Math.Sqrt(Model.NodeModels.Count));
//                permutationArray = new PermutationArray(dimensions, dimensions);
//            }

//            CurrentLayout = permutationArray.Current;

//            CancelTokenSource.Token.ThrowIfCancellationRequested();
            
//            // ToDo: Actually find the best permutation

//        }
        
        
        
        
        
        
        
//        //public int MapGridToNodes(List<NodeModel> nodes, Point[] grid)
//        //{
//        //    int index = nodes[0].Grid.Count;
//        //    for (int i=0; i<nodes.Count; i++)
//        //    {
//        //        nodes[i].Grid.Add(grid[i]);
//        //    }
//        //    return index;
//        //}
//        //
//        //public Task<int> CalculateNodeGrids(
//        //    int                 start,
//        //    int                 count,
//        //    int                 intersection_count_best
//        //)
//        //{
//        //    return Task.Run(
//        //        new Func<int>(
//        //            () =>
//        //            {
//        //                var end = start + count;
//        //                for (int i = start; i < end; i++)
//        //                {
//        //                    for (int i = 0; i < nodes.Count; i++)
//        //                    {
//        //                        nodes[i].AssignToGrid(
//        //                            (uint)current[i].X,
//        //                            (uint)current[i].Y
//        //                        );
//        //                    }
//        //                    var index = permutationArray.MapGridToNodes(graphModel.Nodes.ToList());
//        //                    var intersection_count_now = 0;
//        //                    for (int j = 0; j < graphModel.Edges.Count; j++)
//        //                    {
//        //                        for (int k = j + 1; k < graphModel.Edges.Count; k++)
//        //                        {
//        //                            if (LinearAlgebra.Intersection(
//        //                                    graphModel.Edges[j], graphModel.Edges[k], index
//        //                                )
//        //                            )
//        //                            {
//        //                                intersection_count_now++;
//        //                            }
//        //                        }
//        //                    }
//        //                    if (intersection_count_now > intersection_count_best)
//        //                    {
//        //                        // this can be optimized by removing a range
//        //                        foreach (var node in graphModel.Nodes)
//        //                        {
//        //                            node.Grid.RemoveAt(index);
//        //                        }
//        //                    }
//        //                    else if (intersection_count_now < intersection_count_best)
//        //                    {
//        //                        if (0 < index)
//        //                        {
//        //                            foreach (var node in graphModel.Nodes)
//        //                            {
//        //                                node.Grid.RemoveRange(0, index - 1);
//        //                            }
//        //                        }
//        //                        intersection_count_best = intersection_count_now;
//        //                    }
//        //                    if (!permutationArray.Permutate())
//        //                    {
//        //                        return intersection_count_best;
//        //                    }
//        //                }
//        //                return intersection_count_best;
//        //            }
//        //        )
//        //    );
//        //}
//        //
//        //public Task CalculateCostsOfRange(
//        //    List<Tuple<uint, int>>  costs, 
//        //    List<EdgeModel>         edges, 
//        //    int                     start, 
//        //    int                     count
//        //)
//        //{
//        //    return Task.Run(
//        //        new Action(
//        //            () =>
//        //            {
//        //                var end = Math.Min(start + count, edges.Count);
//        //                for (int i = start; i < end; i++)
//        //                {
//        //                    uint cost = 0;
//        //                    foreach (var edge in edges)
//        //                    {
//        //
//        //                        var sp = edge.Source;
//        //                        var tp = edge.Target;
//        //
//        //                        var dx = sp.X - tp.X;
//        //                        var dy = sp.Y - tp.Y;
//        //                        cost += (uint)Math.Round(Math.Sqrt(dx * dx + dy * dy));
//        //
//        //                    }
//        //                    costs.Add(new Tuple<uint, int>(cost, i));
//        //                }
//        //            }
//        //        )
//        //    );
//        //}
//        //
//        //public Task OrderGridAccordingToCost(List<Tuple<uint, int>> costs, List<NodeModel> nodes)
//        //{
//        //    return Task.Run(
//        //        new Action(
//        //            () =>
//        //            {
//        //                costs.Sort(
//        //                    (a, b) =>
//        //                    {
//        //                        if (a.Item1 < b.Item1)
//        //                        {
//        //                            return -1;
//        //                        }
//        //                        else if (a.Item1 > b.Item1)
//        //                        {
//        //                            return 1;
//        //                        }
//        //                        else
//        //                        {
//        //                            return 0;
//        //                        }
//        //                    }
//        //                );
//        //                foreach (var node in nodes)
//        //                {
//        //                    var newGrid = new List<Point>(node.Grid.Count);
//        //                    foreach (var cost in costs)
//        //                    {
//        //                        newGrid.Add(node.Grid[cost.Item2]);
//        //                    }
//        //                    node.Grid = newGrid;
//        //                }
//        //            }
//        //        )
//        //    );
//        //}
        
//    }

//}
