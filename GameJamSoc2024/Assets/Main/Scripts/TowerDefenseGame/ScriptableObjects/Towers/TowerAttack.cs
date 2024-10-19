using Main.Scripts.TowerDefenseGame.Models;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.ScriptableObjects.Towers
{
    public abstract class TowerAttack : ScriptableObject
    {
        public abstract void Attack(TowerModel model);
    }
}