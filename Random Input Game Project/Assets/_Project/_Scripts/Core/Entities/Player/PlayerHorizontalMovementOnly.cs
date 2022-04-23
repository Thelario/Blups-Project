using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    public class PlayerHorizontalMovementOnly : Player
    {
        [Header("Animation's Names")]
        [SerializeField] private string idle;
        [SerializeField] private string leftDown;
        [SerializeField] private string rightUP;

        private Animator _animator;

        protected override void Awake()
        {
            base.Awake();

            _animator = GetComponent<Animator>();
        }

        protected override void GetMoveInput()
        {
            _horizontalRaw = InputManager.Instance.GetHorizontalInput();
            print(_horizontalRaw);

            if (_horizontalRaw > 0.1f) // Move right
            {
                SoundManager.Instance.PlaySound(SoundType.PlayerWalk, .5f);
                _animator.Play(rightUP);
            }
            else if (_horizontalRaw < -0.1f) // Move left
            {
                SoundManager.Instance.PlaySound(SoundType.PlayerWalk, .5f);
                _animator.Play(leftDown);
            }
            else
                _animator.Play(idle);
        }

        protected override void Move()
        {
            _rb2D.velocity = Time.fixedDeltaTime * moveSpeed * new Vector2(_horizontalRaw, 0f).normalized;
        }
    }
}
