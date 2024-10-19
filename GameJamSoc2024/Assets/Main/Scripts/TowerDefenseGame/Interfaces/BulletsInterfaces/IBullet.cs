using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.Interfaces.BulletsInterfaces
{
    public interface IBullet
    {
        void InitializeBullet(Transform target, int damage, float p_lifeTime, Vector3 p_initPos);
    }
}