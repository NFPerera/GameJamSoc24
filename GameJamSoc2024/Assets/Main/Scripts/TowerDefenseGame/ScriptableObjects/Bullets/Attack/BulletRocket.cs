using Main.Scripts.TowerDefenseGame._Managers;
using Main.Scripts.TowerDefenseGame.Commands;
using Main.Scripts.TowerDefenseGame.Interfaces.EnemiesInterfaces;
using Main.Scripts.TowerDefenseGame.Models;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.ScriptableObjects.Bullets.Attack
{
    [CreateAssetMenu(fileName = "BulletRocket", menuName = "_main/Bullet/Data/Attack/RocketBulletAttack", order = 0)]
    public class BulletRocket : BulletAttack
    {
        private Collider[] m_overlapResults = new Collider[64];
        [SerializeField] private LayerMask enemyLayer;
        public override void Attack(BulletModel model)
        {
            if (model.GetTargetTransform() != null)
            {
                if(model.GetTargetIDamageable() != null)
                    GameManager.Instance.AddEventQueue(new CmdDoDamage(model.GetTargetIDamageable(), model.GetDamage(),model.GetHitsFlying()));
                
                var l_count = Physics.OverlapSphereNonAlloc(model.transform.position, 3, m_overlapResults, enemyLayer);

                for (int i = 0; i < l_count; i++)
                {
                    var col = m_overlapResults[i];

                    if (col.TryGetComponent(out IDamageable damageable))
                    {
                        GameManager.Instance.AddEventQueue(new CmdDoDamage(damageable, model.GetDamage(),model.GetHitsFlying()));
                    }
                }
                Debug.Log("destroy in rok");
                Destroy(model.gameObject);
            }
            else
                Destroy(model.gameObject);
            
            Debug.Log("destroy in rok222");
            
        }
    }
}