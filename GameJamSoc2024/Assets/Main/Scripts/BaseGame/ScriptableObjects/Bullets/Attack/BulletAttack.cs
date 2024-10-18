using Models;
using UnityEngine;

namespace Bullets.Attack
{
    public abstract class BulletAttack : ScriptableObject
    {
        public abstract void Attack(BulletModel model);
    }
}