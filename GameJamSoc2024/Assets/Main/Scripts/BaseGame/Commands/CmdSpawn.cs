﻿using Main.Scripts.BaseGame._Managers;
using Main.Scripts.BaseGame.Factory;
using Main.Scripts.BaseGame.Interfaces;
using Main.Scripts.BaseGame.Models;
using UnityEngine;

namespace Main.Scripts.BaseGame.Commands
{
    public class CmdSpawn : ICommando
    {
        private GameObject _prefab;
        private GameObject _instance;
        private Vector3 _position;

        private AFactory<GameObject> _GameObjectFactory = new AFactory<GameObject>();

        public CmdSpawn(GameObject prefab, Vector3 spawnPosition)
        {
            _prefab = prefab;
            _position = spawnPosition;
        }
        
        public void Execute()
        {
            _instance = _GameObjectFactory.Create(_prefab);
            _instance.transform.position = _position;
        }
        public void Undo()
        {
            if (_instance.TryGetComponent(out TowerModel towerModel))
            {
                GameManager.Instance.OnChangeMoney(towerModel.GetData().Cost);
            }
            
            Object.Destroy(_instance); 
        }
    }
}