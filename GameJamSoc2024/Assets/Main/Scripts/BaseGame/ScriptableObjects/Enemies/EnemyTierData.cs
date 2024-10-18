using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "EnemyTierData", menuName = "_main/Enemy/TierData", order = 0)]
    public class EnemyTierData : ScriptableObject
    {
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}