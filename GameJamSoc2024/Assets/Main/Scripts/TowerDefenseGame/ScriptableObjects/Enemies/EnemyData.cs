using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.ScriptableObjects.Enemies
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "_main/Enemy/Data", order = 0)]
    public class EnemyData : ScriptableObject
    {
        
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }

        [field: SerializeField] public int MoneyReward { get; private set; }

        [field: SerializeField] public int Damage { get; private set; }

        [field: SerializeField] public bool IsFlying { get; private set; }
    }
}