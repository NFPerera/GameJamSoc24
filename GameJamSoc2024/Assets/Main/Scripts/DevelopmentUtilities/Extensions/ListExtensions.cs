using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.DevelopmentUtilities.Extensions
{
    public static class ListExtensions
    {
        public static T GetRandomElement<T>(this List<T> p_baseList)
        {
            return p_baseList[Random.Range(0, p_baseList.Count)];
        }
    }
}