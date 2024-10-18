using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "_main/Enemy/Data", order = 0)]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public EnemyTierData[] enemiesTierDatas;
    }
}