using Main.Scripts.TowerDefenseGame.ScriptableObjects.Bullets.Attack;
using Main.Scripts.TowerDefenseGame.ScriptableObjects.Bullets.Movement;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.ScriptableObjects.Bullets
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "_main/Bullet/Data/BulletData", order = 0)]
    public class BulletData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float BulletLifeTime { get; private set; }
        [field: SerializeField] public BulletAttack BulletAttack { get; private set; }
        [field: SerializeField] public BulletMovement BulletMovement { get; private set; }
    }
}