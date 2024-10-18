using Models;
using UnityEngine;

namespace Bullets.Movement
{
    public abstract class BulletMovement : ScriptableObject
    {
        public abstract void Move(BulletModel model);
    }
}