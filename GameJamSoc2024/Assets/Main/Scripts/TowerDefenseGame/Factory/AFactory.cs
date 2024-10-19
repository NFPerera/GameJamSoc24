using Main.Scripts.TowerDefenseGame.Interfaces.FactoriesInterfaces;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.Factory
{
    public class AFactory<T> : IFactory<T> where T : Object
    {
        public T Create(T prefab) => Object.Instantiate(prefab);
    }
}