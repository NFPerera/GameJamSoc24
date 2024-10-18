namespace Main.Scripts.BaseGame.Interfaces.FactoriesInterfaces
{
    public interface IFactory<T>
    {
        T Create(T prefab);
    }
}