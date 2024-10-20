﻿using System;
using System.Numerics;
using Main.Scripts.DevelopmentUtilities.Extensions;
using Main.Scripts.TowerDefenseGame.Interfaces.BulletsInterfaces;
using Main.Scripts.TowerDefenseGame.Interfaces.EnemiesInterfaces;
using Main.Scripts.TowerDefenseGame.ScriptableObjects.Bullets;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Main.Scripts.TowerDefenseGame.Models
{
    public class BulletModel : MonoBehaviour, IBullet
    {
        [SerializeField] private BulletData data;

        private Transform m_target;
        [SerializeField] private Transform body;
        public Vector3 InitPos { get; private set; }
        private IDamageable m_targetDamageable;
        private int m_damage;
        private bool m_reachTarget;
        private float m_lifeTime;
        private Vector3 m_targetPos;

        public BulletData GetData() => data;
        public void InitializeBullet(Transform p_target, int p_damage, float p_lifeTime, Vector3 p_initPos)
        {
            InitPos = p_initPos;
            m_damage = p_damage;
            m_target = p_target;
            m_lifeTime = p_lifeTime;
            m_reachTarget = false;
            m_targetPos = m_target.position;
            
            data.BulletMovement.Initialize(this);
        }

        private void OnDestroy()
        {
            data.BulletMovement.OnReachTarget(this);
        }

        private void Update()
        {
            if (!m_reachTarget && m_targetPos != null)
            {
                data.BulletMovement.Move(this);
            }
            //else Destroy(gameObject);

            m_lifeTime -= Time.deltaTime;
            if(m_lifeTime <= 0f)
                Destroy(gameObject);
        }

        public void Move(Vector3 p_dir, float p_speed)
        {
             transform.position += p_dir * (p_speed * Time.deltaTime);
             

            // Rotate the bullet to face the direction it is moving
            if (p_dir != Vector3.zero)
            {
                UnityEngine.Quaternion targetRotation = UnityEngine.Quaternion.LookRotation(p_dir);
                transform.rotation = targetRotation * UnityEngine.Quaternion.Euler(0, 90*2, 0);
            }

        }
        private void OnTriggerEnter(Collider p_col)
        {

            if (!p_col.TryGetComponent(out IDamageable l_damageable))
            {
                if (data.TargetLayer.Includes(p_col.gameObject.layer))
                {
                    data.BulletAttack.Attack(this);
                    m_reachTarget = true;
                }
                return;
            }

            if (!m_reachTarget)
            {
                SetTargetDamageable(l_damageable);
                data.BulletAttack.Attack(this);
                m_reachTarget = true;
            }
        }

        private void SetTargetDamageable(IDamageable p_target) => m_targetDamageable = p_target;

        #region Getters
            public IDamageable GetTargetIDamageable() => m_targetDamageable;
            public Transform GetTargetTransform() => m_target;
            public int GetDamage() => m_damage;
            public Vector3 GetTargetPos() => m_targetPos;

        #endregion
        

        
    }
}