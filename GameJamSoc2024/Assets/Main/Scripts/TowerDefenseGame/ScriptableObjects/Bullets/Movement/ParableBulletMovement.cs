using System.Collections.Generic;
using Main.Scripts.TowerDefenseGame.Models;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.ScriptableObjects.Bullets.Movement
{
    [CreateAssetMenu(fileName = "ParableBulletMovement", menuName = "_main/Bullet/Data/Movement/ParableBulletMovement", order = 0)]
    public class ParableBulletMovement : BulletMovement
    {
        private float gravity = 9.81f; // Gravity constant
        private class MovementData
        {
            public Vector3 StartPos;
            public Transform TargetTransform;
            public Vector3 InitVel;
            public float TimeElapsed;
            public float FlightTime; // Added flight time to track projectile duration
        }

        private Dictionary<BulletModel, MovementData> m_dictionary = new Dictionary<BulletModel, MovementData>();

        public override void Initialize(BulletModel p_model)
        {
            m_dictionary[p_model] = new MovementData();
            m_dictionary[p_model].StartPos = p_model.InitPos;
            m_dictionary[p_model].TargetTransform = p_model.GetTargetTransform();
            
            // Calculate initial velocity for reaching the target
            CalculateVelocity(m_dictionary[p_model].TargetTransform.position, p_model);
        }

        public override void Move(BulletModel p_model)
        {
            m_dictionary[p_model].TimeElapsed += Time.deltaTime;

            // Calculate current position of the projectile
            Vector3 currentPosition = CalculateProjectilePosition(m_dictionary[p_model].TimeElapsed, p_model);
            
            // Check if the projectile reached the target based on flight time
            if (m_dictionary[p_model].TimeElapsed >= m_dictionary[p_model].FlightTime)
            {
                OnReachTarget(p_model);
                return;
            }

            p_model.Move(currentPosition, p_model.GetData().Speed);
        }

        public override void OnReachTarget(BulletModel p_model)
        {
            // Remove movement data when reaching the target
            m_dictionary.Remove(p_model);
        }

        // Calculate initial velocity to reach the target
        void CalculateVelocity(Vector3 targetPos, BulletModel p_model)
        {
            MovementData data = m_dictionary[p_model];

            Vector3 direction = targetPos - data.StartPos;

            // Separate direction into XZ (horizontal) and Y (vertical) components
            Vector3 directionXZ = new Vector3(direction.x, 0, direction.z);
            float distanceXZ = directionXZ.magnitude;
            float heightDifference = direction.y;

            // Solve the quadratic equation for time based on vertical motion
            float velocityXZ = p_model.GetData().Speed;
            float time = distanceXZ / velocityXZ;

            // Use the flight time to calculate the vertical velocity needed to reach the target
            float velocityY = (heightDifference + 0.5f * gravity * time * time) / time;

            // Save the initial velocities and flight time
            data.InitVel = new Vector3(directionXZ.normalized.x * velocityXZ, velocityY, directionXZ.normalized.z * velocityXZ);
            data.FlightTime = time;
        }

        // Calculate projectile position based on elapsed time
        Vector3 CalculateProjectilePosition(float time, BulletModel p_model)
        {
            MovementData data = m_dictionary[p_model];
            Vector3 initialPos = data.StartPos;
            Vector3 initialVel = data.InitVel;

            // Parabolic motion equations
            float x = initialPos.x + initialVel.x * time;
            float z = initialPos.z + initialVel.z * time;
            float y = initialPos.y + initialVel.y * time - 0.5f * gravity * time * time;

            return new Vector3(x, y, z);
        }
    }
}