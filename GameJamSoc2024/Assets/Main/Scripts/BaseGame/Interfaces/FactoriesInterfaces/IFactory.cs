using UnityEngine;

namespace Interfaces.FactoriesInterfaces
{
    public interface IFactory<T>
    {
        T Create(T prefab);
    }
}