using UnityEngine;

namespace Game.Entities
{
    public class Player_4Dir : Player
    {
        protected override void GetMoveInput()
        {
            _horizontalRaw = Input.GetAxisRaw("Horizontal");
            _verticalRaw = Input.GetAxisRaw("Vertical");
        }

        protected override void Move()
        {
            _rb2D.velocity = Time.fixedDeltaTime * moveSpeed * new Vector2(_horizontalRaw, _verticalRaw);
        }
    }
}
