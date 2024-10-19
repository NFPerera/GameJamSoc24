using Main.Scripts.TowerDefenseGame._Managers;
using Main.Scripts.TowerDefenseGame.Commands;
using Main.Scripts.TowerDefenseGame.Models;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.ScriptableObjects.Bullets.Attack
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