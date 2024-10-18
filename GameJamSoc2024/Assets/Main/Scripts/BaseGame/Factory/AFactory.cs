using Interfaces.FactoriesInterfaces;
using UnityEngine;

namespace Factory
{
    public class AFactory<T> : IFactory<T> where T : Object
    {
        public T Create(T prefab) => Object.Instantiate(prefab);
    }
}