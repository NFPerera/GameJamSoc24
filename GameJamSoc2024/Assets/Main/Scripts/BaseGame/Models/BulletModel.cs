using Bullets;
using Clases;
using Commands;
using Interfaces.BulletsInterfaces;
using Interfaces.EnemiesInterfaces;
using UnityEngine;

namespace Models
{
    [RequireComponent(typeof(MovementController))]
    public class BulletModel : MonoBehaviour, IBullet
    {
        [SerializeField] private BulletData data;

        private Transform _target;
        private IDamageable _targetDamageable;
        private MovementController _movementController;
        private CmdMove _cmdMove;
        private int _damage;
        private bool _reachTarget;

        public BulletData GetData() => data;
        public void InitializeBullet(Transform target, int damage)
        {
            _damage = damage;
            _target = target;
            _reachTarget = false;

            _movementController = gameObject.GetComponent<MovementController>();
            _movementController.SetSpeed(data.Speed);

            var dir = (_target.position - transform.position).normalized;
            _cmdMove = new CmdMove(_movementController, dir);
        }
        private void Update()
        {
            if (!_reachTarget && _target != null)
            {
                _cmdMove.Execute();
                //GameManager.Instance.AddEventQueue(_cmdMove);
            }
            else Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent(out IDamageable damageable)) return;

            if (!_reachTarget)
            {
                SetTargetDamageable(damageable);
                data.BulletAttack.Attack(this);
                _reachTarget = true;
            }
        }

        private void SetTargetDamageable(IDamageable target) => _targetDamageable = target;

        #region Getters
            public IDamageable GetTargetIDamageable() => _targetDamageable;
            public Transform GetTargetTransform() => _target;
            public int GetDamage() => _damage;
            public MovementController GetMovementController() => _movementController;

        #endregion
        

        
    }
}