using Game.Managers;
using Game.Spawnners;
using UnityEngine;

namespace Game.Entities
{
    public class Player_1Dir : Player
    {
        [SerializeField] private float leftRightFixedPos;
        [SerializeField] private float upDownFixedPos;

        private bool _moveHorizontal = true;

        protected override void Awake()
        {
            base.Awake();

            Dir1ObjectsSpawner.OnSpawnFalse += SetupPlayer;
        }

        private void OnDestroy()
        {
            Dir1ObjectsSpawner.OnSpawnFalse -= SetupPlayer;
        }

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

        private void SetupPlayer()
        {
            SetMoveHorizontal(GameManager.Instance.obstaclesCurrentDirection);
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
