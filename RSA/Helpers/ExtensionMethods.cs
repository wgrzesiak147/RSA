using System.Collections.Generic;
using System.Linq;

namespace RSA.Helpers
{
    public static class ExtensionMethods{

        public static bool ContainsSubsequence<T>(this List<T> sequence, List<T> subsequence){
            if (sequence.Count < subsequence.Count)
            {
                return false;
            }
            return
                Enumerable
                    .Range(0, sequence.Count - subsequence.Count + 1)
                    .Any(n => sequence.Skip(n).Take(subsequence.Count).SequenceEqual(subsequence));
        }
    }
}
