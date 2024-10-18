using UnityEngine;

namespace Clases
{
    public class MovementController : MonoBehaviour
    {
        private float _speed = 10f;
        public void SetSpeed(float speed) => _speed = speed;

        public void Move(Vector3 direction)
        {
            if (gameObject == null) return;
            
            transform.Translate(direction * (_speed * Time.deltaTime));
        }
        public void MoveBackwards(Vector3 direction) => transform.Translate(-direction * (_speed * Time.deltaTime));
        

    }
}