using Main.Scripts.BaseGame.Models;
using UnityEngine;

namespace Main.Scripts.BaseGame.ScriptableObjects.Bullets.Movement
{
    public abstract class BulletMovement : ScriptableObject
    {
        public abstract void Move(BulletModel model);
    }
}