using Main.Scripts.TowerDefenseGame.Interfaces;
using Main.Scripts.TowerDefenseGame.Interfaces.EnemiesInterfaces;

namespace Main.Scripts.TowerDefenseGame.Commands
{
    public class CmdDoDamage : ICommando
    {
        private int _damage;
        private IDamageable _healthController;
        private bool _affectFlying;
        
        public CmdDoDamage(IDamageable healthController, int damage,bool affectFlying = false)
        {
         
            _healthController = healthController;
            _damage = damage;
            _affectFlying = affectFlying;
        }

        public void Execute() => _healthController.DoDamage(_damage, _affectFlying);
        public void Undo() => _healthController.Heal(_damage);
    }
}