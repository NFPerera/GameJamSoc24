using Main.Scripts.TowerDefenseGame._Managers;
using Main.Scripts.TowerDefenseGame.Models;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.Controllers
{
    public class ShopController : MonoBehaviour
    {
        public void OnButtonDownGatlingTower()
        {
            var tower = BuildManager.Instance.GetGatlingTowerPrefab();
            
            var towerCost = tower.GetComponent<TowerModel>().GetData().Cost;

            if (towerCost <= GameManager.Instance.GetMoney())
                BuyTower(tower);
        }

        public void OnButtonDownMortarTower()
        {
            var tower = BuildManager.Instance.GetMortarTowerPrefab();
            
            var towerCost = tower.GetComponent<TowerModel>().GetData().Cost;

            if (towerCost <= GameManager.Instance.GetMoney())
                BuyTower(tower);
        }
        
        public void OnButtonDownRocketTower()
        {
            var tower = BuildManager.Instance.GetRocketTowerPrefab();
            
            var towerCost = tower.GetComponent<TowerModel>().GetData().Cost;

            if (towerCost <= GameManager.Instance.GetMoney())
                BuyTower(tower);
        }

        private void BuyTower(GameObject tower)
        {
            BuildManager.Instance.SetTowerToBuild(tower);
            GameManager.Instance.ToggleBuildingView();
            
        } 
    }
}
