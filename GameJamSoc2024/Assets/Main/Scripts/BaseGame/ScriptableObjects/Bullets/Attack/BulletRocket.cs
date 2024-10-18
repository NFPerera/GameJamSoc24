using _Managers;
using Commands;
using Interfaces.EnemiesInterfaces;
using Models;
using UnityEngine;

namespace Bullets.Attack
{
    [CreateAssetMenu(fileName = "BulletRocket", menuName = "_main/Bullet/Data/Attack/RocketBulletAttack", order = 0)]
    public class BulletRocket : BulletAttack
    {
        private Collider2D[] _overlapArea = new Collider2D[64];
        
        public override void Attack(BulletModel model)
        {
            if (model.GetTargetTransform() != null)
            {
                GameManager.Instance.AddEventQueue(new CmdDoDamage(model.GetTargetIDamageable(), model.GetDamage()));
                
                Physics2D.OverlapCircleNonAlloc(model.transform.position, 3, _overlapArea);
                foreach (Collider2D collider in _overlapArea)
                {
                    if(!collider.CompareTag("Enemies"))
                        return;

                    if (collider.TryGetComponent(out IDamageable damageable))
                    {
                        GameManager.Instance.AddEventQueue(new CmdDoDamage(damageable, model.GetDamage()));
                    }
                }
            
                Destroy(model.gameObject);
            }
            else
                Destroy(model.gameObject);
            
        }
    }
}