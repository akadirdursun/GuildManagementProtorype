using System;
using System.Collections.Generic;
using System.Linq;
namespace AdventurerVillage.Utilities
{
    public static class ArrayExtensions
    {
        public static int FindIndex<T>(this IEnumerable<T> sequence, Func<T, bool> condition)
        {
            var i = 0;
            foreach (var element in sequence)
            {
                if (condition(element)) return i;
                i++;
            }
            return -1;
        }
        
        public static T Random<T>(this IEnumerable<T> sequence)
        {
            var array = sequence as T[] ?? sequence.ToArray();
            var rs = UnityEngine.Random.Range(0, array.Count());
            return array[rs];
        }
        
        public static void Shuffle<T>(this T[] array)
        {
            var arrayLength = array.Length;
            for (int i = 0; i < arrayLength; i++)
            {
                int j = UnityEngine.Random.Range(i, arrayLength);
                /*Representation of line below
                 T temp = list[i];
                list[i] = list[j];
                list[j] = temp;*/
                (array[i], array[j]) = (array[j], array[i]);
            }
        }
    }
}