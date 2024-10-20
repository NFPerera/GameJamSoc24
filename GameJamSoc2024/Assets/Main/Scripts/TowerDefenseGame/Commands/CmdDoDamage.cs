﻿using Main.Scripts.TowerDefenseGame.Interfaces;
using Main.Scripts.TowerDefenseGame.Interfaces.EnemiesInterfaces;

namespace Main.Scripts.TowerDefenseGame.Commands
{
    public class CmdDoDamage : ICommando
    {
        private int _damage;
        private IDamageable _healthController;
        
        public CmdDoDamage(IDamageable healthController, int damage)
        {
            _healthController = healthController;
            _damage = damage;
        }

        public void Execute() => _healthController.DoDamage(_damage);
        public void Undo() => _healthController.Heal(_damage);
    }
}