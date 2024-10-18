using System.Collections.Generic;
using _Managers;
using Clases;
using Enemies;
using Interfaces.EnemiesInterfaces;
using UnityEngine;

namespace Models
{
    [RequireComponent(typeof(MovementController))]
    public class EnemyModel : MonoBehaviour, IDamageable
    {
        [SerializeField] private EnemyData data;
        [SerializeField] private int index;
        
        
        private HealthController _healthController;
        private MovementController _movementController;
        private SpriteRenderer _sprite;
        private List<Transform> _pathPoints;
        private float _speed;
        private int _indexPathPoints;

        private void Awake()
        {
            _pathPoints = GameManager.Instance.PathPoints;
            _healthController = new HealthController(data.enemiesTierDatas[index].MaxHealth);
            
            _movementController = gameObject.GetComponent<MovementController>();
            _movementController.SetSpeed(data.enemiesTierDatas[index].Speed);

            _sprite = gameObject.GetComponent<SpriteRenderer>();
            _sprite.sprite = data.enemiesTierDatas[index].Sprite;
        }


        private void Update()
        {
            var position = transform.position;
            var distanceToTarget = Vector2.Distance(position, _pathPoints[_indexPathPoints < _pathPoints.Count? _indexPathPoints : 0].position);
            var dir = Vector3.zero;

            if (distanceToTarget < 0.1f)
            {
                _indexPathPoints++;
                
                if (_indexPathPoints >= _pathPoints.Count)
                {
                    GameManager.Instance.OnChangeLifePoints.Invoke(index+ 1);
                    Destroy(gameObject);
                    
                    return;
                }
                dir = (_pathPoints[_indexPathPoints].position - position).normalized;
                
            }
            
            if(_indexPathPoints < _pathPoints.Count)
                dir = (_pathPoints[_indexPathPoints].position - position).normalized;
            
            transform.Translate(dir * (data.enemiesTierDatas[index].Speed * Time.deltaTime));
        }

        #region IDamageable

            public Transform GetTransform() => transform;

            public void DoDamage(int damage)
            {
                _healthController?.TakeDamage(damage);
                
                if (_healthController?.Hp <= 0)  LowerTier();
            }

            public void Heal(int healAmount) => _healthController?.HealHp(healAmount);

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
            _healthController = new HealthController(data.enemiesTierDatas[index].MaxHealth);
            _movementController.SetSpeed(data.enemiesTierDatas[index].Speed);
            _sprite.sprite = data.enemiesTierDatas[index].Sprite;
        }
        private void OnDie()
        {
            GameManager.Instance.OnChangeMoney.Invoke(10);
            Destroy(gameObject);
        }
    }
}