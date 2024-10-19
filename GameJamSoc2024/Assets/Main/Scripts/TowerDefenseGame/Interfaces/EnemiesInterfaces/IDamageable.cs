using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.Interfaces.EnemiesInterfaces
{
    public interface IDamageable
    {
        Transform GetTransform();
        void DoDamage(int damage);
        void Heal(int healAmount);
    }
}