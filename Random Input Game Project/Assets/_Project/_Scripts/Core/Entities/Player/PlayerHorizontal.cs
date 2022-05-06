using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    public class PlayerHorizontal : Player
    {
        [Header("Speed")] 
        [SerializeField] private float moveSpeed;
        
        [Header("Animation's Names")]
        [SerializeField] private string idle;
        [SerializeField] private string leftDown;
        [SerializeField] private string rightUp;

        private Animator _animator;

        protected override void Awake()
        {
            base.Awake();

            _animator = GetComponent<Animator>();
        }

        protected override void GetMoveInput()
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

        protected override void Move()
        {
            rb2D.velocity = Time.fixedDeltaTime * moveSpeed * new Vector2(horizontalRaw, 0f).normalized;
        }
    }
}
