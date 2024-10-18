using _Managers;
using Models;
using UnityEngine;

namespace Controllers
{
    public class ShopController : MonoBehaviour
    {
        public void OnButtonDownRangeTower()
        {
            var tower = BuildManager.Instance.GetRangeTowerPrefab();
            
            var towerCost = tower.GetComponent<TowerModel>().GetData().Cost;

            if (towerCost <= GameManager.Instance.GetMoney())
                BuyTower(tower);
        }

        public void OnButtonDownAreaTower()
        {
            var tower = BuildManager.Instance.GetAreaTowerPrefab();
            
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
            GameManager.Instance.OnChangeMoney(-tower.GetComponent<TowerModel>().GetData().Cost);
        } 
    }
}
