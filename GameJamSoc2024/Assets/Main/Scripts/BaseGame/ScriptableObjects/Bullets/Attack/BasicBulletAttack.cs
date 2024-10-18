using _Managers;
using Commands;
using Models;
using UnityEngine;

namespace Bullets.Attack
{
    [CreateAssetMenu(fileName = "BasicBulletAttack", menuName = "_main/Bullet/Data/Attack/BasicBulletAttack", order = 0)]
    public class BasicBulletAttack : BulletAttack
    {
        public override void Attack(BulletModel model)
        {
            GameManager.Instance.AddEventQueue(new CmdDoDamage(model.GetTargetIDamageable(), model.GetDamage()));
            
            Destroy(model.gameObject);
        }
    }
}