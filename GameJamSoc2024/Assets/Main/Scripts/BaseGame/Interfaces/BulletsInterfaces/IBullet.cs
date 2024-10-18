using UnityEngine;

namespace Interfaces.BulletsInterfaces
{
    public interface IBullet
    {
        void InitializeBullet(Transform target, int damage);
    }
}