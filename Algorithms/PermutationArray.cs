using Grapher.Models;
using System;
using System.Collections.Generic;

namespace Grapher.Algorithms
{

    public class PermutationArray
    {

        public PermutationArray(uint rows, uint cols)
        {
            this.rows = rows;
            this.cols = cols;
            array = new uint[rows * cols];
            for (uint i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }
        }

        private uint   rows;
        private uint   cols;
        private uint[] array;

        public int Count
        {
            get
            {
                int count = 1;
                for (int i = (int)(rows * cols); i > 1; count *= i--)
                {
                }
                return count;
            }
        }
        
        public Pixel[] Current
        {
            get
            {
                var current = new Pixel[rows * cols];
                for (uint i = 0; i < array.Length; i++)
                {
                    current[(int)array[i]] = new Pixel
                    {
                        X = i % cols,
                        Y = i / cols
                    };
                }
                return current;
            }
        }
        
        public bool Permutate()
        {

            uint n = (uint)array.Length;
            uint k = uint.MaxValue;

            for (uint i = 1; i < n; i++)
            {
                if (array[i - 1] < array[i])
                {
                    k = i - 1;
                }
            }

            if (k == uint.MaxValue)
            {
                for (uint i = 0; i < n; i++)
                {
                    array[i] = i;
                }
                return false;
            }

            uint l = k + 1;

            for (uint i = l; i < n; i++)
            {
                if (array[k] < array[i])
                {
                    l = i;
                }
            }

            uint t = array[k];
            array[k] = array[l];
            array[l] = t;

            Array.Reverse(array, (int)k + 1, array.Length - ((int)k + 1));
            
            return true;

        }

    }

}
