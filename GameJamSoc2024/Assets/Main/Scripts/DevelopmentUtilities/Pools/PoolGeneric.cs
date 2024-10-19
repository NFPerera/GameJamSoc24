using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.DevelopmentUtilities.Pools
{
    public class PoolGeneric<T> : IPool where T : Object
    {
        private readonly T m_prefab;
        private readonly Transform m_parent;
        private readonly Queue<T> m_availables = new();
        private readonly List<T> m_inUse = new();

        public PoolGeneric(T p_prefab, Transform p_transformParent = default)
        {
            m_prefab = p_prefab;
            m_parent = p_transformParent;
        }

        public T GetOrCreate()
        {
            if (m_availables.Count > 0)
            {
                var l_obj = m_availables.Dequeue();
                while (l_obj == default &&  m_availables.Count > 0)
                {
                    l_obj = m_availables.Dequeue();
                }
                if (l_obj == default)
                    l_obj = Object.Instantiate(m_prefab, m_parent);
                m_inUse.Add(l_obj);
                return l_obj;
            }

            var l_newObj = Object.Instantiate(m_prefab, m_parent);
            m_inUse.Add(l_newObj);
            return l_newObj;
        }

        public void AddPool(T p_poolEntry)
        {
            if (!m_inUse.Contains(p_poolEntry)) 
                return;
            
            m_inUse.Remove(p_poolEntry);
            m_availables.Enqueue(p_poolEntry);
        }

        public void ClearData()
        {
            m_inUse.Clear();
            m_availables.Clear();
        }
    }
}
