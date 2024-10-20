using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.Interfaces.BulletsInterfaces
{
    public interface IBullet
    {
        void InitializeBullet(Transform target, int damage, Vector3 p_initPos);
    }
}