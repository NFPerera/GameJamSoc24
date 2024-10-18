using System;
using System.Collections.Generic;
using Interfaces;
using Interfaces.EnemiesInterfaces;
using UnityEngine;

namespace _Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [field: SerializeField] public int MaxLifePoints { get; private set; }
        [field: SerializeField] public int StartMoneyPoints { get; private set; }
        [field: SerializeField] public List<Transform> PathPoints { get; private set; }
        [SerializeField] private Camera mainCamera;

        private List<ICommando> _events = new List<ICommando>();
        private Stack<ICommando> _sellableEvents = new Stack<ICommando>();
        private List<ICommando> _doneEvents = new List<ICommando>();
        private List<IDamageable> _enemies = new List<IDamageable>();
        private int _lifePoints;
        private int _money;
        
        
        public Action<int> OnChangeLifePoints;
        public Action<int> OnChangeMoney;
        public Action OnClick;
        public Action OnGameOver;

        private const int MaxUndos = 25;


        private UIManager _ui;

        
        private void Awake()
        {
            if(Instance != null) Destroy(this);
            Instance = this;

            _ui = GetComponent<UIManager>();
            
            _lifePoints = MaxLifePoints;
            _money = StartMoneyPoints;
            
            OnChangeLifePoints += OnLooseLifePoints;
            OnChangeMoney += ChangeMoney;
            OnGameOver += LoseGame;
        }

        
        private void Update()
        {
            foreach (var events in _events)
            {
                events.Execute();
                _doneEvents.Add(events);

                if (_doneEvents.Count > MaxUndos)
                    _doneEvents.RemoveAt(0);
            }

            _events.Clear();

            if (Input.GetMouseButtonDown(0))
            {
                OnClick.Invoke();
            }
        }

        #region Facade

            public void AddEventQueue(ICommando commando) => _events.Add(commando);
            public void AddSellEvent(ICommando commando) => _sellableEvents.Push(commando);

        #endregion
        
        #region Memento
            public void SellLastTower() => _sellableEvents.Pop().Undo();
            public void UndoAllEventList()
            {
                if(_doneEvents.Count <= 0) return;

                for (int i = 0; i < _doneEvents.Count; i++)
                {
                    _doneEvents[i].Undo();
                    _doneEvents.RemoveAt(_doneEvents.Count - 1);
                }
            }

        #endregion

        #region Getters
            public Stack<ICommando> GetSellableEvents() => _sellableEvents;

            public int GetLifePoints() => _lifePoints;
            public int GetMoney() => _money;
            public Camera GetCamera() => mainCamera;

        #endregion
        
        #region GAME RULES;

            private void OnLooseLifePoints(int lifeChange)
            {
                _lifePoints -= lifeChange;

                if (_lifePoints <= 0) LoseGame();
            }

            private void ChangeMoney(int moneyChange)
            {
                _money += moneyChange;
            }

            private void LoseGame()
            {
                _ui.ActivateGameOverScreen(false);
            }

        #endregion
        
        
        
    }
}