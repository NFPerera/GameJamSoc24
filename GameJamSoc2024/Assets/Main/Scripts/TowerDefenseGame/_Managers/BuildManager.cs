using System;
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
        [SerializeField] private LayerMask buildingMask;
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
            
            if (!Physics.Raycast(l_ray, out var l_raycastHit, Mathf.Infinity, buildingMask)){
                return;}


            if (!l_raycastHit.transform.TryGetComponent(out BuildingModel l_buildingModel)){ 
                return;}
            
            if(!l_buildingModel.IsAvailable)
                return;
            
            l_buildingModel.Construct();
            CmdSpawn l_cmdSpawn = new CmdSpawn(_towerToBuild, l_buildingModel.GetConstructionPos());

            var ins = GameManager.Instance;
            ins.AddEventQueue(l_cmdSpawn);
            ins.AddSellEvent(l_cmdSpawn);
            ins.ToggleBuildingView();
            var tower = _towerToBuild.GetComponent<TowerModel>();
            GameManager.Instance.OnChangeMoney(-tower.GetData().Cost);
            _towerToBuild = null;
        }



        public GameObject GetMortarTowerPrefab() => mortarTowerPrefab;
        public GameObject GetGatlingTowerPrefab() => gatlingTowerPrefab;
        public GameObject GetRocketTowerPrefab() => rocketTowerPrefab;
        
        

    }
}