using Main.Scripts.DevelopmentUtilities.Pools;
using Main.Scripts.TowerDefenseGame._Managers;
using Main.Scripts.TowerDefenseGame.Interfaces;
using Main.Scripts.TowerDefenseGame.Models;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.Commands
{
    public class CmdSpawn : ICommando
    {
        private GameObject m_prefab;
        private GameObject m_instance;
        private Vector3 m_position;

        private PoolManager m_poolManager = new PoolManager();

        public CmdSpawn(GameObject p_prefab, Vector3 p_spawnPosition)
        {
            m_prefab = p_prefab;
            m_position = p_spawnPosition;
        }
        
        public void Execute()
        {
            m_instance = m_poolManager.Spawn(m_prefab);
            m_instance.transform.position = m_position;
        }
        public void Undo()
        {
            if (m_instance.TryGetComponent(out TowerModel l_towerModel))
            {
                GameManager.Instance.OnChangeMoney(l_towerModel.GetData().Cost);
            }
            
            m_poolManager.ReturnToPool(m_instance, m_prefab);
        }
    }
}