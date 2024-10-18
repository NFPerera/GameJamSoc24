using _Managers;
using Commands;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Controllers
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
        private int _nextWave;
        private int _spawnedEnemies;
        private float _timer;
        private bool _isWaveActive;
        
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Button waveButton;


        private UIManager _ui;
        private void Awake()
        {
            _ui = GetComponent<UIManager>();
        }

        private void Update()
        {
            if (_nextWave < waves.Length)
            {
                if (_nextWave == waves.Length - 1)
                {
                    SpawnFinalWave();
                }
                else if (_isWaveActive)
                {
                    SpawnWave();
                }
                else
                {
                    _timer = waves[_nextWave].countDownBetweenEnemies;
                    _spawnedEnemies = 0;
                }
            }
            else
            {
                _ui.ActivateGameOverScreen(true);
            }
        }

        private void SpawnWave()
        {
            _timer -= Time.deltaTime;
            
            if (_timer <= 0)
            {
                Random rnd = new Random();
                var aux = rnd.Next(0, 3);
                
                GameManager.Instance.AddEventQueue(new CmdSpawn(waves[_nextWave].enemy[aux], spawnPoint.position));
                _timer = waves[_nextWave].countDownBetweenEnemies;
                _spawnedEnemies++;
            }

            if (_spawnedEnemies > waves[_nextWave].numberOfEnemies)
            {
                waveButton.interactable = true;
                _nextWave++;
                _isWaveActive = false;
            }
        }

        private void SpawnFinalWave()
        {
            _timer -= Time.deltaTime;
            
            if (_timer <= 0)
            {
                Random rnd = new Random();
                var aux = rnd.Next(0, 4);
                
                GameManager.Instance.AddEventQueue(new CmdSpawn(waves[_nextWave].enemy[aux], spawnPoint.position));
                _timer = waves[_nextWave].countDownBetweenEnemies;
                _spawnedEnemies++;
            }

            if (_spawnedEnemies > waves[_nextWave].numberOfEnemies)
            {
                waveButton.interactable = true;
                _nextWave++;
                _isWaveActive = false;
            }
        }
        public void ActivateWave() => _isWaveActive = true;
    }
}