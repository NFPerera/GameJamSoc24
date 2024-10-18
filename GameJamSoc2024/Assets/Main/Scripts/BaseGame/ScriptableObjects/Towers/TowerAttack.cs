using Models;
using UnityEngine;

namespace Towers
{
    public abstract class TowerAttack : ScriptableObject
    {
        public abstract void Attack(TowerModel model);
    }
}