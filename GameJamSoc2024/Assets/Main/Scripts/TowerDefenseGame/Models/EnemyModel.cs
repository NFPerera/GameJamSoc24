using System;
using System.Collections.Generic;
using Main.Scripts.TowerDefenseGame._Managers;
using Main.Scripts.TowerDefenseGame.Clases;
using Main.Scripts.TowerDefenseGame.Interfaces.EnemiesInterfaces;
using Main.Scripts.TowerDefenseGame.ScriptableObjects.Enemies;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.Models
{
    [RequireComponent(typeof(MovementController))]
    public class EnemyModel : MonoBehaviour, IDamageable
    {
        [SerializeField] private EnemyData data;
        [SerializeField] private int index;
        
        
        private HealthController m_healthController;
        private MovementController m_movementController;
        private SpriteRenderer m_sprite;
        private List<Transform> m_pathPoints;
        private float m_speed;
        private int m_indexPathPoints;

        public event Action<EnemyModel> OnDeath;

        private void Awake()
        {
            m_pathPoints = GameManager.Instance.PathPoints;
            m_healthController = new HealthController(data.enemiesTierDatas[index].MaxHealth);
            
            m_movementController = gameObject.GetComponent<MovementController>();
            m_movementController.SetSpeed(data.enemiesTierDatas[index].Speed);

            //m_sprite = gameObject.GetComponent<SpriteRenderer>();
            //m_sprite.sprite = data.enemiesTierDatas[index].Sprite;
        }


        private void Update()
        {
            var l_position = transform.position;
            var l_distanceToTarget = Vector2.Distance(l_position, m_pathPoints[m_indexPathPoints < m_pathPoints.Count? m_indexPathPoints : 0].position);
            var l_dir = Vector3.zero;

            if (l_distanceToTarget < 0.1f)
            {
                m_indexPathPoints++;
                
                if (m_indexPathPoints >= m_pathPoints.Count)
                {
                    GameManager.Instance.OnChangeLifePoints.Invoke(index+ 1);
                    Destroy(gameObject);
                    
                    return;
                }
                l_dir = (m_pathPoints[m_indexPathPoints].position - l_position).normalized;
                
            }
            
            if(m_indexPathPoints < m_pathPoints.Count)
                l_dir = (m_pathPoints[m_indexPathPoints].position - l_position).normalized;
            
            transform.Translate(l_dir * (data.enemiesTierDatas[index].Speed * Time.deltaTime));
        }

        #region IDamageable

            public Transform GetTransform() => transform;

            public void DoDamage(int p_damage)
            {
                m_healthController?.TakeDamage(p_damage);
                
                if (m_healthController?.Hp <= 0)  LowerTier();
            }

            public void Heal(int p_healAmount) => m_healthController?.HealHp(p_healAmount);

        #endregion
        
        private void LowerTier()
        {
            index--;
            
            if (index < 0)
            {
                OnDie();
                return;
            }
            GameManager.Instance.OnChangeMoney.Invoke(5);
            ChangeStats();
        }
        
        private void ChangeStats()
        {
            m_healthController = new HealthController(data.enemiesTierDatas[index].MaxHealth);
            m_movementController.SetSpeed(data.enemiesTierDatas[index].Speed);
            m_sprite.sprite = data.enemiesTierDatas[index].Sprite;
        }
        private void OnDie()
        {
            GameManager.Instance.OnChangeMoney.Invoke(10);
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }
    }
}