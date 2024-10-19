using System;
using System.Collections.Generic;
using Main.Scripts.TowerDefenseGame.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.Scripts
{
    public class MasterManager : MonoBehaviour
    {
        public static MasterManager Instance => m_instance;
        private static MasterManager m_instance;

        public event Action OnLevelFinished;
        private void Awake()
        {
            if (m_instance != null && m_instance != this)
            {
                Destroy(this);
                return;
            }

            m_instance = this;
            
            DontDestroyOnLoad(this.gameObject);

        }

            
    }
}
