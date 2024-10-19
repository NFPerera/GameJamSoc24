using Main.Scripts.TowerDefenseGame.Models;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.ScriptableObjects.Bullets.Attack
{
    public abstract class BulletAttack : ScriptableObject
    {
        public abstract void Attack(BulletModel model);
    }
}