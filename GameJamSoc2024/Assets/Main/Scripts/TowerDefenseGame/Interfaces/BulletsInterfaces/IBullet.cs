using UnityEngine;

namespace Main.Scripts.BaseGame.Interfaces.BulletsInterfaces
{
    public interface IBullet
    {
        void InitializeBullet(Transform target, int damage);
    }
}