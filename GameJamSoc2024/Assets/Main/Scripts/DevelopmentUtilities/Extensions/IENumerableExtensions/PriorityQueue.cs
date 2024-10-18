using System;
using System.Collections.Generic;

namespace _Main.Scripts.DevelopmentUtilities
{
    public class PriorityQueue<TData>
    {
        public bool IsEmpty { get { return m_data.Count < 1; } }

        private List<Tuple<TData, float>> m_data;
        private Dictionary<TData, int> m_indexes;
        private Func<float, float, bool> m_critery;

        public PriorityQueue()
        {
            m_data = new List<Tuple<TData, float>>();
            m_indexes = new Dictionary<TData, int>();
            m_critery = (p_x, p_y) => p_x.CompareTo(p_y) < 0;
        }

        public PriorityQueue(Func<float, float, bool> p_critery)
        {
            m_data = new List<Tuple<TData, float>>();
            m_indexes = new Dictionary<TData, int>();
            this.m_critery = p_critery;
        }

        public void Enqueue(TData p_data, float p_priority)
        {
            Enqueue(new Tuple<TData, float>(p_data, p_priority));
        }

        public void Enqueue(Tuple<TData, float> p_dp)
        {
            int l_currentIndex;
            int l_parentIndex;

            if (m_indexes.ContainsKey(p_dp.Item1))
            {
                l_currentIndex = m_indexes[p_dp.Item1];
                l_parentIndex = (l_currentIndex - 1) / 2;

                if (m_critery(m_data[l_currentIndex].Item2, p_dp.Item2)) return;

                m_data[l_currentIndex] = p_dp;
            }
            else
            {
                m_data.Add(p_dp);

                l_currentIndex = m_data.Count - 1;
                l_parentIndex = (l_currentIndex - 1) / 2;

                m_indexes.Add(p_dp.Item1, l_currentIndex);
            }

            while (l_currentIndex > 0 && m_critery(m_data[l_currentIndex].Item2, m_data[l_parentIndex].Item2))
            {
                Swap(l_currentIndex, l_parentIndex);

                l_currentIndex = l_parentIndex;
                l_parentIndex = (l_currentIndex - 1) / 2;
            }
        }

        private void EnqueueData(Tuple<TData, float> p_dp)
        {
            m_data.Add(p_dp);

            int l_currentIndex = m_data.Count - 1;//La posicion del dato recien ingresado en la lista.
            int l_parentIndex = (l_currentIndex - 1) / 2; //La posicion del nodo padre en la lista. 

            m_indexes.Add(p_dp.Item1, l_currentIndex);

            while (l_currentIndex > 0 && m_critery(m_data[l_currentIndex].Item2, m_data[l_parentIndex].Item2))
            {
                Swap(l_currentIndex, l_parentIndex);

                l_currentIndex = l_parentIndex;
                l_parentIndex = (l_currentIndex - 1) / 2;
            }
        }

        public TData Peek()
        {
            return PeekTuple().Item1;
        }

        public Tuple<TData, float> PeekTuple()
        {
            return m_data[0];
        }

        public TData Dequeue()
        {
            return DequeueTuple().Item1;
        }

        public Tuple<TData, float> DequeueTuple()
        {
            var l_date = m_data[0];

            m_data[0] = m_data[m_data.Count - 1];
            m_indexes[m_data[0].Item1] = 0;

            m_data.RemoveAt(m_data.Count - 1);
            m_indexes.Remove(l_date.Item1);

            int l_currentIndex = 0;
            int l_leftIndex = 1;
            int l_rightIndex = 2;
            int l_exploreIndex = GetExplorerIndex(l_leftIndex, l_rightIndex);


            if (l_exploreIndex == -1) return l_date;

            while (m_critery(m_data[l_exploreIndex].Item2, m_data[l_currentIndex].Item2))
            {
                Swap(l_exploreIndex, l_currentIndex);

                l_currentIndex = l_exploreIndex;
                l_leftIndex = (l_currentIndex * 2) + 1;
                l_rightIndex = (l_currentIndex * 2) + 2;
                l_exploreIndex = GetExplorerIndex(l_leftIndex, l_rightIndex);

                if (l_exploreIndex == -1) break;
            }
            return l_date;
        }

        private int GetExplorerIndex(int p_leftIndex, int p_rightIndex)
        {
            if (m_data.Count > p_rightIndex)
                return m_critery(m_data[p_leftIndex].Item2, m_data[p_rightIndex].Item2) ? p_leftIndex : p_rightIndex;
            else if (m_data.Count > p_leftIndex)
                return p_leftIndex;

            return -1;
        }

        private void Swap(int p_from, int p_to)
        {
            //Swapeo referencia de indices en diccionario.
            
            m_indexes[m_data[p_from].Item1] = p_to;
            m_indexes[m_data[p_to].Item1] = p_from;

            //swapeo objetos en la lista.
            
            (m_data[p_from], m_data[p_to]) = (m_data[p_to], m_data[p_from]);
        }
    }
}
