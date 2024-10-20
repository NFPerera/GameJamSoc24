using System.Collections.Generic;
using Main.Scripts.TowerDefenseGame.Interfaces.EnemiesInterfaces;
using Main.Scripts.TowerDefenseGame.Models;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.ScriptableObjects.Towers
{
    [CreateAssetMenu(fileName = "AttackInPoint", menuName = "_main/Tower/Attack/Range", order = 0)]
    public class AttackInPoint : TowerAttack
    {
        private List<IDamageable> _totalEnemiesInRange = new List<IDamageable>();
        public override void Attack(TowerModel model)
        {
            _totalEnemiesInRange = model.GetEnemiesInRange();
            
            if(_totalEnemiesInRange == null || _totalEnemiesInRange.Count <= 0) return;
            
            var firstEnemyInRange = _totalEnemiesInRange[0];
            var data = model.GetData();
            
            var bullet = Instantiate(data.BulletPrefabs, model.GetShootPoint().position, Quaternion.identity);
            
            bullet.InitializeBullet(firstEnemyInRange.GetTransform(), data.Damage, model.transform.position);
        }
    }
}