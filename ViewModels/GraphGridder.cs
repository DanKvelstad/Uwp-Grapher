using Grapher.Algorithms;
using Grapher.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Grapher.ViewModels
{

    public class GraphGridder : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        
        private double _Width = 500;
        public double Width
        {
            get
            {
                return _Width;
            }
            set
            {
                _Width = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Width"));
            }
        }

        public double _Height = 200;
        public double Height
        {
            get
            {
                return _Height;
            }
            set
            {
                _Height = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Height"));
            }
        }
        
        private int _ProgressMaximum;
        public  int ProgressMaximum
        {
            get
            {
                return _ProgressMaximum;
            }
            private set
            {
                if (value > Width)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _ProgressMaximum = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProgressMaximum"));
            }
        }

        private int _ProgressCurrent;
        public  int ProgressCurrent
        {
            get
            {
                return _ProgressCurrent;
            }
            private set
            {
                if(value>ProgressMaximum)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _ProgressCurrent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProgressCurrent"));
            }
        }

        public async Task<List<Point[]>> GridItAsync(GraphModel graph)
        {

            var grid                = new Grid(graph);
            var candidates          = new List<Point[]>();

            var PermutationCount    = grid.PermutationsCount();
                ProgressCurrent     = 0;
                ProgressMaximum     = 100;
            var ProgressStep        = Math.Ceiling(PermutationCount / 100.0);
            
            var intersection_count = int.MaxValue;
            for (ProgressCurrent = 0; ProgressCurrent < ProgressMaximum; ProgressCurrent++)
            {
                await Task.Run(
                    new Action(
                        () =>
                        {
                            for (int i = 0; i < ProgressStep; i++)
                            {
                                if (grid.intersection_count == intersection_count)
                                {
                                    candidates.Add((Point[])grid.candidate.Clone());
                                }
                                else if (grid.intersection_count < intersection_count)
                                {
                                    candidates.Clear();
                                    candidates.Add((Point[])grid.candidate.Clone());
                                    intersection_count = grid.intersection_count;
                                }
                                if (!grid.Next())
                                {
                                    return;
                                }
                            }
                        }
                    )
                );
            }
            
            if(0>=candidates.Count)
            {
                throw new Exception("Could not grid graph, candidates is zero");
            }

            return candidates;

        }

    }

}
