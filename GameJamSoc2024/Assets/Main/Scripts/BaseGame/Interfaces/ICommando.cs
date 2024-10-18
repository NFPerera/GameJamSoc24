namespace Main.Scripts.BaseGame.Interfaces
{
    public interface ICommando
    {
        void Execute();
        void Undo();
    }
}