namespace Main.Scripts.TowerDefenseGame.Interfaces.FactoriesInterfaces
{
    public interface IFactory<T>
    {
        T Create(T prefab);
    }
}