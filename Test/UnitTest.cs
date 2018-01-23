using Microsoft.VisualStudio.TestTools.UnitTesting;
using Grapher.Algorithms;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    [TestClass]
    public class TestPermutationArray
    {

        [TestMethod]
        public void TestContent()
        {

            uint rows = 2;
            uint cols = 3;

            var uut = new PermutationArray(rows, cols);
            
            // 2*3=6, !6 = 720
            Assert.AreEqual(720, uut.Count);

            Func<List<uint[,]>, uint[,], bool> ListDoesNotContainElement = (list, element) =>
            {
                foreach (var existing in list)
                {
                    bool isTrulyUnique = false;
                    for (uint row = 0; row < rows; row++)
                    {
                        for (uint col = 0; col < cols; col++)
                        {
                            if (existing[row, col] != element[row, col])
                            {
                                isTrulyUnique = true;
                            }
                        }
                    }
                    if (!isTrulyUnique)
                    {
                        return false;
                    }
                }
                return true;
            };

            var seenSoFar = new List<uint[,]>();

            for(int i = 0; i<uut.Count; i++)
            {
                var matrix = new uint[rows, cols];
                Assert.IsTrue(
                    ListDoesNotContainElement(seenSoFar, matrix),
                    "duplicate permutation "+i
                );
                for (int j=0; j<uut.Current.Length; j++)
                {
                    matrix[(int)uut.Current[j].X, (int)uut.Current[j].Y] = (uint)j;
                }
                seenSoFar.Add(matrix);
                if(i < uut.Count-1)
                {
                    Assert.IsTrue(
                        uut.Permutate(),
                        "could not permutate " + i
                    );
                }
                else
                {
                    Assert.IsFalse(uut.Permutate());
                }
            }
            Assert.AreEqual(uut.Count, seenSoFar.Count);

            Random rnd = new Random();
            for(uint i=0; i < 10; i++)
            {

                var randomArray = new uint[] {
                    0, 1, 2, 3, 4, 5
                }.OrderBy(x => rnd.Next()).ToArray();
                var duplicate = new uint[rows, cols];
                for (uint row = 0; row < rows; row++)
                {
                    for (uint col = 0; col < cols; col++)
                    {
                        duplicate[row, col] = randomArray[row * cols + col];
                    }
                }

                Assert.IsFalse(
                    ListDoesNotContainElement(seenSoFar, duplicate),
                    message: "found a duplicate matrix through random check"
                );

            }

        }

    }
}
