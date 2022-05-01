using UnityEngine;

namespace Game.Entities
{
    public class PlayerShield : PlayerRotation
    {
        protected override void HandleTriggerObstacle(Collider2D collision)
        {
            base.HandleTriggerObstacle(collision);


        }
    }
}
