using System;
using Models;
using UnityEngine;

namespace Towers
{
    public enum AttackSpeed
    {
        Slow,
        Medium,
        Fast,
        UltraFast
    };
    
    [CreateAssetMenu(fileName = "TowerData", menuName = "_main/Tower/Data/Data", order = 0)]
    public class TowerData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public TowerAttack TowerAttack { get; private set; }
        [field: SerializeField] public BulletModel BulletPrefabs { get; private set; }
        [SerializeField] private AttackSpeed Speed;
        [TextArea] [SerializeField] private String description;

        public String Description => description;

        public float AttackSpeed
        {
            get
            {
                switch (Speed)
                {
                    case Towers.AttackSpeed.Slow:
                        return 2f;
                    case Towers.AttackSpeed.Medium:
                        return 1f;
                    case Towers.AttackSpeed.Fast:
                        return 0.5f;
                    case Towers.AttackSpeed.UltraFast:
                        return 0.1f;
                    
                    default:
                        return 0f;
                    
                }    
            }
        }
    }
}