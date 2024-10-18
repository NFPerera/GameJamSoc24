using Bullets.Attack;
using Bullets.Movement;
using UnityEngine;

namespace Bullets
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "_main/Bullet/Data/BulletData", order = 0)]
    public class BulletData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public BulletAttack BulletAttack { get; private set; }
        [field: SerializeField] public BulletMovement BulletMovement { get; private set; }
    }
}