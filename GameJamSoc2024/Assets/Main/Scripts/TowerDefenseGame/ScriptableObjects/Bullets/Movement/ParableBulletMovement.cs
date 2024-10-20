using System.Collections.Generic;
using System.Security.Cryptography;
using Main.Scripts.TowerDefenseGame.Models;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.ScriptableObjects.Bullets.Movement
{
    [CreateAssetMenu(fileName = "ParableBulletMovement", menuName = "_main/Bullet/Data/Movement/ParableBulletMovement", order = 0)]
    public class ParableBulletMovement : BulletMovement
    {
        [SerializeField] private float force;
        
        [SerializeField] private float timer;
        private class MovementData
        {
            public Transform TargetTransform;
            public Vector3 TargetInitPos;
            public Vector3 InitVel;
            public float currentTime;
            public Rigidbody rb;
        }

        private Dictionary<BulletModel, MovementData> m_dictionary = new Dictionary<BulletModel, MovementData>();

        public override void Initialize(BulletModel p_model)
        {
            m_dictionary[p_model] = new MovementData();
            
            m_dictionary[p_model].TargetTransform = p_model.GetTargetTransform();
            m_dictionary[p_model].TargetInitPos = p_model.GetTargetTransform().position;

            m_dictionary[p_model].currentTime= timer;

            

            if (p_model.TryGetComponent<Rigidbody>(out var rb))
            {
                m_dictionary[p_model].rb = rb;
                rb.velocity = Vector3.up*force;                
            }
        }
        
        public override void Move(BulletModel p_model)
        {
            
            m_dictionary[p_model].currentTime -= Time.deltaTime;
            if (m_dictionary[p_model].currentTime <= 0)
            {
                if (m_dictionary[p_model].rb.velocity.y > 0){
                    m_dictionary[p_model].rb.velocity = Vector3.zero;
                }

                Vector3 l_targetPosition = m_dictionary[p_model].TargetInitPos;
                
                if(m_dictionary[p_model].TargetTransform != null)
                    l_targetPosition = m_dictionary[p_model].TargetTransform.position;
                
                p_model.transform.position  = new Vector3(l_targetPosition.x, p_model.transform.position.y,l_targetPosition.z);
            }
        }

        public override void OnReachTarget(BulletModel p_model)
        {
            m_dictionary.Remove(p_model);
        }

    }
}