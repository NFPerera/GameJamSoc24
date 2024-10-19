using System.Collections.Generic;
using Main.Scripts.TowerDefenseGame.Models;
using UnityEngine;

namespace Main.Scripts.TowerDefenseGame.ScriptableObjects.Bullets.Movement
{
    [CreateAssetMenu(fileName = "BasicBulletMovement", menuName = "_main/Bullet/Data/Movement/BasicBulletMovement", order = 0)]
    public class BasicBulletMovement : BulletMovement
    {
        

        public override void Move(BulletModel p_model)
        {
            Vector3 l_targetPosition = p_model.GetTargetPos();
            Vector3 l_modelPosition = p_model.transform.position;
            var l_direction = (l_targetPosition - l_modelPosition).normalized;
            
            p_model.Move(l_direction, p_model.GetData().Speed);
        }
        
    }
}