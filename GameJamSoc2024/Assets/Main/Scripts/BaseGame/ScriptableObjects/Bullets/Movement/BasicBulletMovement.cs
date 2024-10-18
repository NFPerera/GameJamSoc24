using _Managers;
using Commands;
using Models;
using UnityEngine;

namespace Bullets.Movement
{
    [CreateAssetMenu(fileName = "BasicBulletMovement", menuName = "_main/Bullet/Data/Movement/BasicBulletMovement", order = 0)]
    public class BasicBulletMovement : BulletMovement
    {
        public override void Move(BulletModel model)
        {
            Vector3 targetPosition = model.GetTargetTransform().position;
            Vector3 modelPosition = model.transform.position;
            var direction = (targetPosition - modelPosition).normalized;
            
            var cmdMove = new CmdMove(model.GetMovementController(), direction);
            
            GameManager.Instance.AddEventQueue(cmdMove);
        }
    }
}