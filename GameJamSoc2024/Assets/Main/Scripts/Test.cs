using System;
using UnityEngine;

namespace Main.Scripts
{
    public class Test : MonoBehaviour
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

        private void Awake()
        {
            Initialize();
        }

        [SerializeField] private Transform targetTransform;
        private Vector3 InitPos;

        public void Initialize()
        {
            InitPos = transform.position;
            
            // Calculate initial velocity for reaching the target
            CalculateVelocity(targetTransform.position);
            timeElapsed = 0f;

            if (TryGetComponent<Rigidbody>(out var rb))
            {
                rb.velocity = InitVel;
            }
            
        }


        public float Speed;
        private float timeElapsed;
        private float FlightTime;

        public Vector3 InitVel;
        // Calculate initial velocity to reach the target
        void CalculateVelocity(Vector3 targetPos)
        {
            Vector3 direction = targetPos - InitPos;

            // Separate direction into XZ (horizontal) and Y (vertical) components
            Vector3 directionXZ = new Vector3(direction.x, 0, direction.z);
            float distanceXZ = directionXZ.magnitude;
            float heightDifference = direction.y;
            print(heightDifference);
            print(distanceXZ);
            

            // Solve the quadratic equation for time based on vertical motion
            float velocityXZ = Speed;
            float time = distanceXZ / velocityXZ;
            print("flighttime:"+  time);       
            // Use the flight time to calculate the vertical velocity needed to reach the target
            float velocityY = gravity*time/2;

            // Save the initial velocities and flight time
            InitVel = new Vector3(directionXZ.normalized.x * velocityXZ, velocityY, directionXZ.normalized.z * velocityXZ);
            FlightTime = time;
        }

    }
}