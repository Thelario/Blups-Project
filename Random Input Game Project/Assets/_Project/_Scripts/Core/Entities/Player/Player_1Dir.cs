using UnityEngine;

namespace Game.Entities
{
    public class Player_1Dir : Player
    {
        [SerializeField] private float leftRightFixedPos;
        [SerializeField] private float upDownFixedPos;

        private bool _moveHorizontal = true;

        protected override void GetMoveInput()
        {
            if (_moveHorizontal)
            {
                _horizontalRaw = Input.GetAxisRaw("Horizontal");
                return;
            }

            _verticalRaw = Input.GetAxisRaw("Vertical");
        }

        protected override void Move()
        {
            if (_moveHorizontal)
            {
                _rb2D.velocity = Time.fixedDeltaTime * moveSpeed * new Vector2(_horizontalRaw, 0f);
                return;
            }

            _rb2D.velocity = Time.fixedDeltaTime * moveSpeed * new Vector2(0f, _verticalRaw);
        }

        public void SetMoveHorizontal(Direction dir)
        {
            if (dir == Direction.Left) // Obstacles appear on the left
            {
                _moveHorizontal = false;
                SetPlayerFixedPosition(leftRightFixedPos, 0f);
            }
            else if (dir == Direction.Right) // Obstacles appear on the right
            {
                _moveHorizontal = false;
                SetPlayerFixedPosition(-leftRightFixedPos, 0f);
            }
            else if (dir == Direction.Up) // Obstacles appear above
            {
                _moveHorizontal = true;
                SetPlayerFixedPosition(0f, -upDownFixedPos);
            }
            else // Obstacles appear below
            {
                _moveHorizontal = true;
                SetPlayerFixedPosition(0f, upDownFixedPos);
            }
        }

        private void SetPlayerFixedPosition(float x, float y)
        {
            _transform.position = new Vector3(x, y, 0f);
        }
    }
}
