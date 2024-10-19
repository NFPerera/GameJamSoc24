using Main.Scripts.TowerDefenseGame.Commands;
using Main.Scripts.TowerDefenseGame.Models;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame._Managers
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager Instance;
        [SerializeField] private GameObject mortarTowerPrefab;
        [SerializeField] private GameObject gatlingTowerPrefab;
        [SerializeField] private GameObject rocketTowerPrefab;

        private GameObject _towerToBuild;
        private MousePosition _mousePosition;

        private Camera m_camera;
        private void Start()
        {
            if(Instance != null) Destroy(this);
                Instance = this;

            _mousePosition = GameObject.FindGameObjectWithTag("Mouse").GetComponent<MousePosition>();
            m_camera = Camera.main;
            GameManager.Instance.OnClick += BuildTowerInWorld;
        }
        public void SetTowerToBuild(GameObject towerToBuild) => _towerToBuild = towerToBuild;

        private void BuildTowerInWorld()
        {
            var l_ray = m_camera.ScreenPointToRay(Input.mousePosition);
            
            
            if(_towerToBuild == null) return;
            
            if (!Physics.Raycast(l_ray, out var l_raycastHit))
                return;

            if (!l_raycastHit.transform.TryGetComponent(out BuildingModel l_buildingModel)) 
                return;
            
            
            CmdSpawn l_cmdSpawn = new CmdSpawn(_towerToBuild, l_buildingModel.GetConstructionPos());
                
            GameManager.Instance.AddEventQueue(l_cmdSpawn);
            GameManager.Instance.AddSellEvent(l_cmdSpawn);
                
            _towerToBuild = null;
        }



        public GameObject GetMortarTowerPrefab() => mortarTowerPrefab;
        public GameObject GetGatlingTowerPrefab() => gatlingTowerPrefab;
        public GameObject GetRocketTowerPrefab() => rocketTowerPrefab;
        
        

    }
}