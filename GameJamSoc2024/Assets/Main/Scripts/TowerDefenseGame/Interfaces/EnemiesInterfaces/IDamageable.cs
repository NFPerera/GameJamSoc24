using Main.Scripts.TowerDefenseGame.Models;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.Interfaces.EnemiesInterfaces
{
    public interface IDamageable
    {
        Transform GetTransform();
        void DoDamage(int damage, bool affectFlying = false);
        void Heal(int healAmount);
        
        event System.Action<IDamageable> OnDeath;
    }
}