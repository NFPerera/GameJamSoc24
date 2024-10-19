using Main.Scripts.TowerDefenseGame._Managers;
using Main.Scripts.TowerDefenseGame.Commands;
using Main.Scripts.TowerDefenseGame.Models;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.ScriptableObjects.Towers
{
    [CreateAssetMenu(fileName = "AttackInArea", menuName = "_main/Tower/Attack/Area", order = 0)]
    public class AttackInArea : TowerAttack
    {
        public override void Attack(TowerModel model)
        {
            var enemiesInRange = model.GetEnemiesInRange();
            var data = model.GetData();
            
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                GameManager.Instance.AddEventQueue(new CmdDoDamage(enemiesInRange[i], data.Damage));
            }
        }
    }
}