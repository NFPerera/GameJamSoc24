using System.Collections.Generic;
using Interfaces.EnemiesInterfaces;
using Models;
using UnityEngine;

namespace Towers
{
    [CreateAssetMenu(fileName = "AttackInRange", menuName = "_main/Tower/Attack/Range", order = 0)]
    public class AttackInRange : TowerAttack
    {
        private List<IDamageable> _totalEnemiesInRange = new List<IDamageable>();
        public override void Attack(TowerModel model)
        {
            _totalEnemiesInRange = model.GetEnemiesInRange();
            
            if(_totalEnemiesInRange == null || _totalEnemiesInRange.Count <= 0) return;

            ChangeAimAngle(model);
            
            var firstEnemyInRange = _totalEnemiesInRange[0];
            var data = model.GetData();
            
            var bullet = Instantiate(data.BulletPrefabs, model.GetShootPoint().position, Quaternion.identity);
            
            bullet.InitializeBullet(firstEnemyInRange.GetTransform(), data.Damage);
        }


        private void ChangeAimAngle(TowerModel model)
        {
            Vector3 enemyPosition = _totalEnemiesInRange[0].GetTransform().position;

            Vector3 aimDirection= (enemyPosition - model.transform.position).normalized;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

            model.transform.eulerAngles = new Vector3(0, 0, aimAngle);
        }
    }
}