using Commands;
using UnityEngine;

namespace _Managers
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager Instance;
        [SerializeField] private GameObject areaTowerPrefab;
        [SerializeField] private GameObject rangeTowerPrefab;
        [SerializeField] private GameObject rocketTowerPrefab;

        private GameObject _towerToBuild;
        private MousePosition _mousePosition;
        private void Awake()
        {
            if(Instance != null) Destroy(this);
                Instance = this;

            _mousePosition = GameObject.FindGameObjectWithTag("Mouse").GetComponent<MousePosition>();
            GameManager.Instance.OnClick += BuildTowerInWorld;
        }
        public void SetTowerToBuild(GameObject towerToBuild) => _towerToBuild = towerToBuild;

        private void BuildTowerInWorld()
        {
            var clickPosition = _mousePosition.GetMousePosition();
            if(_towerToBuild == null) return;
            
            CmdSpawn cmdSpawn = new CmdSpawn(_towerToBuild, clickPosition);
            
            GameManager.Instance.AddEventQueue(cmdSpawn);
            GameManager.Instance.AddSellEvent(cmdSpawn);
            
            _towerToBuild = null;
        }



        public GameObject GetAreaTowerPrefab() => areaTowerPrefab;
        public GameObject GetRangeTowerPrefab() => rangeTowerPrefab;
        public GameObject GetRocketTowerPrefab() => rocketTowerPrefab;
        
        

    }
}