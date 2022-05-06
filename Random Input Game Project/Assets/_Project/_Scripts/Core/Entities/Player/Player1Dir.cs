using Game.Managers;
using Game.Spawnners;
using UnityEngine;

namespace Game.Entities
{
    public class Player1Dir : Player
    {
        [Header("Speed")] 
        [SerializeField] private float moveSpeed;
        
        [Header("Fixed Positions")]
        [SerializeField] private float leftRightFixedPos;
        [SerializeField] private float upDownFixedPos;

        [Header("Animation's Names")]
        [SerializeField] private string idle;
        [SerializeField] private string leftDown;
        [SerializeField] private string rightUp;

        private bool _moveHorizontal = true;

        private Animator _animator;

        protected override void Awake()
        {
            base.Awake();

            _animator = GetComponent<Animator>();

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
                horizontalRaw = InputManager.Instance.GetHorizontalInput();

                if (horizontalRaw > 0.1f) // Move right
                {
                    SoundManager.Instance.PlaySound(SoundType.PlayerWalk, .5f);
                    _animator.Play(rightUp);
                }
                else if (horizontalRaw < -0.1f) // Move left
                {
                    SoundManager.Instance.PlaySound(SoundType.PlayerWalk, .5f);
                    _animator.Play(leftDown);
                }
                else
                    _animator.Play(idle);
            }
            else
            {
                verticalRaw = InputManager.Instance.GetVerticalInput();

                if (verticalRaw > 0.1f) // Move up
                {
                    SoundManager.Instance.PlaySound(SoundType.PlayerWalk, .5f);
                    _animator.Play(rightUp);
                }
                else if (verticalRaw < -0.1f) // Move down
                {
                    SoundManager.Instance.PlaySound(SoundType.PlayerWalk, .5f);
                    _animator.Play(leftDown);
                }
                else
                    _animator.Play(idle);
            }
        }

        protected override void Move()
        {
            if (_moveHorizontal)
            {
                rb2D.velocity = Time.fixedDeltaTime * moveSpeed * new Vector2(horizontalRaw, 0f).normalized;
                return;
            }

            rb2D.velocity = Time.fixedDeltaTime * moveSpeed * new Vector2(0f, verticalRaw).normalized;
        }

        private void SetupPlayer()
        {
            SetMoveHorizontal(GameManager.Instance.obstaclesCurrentDirection);
        }

        private void SetMoveHorizontal(Direction dir)
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
            thisTransform.position = new Vector3(x, y, 0f);
        }
    }
}
