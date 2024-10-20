﻿using Main.Scripts.TowerDefenseGame._Managers;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame
{
    public class MousePosition : MonoBehaviour
    {
        private Vector3 _mousePosition;
        void Update()
        {
            _mousePosition = GameManager.Instance.GetCamera().ScreenToWorldPoint(Input.mousePosition);
            _mousePosition.z = 0f;
            transform.position = _mousePosition;
        }


        public Vector3 GetMousePosition()
        {
            _mousePosition.z = 0f;
            
            return _mousePosition;
        } 
    }
}