using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var list1 = new List<int>() {1, 2, 3, 4, 5, 6,};
            var list2 = new List<int>() {5, 6};
            var list3 = new List<int>() {1, 3};
            var list4 = new List<int>() {1, 2, 3, 4, 5, 6, 7};
            var list5 = new List<int>() {1, 2};

            Assert.AreEqual(true, list1.ContainsSubsequence(list2));
            Assert.AreEqual(false, list1.ContainsSubsequence(list3));
            Assert.AreEqual(false, list1.ContainsSubsequence(list4));
            Assert.AreEqual(true, list1.ContainsSubsequence(list5));
        }
    }


    public static class ExtensionMethods
    {
    

        public static bool ContainsSubsequence<T>(this List<T> sequence, List<T> subsequence)
        {
            return
                Enumerable
                    .Range(0, sequence.Count - subsequence.Count + 1)
                    .Any(n => sequence.Skip(n).Take(subsequence.Count).SequenceEqual(subsequence));
        }
    }
}