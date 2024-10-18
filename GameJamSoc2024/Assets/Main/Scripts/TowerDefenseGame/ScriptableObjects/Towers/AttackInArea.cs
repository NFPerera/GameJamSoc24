using Main.Scripts.BaseGame._Managers;
using Main.Scripts.BaseGame.Commands;
using Main.Scripts.BaseGame.Models;
using UnityEngine;

namespace Main.Scripts.BaseGame.ScriptableObjects.Towers
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