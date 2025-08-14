using System.Collections.Generic;
using UnityEngine;

namespace AdventurerVillage.Utilities
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int j = Random.Range(i, list.Count);
                /*Representation of line below
                 T temp = list[i];
                list[i] = list[j];
                list[j] = temp;*/
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}