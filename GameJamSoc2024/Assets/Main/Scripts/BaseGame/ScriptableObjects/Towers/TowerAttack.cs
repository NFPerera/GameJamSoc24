using Main.Scripts.BaseGame.Models;
using UnityEngine;

namespace Main.Scripts.BaseGame.ScriptableObjects.Towers
{
    public abstract class TowerAttack : ScriptableObject
    {
        public abstract void Attack(TowerModel model);
    }
}