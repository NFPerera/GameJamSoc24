using Main.Scripts.DevelopmentUtilities.Pools;
using Main.Scripts.TowerDefenseGame._Managers;
using Main.Scripts.TowerDefenseGame.Commands;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Main.Scripts.TowerDefenseGame.Controllers
{
    public class WaveController : MonoBehaviour
    {
        [System.Serializable]
        public class Wave
        {
            public GameObject[] enemy;
            public int numberOfEnemies;
            public float countDownBetweenEnemies;
        }

        [SerializeField] private Wave[] waves;
        private int m_nextWave;
        private int m_spawnedEnemies;
        private float m_timer;
        private bool m_isWaveActive;
        
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Button waveButton;

        private PoolGeneric<GameObject> m_pool;
        private UIManager m_ui;
        private void Awake()
        {
            m_ui = GetComponent<UIManager>();
        }

        private void Update()
        {
            if (m_nextWave < waves.Length)
            {
                if (m_nextWave == waves.Length - 1)
                {
                    SpawnFinalWave();
                }
                else if (m_isWaveActive)
                {
                    SpawnWave();
                }
                else
                {
                    m_timer = waves[m_nextWave].countDownBetweenEnemies;
                    m_spawnedEnemies = 0;
                }
            }
            else
            {
                m_ui.ActivateGameOverScreen(true);
            }
        }

        private void SpawnWave()
        {
            m_timer -= Time.deltaTime;
            
            if (m_timer <= 0)
            {
                Random l_rnd = new Random();
                //var aux = rnd.Next(0, 3);
                
                GameManager.Instance.AddEventQueue(new CmdSpawn(waves[m_nextWave].enemy[0], spawnPoint.position));
                m_timer = waves[m_nextWave].countDownBetweenEnemies;
                m_spawnedEnemies++;
            }

            if (m_spawnedEnemies > waves[m_nextWave].numberOfEnemies)
            {
                waveButton.interactable = true;
                m_nextWave++;
                m_isWaveActive = false;
            }
        }

        private void SpawnFinalWave()
        {
            m_timer -= Time.deltaTime;
            
            if (m_timer <= 0)
            {
                Random l_rnd = new Random();
                var l_aux = l_rnd.Next(0, 4);
                
                GameManager.Instance.AddEventQueue(new CmdSpawn(waves[m_nextWave].enemy[l_aux], spawnPoint.position));
                m_timer = waves[m_nextWave].countDownBetweenEnemies;
                m_spawnedEnemies++;
            }

            if (m_spawnedEnemies > waves[m_nextWave].numberOfEnemies)
            {
                waveButton.interactable = true;
                m_nextWave++;
                m_isWaveActive = false;
            }
        }
        public void ActivateWave() => m_isWaveActive = true;
    }
}