using System.Collections.Generic;
using Main.Scripts.TowerDefenseGame.Models;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.ScriptableObjects.Bullets.Movement
{
    [CreateAssetMenu(fileName = "ParableBulletMovement", menuName = "_main/Bullet/Data/Movement/ParableBulletMovement", order = 0)]
    public class ParableBulletMovement : BulletMovement
    {
        [SerializeField] private float force;
        private float gravity = 9.81f; // Gravity constant
        private class MovementData
        {
            public Vector3 StartPos;
            public Transform TargetTransform;
            public Vector3 InitVel;
        }

        private Dictionary<BulletModel, MovementData> m_dictionary = new Dictionary<BulletModel, MovementData>();

        public override void Initialize(BulletModel p_model)
        {
            m_dictionary[p_model] = new MovementData();
            m_dictionary[p_model].StartPos = p_model.transform.position;
            
            m_dictionary[p_model].TargetTransform = p_model.GetTargetTransform();
            CalculateVelocity(m_dictionary[p_model].TargetTransform.position, p_model);
            

            if (p_model.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.velocity = m_dictionary[p_model].InitVel;
            }
        }
        
        void CalculateVelocity(Vector3 p_targetPos, BulletModel p_model)
        {
            Vector3 l_direction = p_targetPos - m_dictionary[p_model].StartPos;

            // Separate direction into XZ (horizontal) and Y (vertical) components
            Vector3 l_directionXZ = new Vector3(l_direction.x, 0, l_direction.z);
            float l_distanceXZ = l_directionXZ.magnitude;
            float l_heightDifference = l_direction.y;
            

            // Solve the quadratic equation for time based on vertical motion
            float l_velocityXZ = force;
            float l_time = l_distanceXZ / l_velocityXZ;
            
            // Use the flight time to calculate the vertical velocity needed to reach the target
            float l_velocityY = gravity*l_time/2 + l_heightDifference/l_time;

            // Save the initial velocities and flight time
            m_dictionary[p_model].InitVel = new Vector3(l_directionXZ.normalized.x * l_velocityXZ, l_velocityY, l_directionXZ.normalized.z * l_velocityXZ);
        }

        public override void Move(BulletModel p_model)
        {
        }

        public override void OnReachTarget(BulletModel p_model)
        {
        }

    }
}