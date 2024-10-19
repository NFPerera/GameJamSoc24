using Main.Scripts.TowerDefenseGame.Models;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.ScriptableObjects.Bullets.Movement
{
    public abstract class BulletMovement : ScriptableObject
    {
        public virtual void Initialize(BulletModel p_model) { }
        public abstract void Move(BulletModel model);
        public virtual void OnReachTarget(BulletModel p_model){}
    }
}