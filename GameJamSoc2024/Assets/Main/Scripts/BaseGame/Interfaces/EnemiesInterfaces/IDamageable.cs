using UnityEngine;

namespace Interfaces.EnemiesInterfaces
{
    public interface IDamageable
    {
        Transform GetTransform();
        void DoDamage(int damage);
        void Heal(int healAmount);
    }
}