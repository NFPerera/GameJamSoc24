using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.DevelopmentUtilities.Pools
{
    public class PoolManager
    {
        private readonly Dictionary<Object, IPool> m_pools = new();

        public T Spawn<T>(T p_prefab, Transform p_parent = null) where T : Object
        {
            if (!m_pools.ContainsKey(p_prefab))
            {
                var l_newPool = new PoolGeneric<T>(p_prefab, p_parent);
                m_pools.Add(p_prefab, l_newPool);
            }

            var l_pool = (PoolGeneric<T>)m_pools[p_prefab];
            return l_pool.GetOrCreate();
        }

        public void ReturnToPool<T>(T p_obj, T p_prefab) where T : Object
        {
            if (m_pools.ContainsKey(p_prefab))
            {
                var l_pool = (PoolGeneric<T>)m_pools[p_prefab];
                l_pool.AddPool(p_obj);
            }
        }

        public void ClearAllPools()
        {
            foreach (var l_pool in m_pools.Values)
            {
                l_pool.ClearData();
            }
            m_pools.Clear();
        }
    }
}