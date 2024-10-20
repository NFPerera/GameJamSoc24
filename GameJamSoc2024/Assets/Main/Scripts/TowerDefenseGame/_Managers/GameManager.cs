using System;
using System.Collections.Generic;
using Main.Scripts.TowerDefenseGame.Interfaces;
using Main.Scripts.TowerDefenseGame.Interfaces.EnemiesInterfaces;
using Main.Scripts.TowerDefenseGame.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.Scripts.TowerDefenseGame._Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [field: SerializeField] public int MaxLifePoints { get; private set; }
        [field: SerializeField] public int StartMoneyPoints { get; private set; }
        [field: SerializeField] public List<Transform> PathPoints { get; private set; }
        [SerializeField] private Camera mainCamera;

        private List<ICommando> m_events = new List<ICommando>();
        private Stack<ICommando> m_sellableEvents = new Stack<ICommando>();
        private List<ICommando> m_doneEvents = new List<ICommando>();
        private List<IDamageable> m_enemies = new List<IDamageable>();
        
        private int m_lifePoints;
        private int m_money;
        
        
        public Action<int> OnChangeLifePoints;
        public Action<int> OnChangeMoney;
        public Action OnClick;
        public Action OnGameOver;

        private const int MAX_UNDOS = 25;


        private UIManager m_ui;

        private void Awake()
        {
            if(Instance != null) Destroy(this);
            
            Instance = this;
            DontDestroyOnLoad(this);
            m_ui = GetComponent<UIManager>();
            
            m_lifePoints = MaxLifePoints;
            m_money = StartMoneyPoints;
            
            OnChangeLifePoints += OnLooseLifePoints;
            OnChangeMoney += ChangeMoney;
            OnGameOver += LoseGame;
        }

        
        private void Update()
        {
            foreach (var l_events in m_events)
            {
                l_events.Execute();
                m_doneEvents.Add(l_events);

                if (m_doneEvents.Count > MAX_UNDOS)
                    m_doneEvents.RemoveAt(0);
            }

            m_events.Clear();

            if (Input.GetMouseButtonDown(0))
            {
                OnClick.Invoke();
            }
            
            
            if(Input.GetKeyDown(KeyCode.N))
                SceneManager.LoadScene("LevelDesignFPS");
        }

        [SerializeField] private List<BuildingModel> allBuildings;
        public void ToggleBuildingView()
        {
            foreach (var building in allBuildings)
            {
                building.ToggleLight();
            }
        }

        #region Facade

            public void AddEventQueue(ICommando p_commando) => m_events.Add(p_commando);
            public void AddSellEvent(ICommando p_commando) => m_sellableEvents.Push(p_commando);

        #endregion
        
        #region Memento
            public void SellLastTower() => m_sellableEvents.Pop().Undo();
            public void UndoAllEventList()
            {
                if(m_doneEvents.Count <= 0) return;

                for (int l_i = 0; l_i < m_doneEvents.Count; l_i++)
                {
                    m_doneEvents[l_i].Undo();
                    m_doneEvents.RemoveAt(m_doneEvents.Count - 1);
                }
            }

        #endregion

        #region Getters
            public Stack<ICommando> GetSellableEvents() => m_sellableEvents;

            public int GetLifePoints() => m_lifePoints;
            public int GetMoney() => m_money;
            public Camera GetCamera() => mainCamera;

        #endregion
        
        #region GAME RULES;

            private void OnLooseLifePoints(int p_lifeChange)
            {
                m_lifePoints -= p_lifeChange;

                if (m_lifePoints <= 0) LoseGame();
            }

            private void ChangeMoney(int p_moneyChange)
            {
                m_money += p_moneyChange;
            }

            private void LoseGame()
            {
                m_ui.ActivateGameOverScreen(false);
            }

        #endregion
        
        
        
    }
}