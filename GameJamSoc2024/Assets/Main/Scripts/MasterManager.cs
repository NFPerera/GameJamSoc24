using System;
using System.Collections.Generic;
using Main.Scripts.TowerDefenseGame.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.Scripts
{
    public class MasterManager : MonoBehaviour
    {
        private class PlayerData
        {
            public string PlayersName;
            //public PlayerModel Model;
            //public List<NetworkObject> PlayersObj = new List<NetworkObject>();
        }
        
        
        //[SerializeField] private List<NetworkObject> SpawnableNetworkObjects = new List<NetworkObject>();
        //public List<NetworkObject> networkObjects = new List<NetworkObject>();
        public static MasterManager Instance => m_instance;
        private static MasterManager m_instance;

        
        private const int MaxUndos = 25;
        private void Awake()
        {
            if (m_instance != null && m_instance != this)
            {
                Destroy(this);
                return;
            }

            m_instance = this;
            
            DontDestroyOnLoad(this.gameObject);

            m_lifePoints = MaxLifePoints;
        }

        public void ChangeNetScene(string pSceneName) => SceneManager.LoadScene(pSceneName, LoadSceneMode.Single);
        

       
        
        

        


        

        
        
       
        
        

        #region Main Menu

            [System.Serializable]
            public class RoomData
            {
                public int RoomId;
                public string Name;
            }
            
            private Dictionary<ulong, RoomData> m_roomDatas = new Dictionary<ulong, RoomData>();
            private int m_playersCount;
            
            

            
            

            public RoomData GetRoomData(ulong id) => m_roomDatas[id];
        
        #endregion

        #region Level
       
        
            [field: SerializeField] public int MaxLifePoints { get; private set; }
            [SerializeField] private String victorySceneName;
            [SerializeField] private String defeatSceneName;
            
            private int m_lifePoints;
            
            private WaveController m_waveController;
            
            //public Action<int> OnChangeLifePoints;
            public void SetWaveController(WaveController waveController)
            {
                m_waveController = waveController;
                //m_waveController.OnFinishWave += RequestEnableWaveButtonsClientRpc;
            }
            
            public void RequestActivateWaveServerRpc() 
            {
                m_waveController.ActivateWave();
                RequestUnableWaveButtonsClientRpc();
            }
        
            public void RequestUnableWaveButtonsClientRpc()
            {
                //GameManager.Instance.ToggleWaveButton(false);
            }
            
            public void RequestEnableWaveButtonsClientRpc()
            {
                //GameManager.Instance.ToggleWaveButton(true);
            }

            /*
            public void RequestChangeMoneyServerRpc(ulong affectedPlayer, int moneyChange)
            {
                var model = m_playerDic[affectedPlayer].Model;

                var diff = model.GetMoney() + moneyChange;
                m_playerDic[affectedPlayer].Model.RequestChangeMoneyClientRpc(diff);
            }*/
            
            public void RequestLooseLifePoints(int lifeChange)
            {
                m_lifePoints -= lifeChange;
                LooseLifePointsClientRpc(lifeChange);
                
                if (m_lifePoints <= 0)
                    SceneManager.LoadScene(defeatSceneName, LoadSceneMode.Single);
            }

            public Action<int> OnChangeLifePoints;

            private void LooseLifePointsClientRpc(int lifeChange)
            {
                
                m_lifePoints -= lifeChange;
                OnChangeLifePoints.Invoke(m_lifePoints);
            }

            public void RequestLoadWinSceneServerRpc()
            {
                SceneManager.LoadScene(victorySceneName, LoadSceneMode.Single);
            }
            /*
            public void RequestBuyTowerServerRpc(ulong buyerId,int towerId, int towerCost)
            {
                if (m_playerDic[buyerId].Model.GetMoney() > towerCost)
                {
                    m_playerDic[buyerId].Model.SetTowerToBuildClientRpc(towerId);
                }
            }*/

             
            //public int GetPlayersCurrMoney(ulong playersId) => m_playerDic[playersId].Model.GetMoney();
            public int GetLifePoints() => m_lifePoints;
            
            

            #endregion
            
    }
}
