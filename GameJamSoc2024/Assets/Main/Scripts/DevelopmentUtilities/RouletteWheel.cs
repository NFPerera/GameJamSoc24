using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.DevelopmentUtilities
{
    public class RouletteWheel<T>
    {
        public RouletteWheel()
        {
        }

        public RouletteWheel(Dictionary<T, float> p_items)
        {
            SetCachedDictionary(p_items);
        }

        public RouletteWheel(List<T> p_items, List<float> p_chances)
        {
            SetCachedDictionaryFromLists(p_items, p_chances);
        }

        private Dictionary<T, float> m_cachedDictionary = new();

        private float m_cachedSum;


        public static TY Run<TY>(Dictionary<TY, float> p_items)
        {
            float l_max = 0;

            foreach (var l_item in p_items)
            {
                l_max += l_item.Value;
            }

            var l_random = Random.Range(0, l_max);

            foreach (var l_item in p_items)
            {
                l_random -= l_item.Value;
                if (l_random <= 0)
                {
                    return l_item.Key;
                }
            }

            return default;
        }

        public void SetCachedDictionary(Dictionary<T, float> p_itemsToCache)
        {
            m_cachedDictionary = p_itemsToCache;

            m_cachedSum = 0;
            foreach (var l_item in m_cachedDictionary)
            {
                m_cachedSum += l_item.Value;
            }
        }

        public void SetCachedDictionaryFromLists(List<T> p_items, List<float> p_chances)
        {
            m_cachedDictionary = new Dictionary<T, float>();
            m_cachedSum = 0;

            for (int l_i = 0; l_i < p_items.Count; l_i++)
            {
                var l_chance = p_chances[l_i];
                m_cachedDictionary.Add(p_items[l_i], l_chance);
                m_cachedSum += l_chance;
            }
        }

        public T RunWithCached()
        {
            if (m_cachedDictionary == null)
                return default;

            var l_random = Random.Range(0, m_cachedSum);

            foreach (var l_item in m_cachedDictionary)
            {
                l_random -= l_item.Value;
                if (l_random <= 0)
                {
                    return l_item.Key;
                }
            }

            return default;
        }
    }
}