using System;
using System.Collections.Generic;
using Main.Scripts.DevelopmentUtilities.Pools;
using Main.Scripts.TowerDefenseGame._Managers;
using Main.Scripts.TowerDefenseGame.Commands;
using Main.Scripts.TowerDefenseGame.Interfaces.EnemiesInterfaces;
using Main.Scripts.TowerDefenseGame.Models;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Main.Scripts.TowerDefenseGame.Controllers
{
    public class WaveController : MonoBehaviour
    {
        [System.Serializable]
        public class Level
        {
            public Wave[] Waves;
        }
        [System.Serializable]
        public class Wave
        {
            public GameObject[] enemy;
            public int numberOfEnemies;
            public float countDownBetweenEnemies;
        }

        [SerializeField] private Level[] levels;
        private int m_levelId;
        private Level m_currLevel;
        private int m_nextWave;
        private int m_spawnedEnemies;
        private float m_timer;
        private bool m_isWaveActive;
        private bool m_isLevelFinished;
        
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Button waveButton;

        private PoolGeneric<GameObject> m_pool;
        private UIManager m_ui;
        
        public event Action OnLevelFinished;
        private void Awake()
        {
            m_isLevelFinished = false;
            m_ui = GetComponent<UIManager>();
            m_levelId = 0;
            m_currLevel = levels[m_levelId];
        }

        private void Update()
        {
            if(m_levelId > levels.Length)
                return;
            
            
            if (m_nextWave < m_currLevel.Waves.Length)
            {
                if (m_nextWave == m_currLevel.Waves.Length - 1)
                {
                    SpawnFinalLevelWave();
                }
                else if (m_isWaveActive)
                {
                    SpawnWave();
                }
                else
                {
                    m_timer = m_currLevel.Waves[m_nextWave].countDownBetweenEnemies;
                    m_spawnedEnemies = 0;
                }
            }
            else
            {
                //Final cut and load scene
                m_ui.ActivateGameOverScreen(true);
            }
            
            
            if(m_allEnemies.Count <= 0 && !m_isWaveActive && m_isLevelFinished)
                FinishLevel();
        }

        private void FinishLevel()
        {
            Debug.Log($"TERMINASTE EL NIVEL {m_levelId}");
            if (m_levelId < levels.Length - 1)
            {
                m_levelId++;
                m_currLevel = levels[m_levelId];
                m_nextWave = 0;
                OnLevelFinished?.Invoke();
            }
            else
            {
                //Final cut and load scene
            }
                
        }

        private List<IDamageable> m_allEnemies = new List<IDamageable>();
        private void SpawnWave()
        {
            m_isLevelFinished = false;
            m_timer -= Time.deltaTime;
            
            if (m_timer <= 0)
            {
                var enemie = Instantiate(m_currLevel.Waves[m_nextWave].enemy[0], spawnPoint.position,
                    Quaternion.identity);
                var model = enemie.GetComponent<EnemyModel>();
                
                model.OnDeath += OnEnemyOnDeath;
                m_allEnemies.Add(model);
                //GameManager.Instance.AddEventQueue(new CmdSpawn(m_currLevel.Waves[m_nextWave].enemy[0], spawnPoint.position));
                m_timer = m_currLevel.Waves[m_nextWave].countDownBetweenEnemies;
                m_spawnedEnemies++;
            }

            if (m_spawnedEnemies > m_currLevel.Waves[m_nextWave].numberOfEnemies)
            {
                waveButton.interactable = true;
                m_nextWave++;
                m_isWaveActive = false;
            }
        }

        private void OnEnemyOnDeath(IDamageable p_obj)
        {
            m_allEnemies.Remove(p_obj);
        }

        private void SpawnFinalLevelWave()
        {
            Debug.Log($"Final wave");
            m_timer -= Time.deltaTime;
            
            if (m_timer <= 0)
            {
                var enemie = Instantiate(m_currLevel.Waves[m_nextWave].enemy[0], spawnPoint.position,
                    Quaternion.identity);
                var model = enemie.GetComponent<EnemyModel>();
                
                model.OnDeath += OnEnemyOnDeath;
                m_allEnemies.Add(model);
                
                //GameManager.Instance.AddEventQueue(new CmdSpawn(m_currLevel.Waves[m_nextWave].enemy[0], spawnPoint.position));
                m_timer = m_currLevel.Waves[m_nextWave].countDownBetweenEnemies;
                m_spawnedEnemies++;
            }

            if (m_spawnedEnemies > m_currLevel.Waves[m_nextWave].numberOfEnemies)
            {
                waveButton.interactable = true;
                m_nextWave++;
                m_isWaveActive = false;
                m_isLevelFinished = true;
            }
        }

        public void ActivateWave() => m_isWaveActive = true;
    }
}