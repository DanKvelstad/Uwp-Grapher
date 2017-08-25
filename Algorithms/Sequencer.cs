using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher.Algorithms
{

    static class Sequencer
    {

        public static int PermutationsCount(double state_count)
        {
            int grid_dimensions = (int)Math.Ceiling(Math.Sqrt(state_count));
            int n = grid_dimensions * grid_dimensions;
            return n == 0 ? 1 : Enumerable.Range(1, n).Aggregate((acc, x) => acc * x);
        }


        public static bool NextPermutation(int[] perm)
        {

            int n = perm.Length;

            int k = -1;

            for (int i = 1; i < n; i++)

                if (perm[i - 1] < perm[i])

                    k = i - 1;

            if (k == -1)

            {

                for (int i = 0; i < n; i++)

                    perm[i] = i;

                return false;

            }



            int l = k + 1;

            for (int i = l; i < n; i++)

                if (perm[k] < perm[i])

                    l = i;



            int t = perm[k];

            perm[k] = perm[l];

            perm[l] = t;

            Array.Reverse(perm, k + 1, perm.Length - (k + 1));

            return true;

        }

    }

}
