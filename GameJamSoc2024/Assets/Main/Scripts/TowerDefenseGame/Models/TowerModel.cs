using System.Collections.Generic;
using Main.Scripts.BaseGame.Interfaces.EnemiesInterfaces;
using Main.Scripts.BaseGame.Interfaces.TowerInterfaces;
using Main.Scripts.BaseGame.ScriptableObjects.Towers;
using UnityEngine;

namespace Main.Scripts.BaseGame.Models
{
    public class TowerModel : MonoBehaviour, ITower
    {
        [SerializeField] private TowerData data;
        [SerializeField] private Transform shootPoint;

        private List<IDamageable> m_enemiesInRange = new List<IDamageable>();
        private float m_timer;
        private void Update()
        {
            m_timer += Time.deltaTime;
            
            if (m_timer >= data.AttackSpeed)
            {
                Attack();
                m_timer = 0f;
            }
        }
        public void Attack() => data.TowerAttack.Attack(this);

        
        private void OnTriggerEnter(Collider p_col)
        {
            if (!p_col.TryGetComponent(out IDamageable l_damageable)) return;
            
            m_enemiesInRange.Add(l_damageable);
        }
        private void OnTriggerExit(Collider p_other)
        {
            if (!p_other.TryGetComponent(out IDamageable l_damageable)) return;
            
            m_enemiesInRange.Remove(l_damageable);
        }

        #region Getters
            public TowerData GetData() => data;
            public List<IDamageable> GetEnemiesInRange() => m_enemiesInRange;
            public Transform GetShootPoint() => shootPoint;

        #endregion
    }
}