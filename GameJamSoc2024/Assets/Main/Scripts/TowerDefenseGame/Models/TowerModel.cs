using System;
using System.Collections.Generic;
using Main.Scripts.TowerDefenseGame.Interfaces.EnemiesInterfaces;
using Main.Scripts.TowerDefenseGame.Interfaces.TowerInterfaces;
using Main.Scripts.TowerDefenseGame.ScriptableObjects.Towers;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.Models
{
    public class TowerModel : MonoBehaviour, ITower
    {
        [SerializeField] private TowerData data;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject pivot;

 
        private List<IDamageable> m_enemiesInRange = new List<IDamageable>();
        private float m_timer;

        private Vector3 m_targetPos;

        private void Start()
        {
            var trigger = GetComponent<SphereCollider>();
            
                
            trigger.radius = data.Range;
            
        }

        private void Update()
        {
            m_timer += Time.deltaTime;
            if (m_enemiesInRange.Count == 0) return;
             if( m_enemiesInRange.Count > 0 && m_enemiesInRange[0] != null)
                try
                {
                    ChangeAimDir(m_enemiesInRange[0].GetTransform().position);
                }
                catch (Exception e)
                {
                    m_enemiesInRange.Remove(m_enemiesInRange[0]);
                }
                
            
            if (m_timer >= data.AttackSpeed)
            {
                Attack();
                m_timer = 0f;
            }
        }
        public void Attack() => data.TowerAttack.Attack(this);

        public void ChangeAimDir(Vector3 p_targetPos)
        {
            Vector3 directionToTarget = p_targetPos - transform.position;
            directionToTarget.Normalize();
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
            pivot.transform.rotation = lookRotation;
        }
        
        private void OnTriggerEnter(Collider p_col)
        {
            if (!p_col.TryGetComponent(out IDamageable l_damageable)) return;
            if (p_col.GetComponent<EnemyModel>().GetIsFlying() && !data.GetHitsFlying) return;
            m_enemiesInRange.Add(l_damageable);
            l_damageable.OnDeath += HandleEnemyDeath;
        }

        private void HandleEnemyDeath(IDamageable enemy)
        {
            m_enemiesInRange.Remove(enemy);
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