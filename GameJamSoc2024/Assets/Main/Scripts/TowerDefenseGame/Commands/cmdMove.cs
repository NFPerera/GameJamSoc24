using Main.Scripts.TowerDefenseGame.Clases;
using Main.Scripts.TowerDefenseGame.Interfaces;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.Commands
{
    public class CmdMove : ICommando
    {
        private MovementController _movementController;
        private Vector3 _direction;
        
        public CmdMove(MovementController movementController, Vector3 direction)
        {
            _movementController = movementController;
            _direction = direction;
        }

        public void Execute() => _movementController.Move(_direction);
        public void Undo() => _movementController.MoveBackwards(_direction);
    }
}