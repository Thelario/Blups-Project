using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    public class PlayerVertical : Player
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
            verticalRaw = InputManager.Instance.GetVerticalInput();

            if (verticalRaw > 0.1f) // Move up
            {
                SoundManager.Instance.PlaySound(SoundType.PlayerWalk, .5f);

                Animate(rightUp);
            }
            else if (verticalRaw < -0.1f) // Move down
            {
                SoundManager.Instance.PlaySound(SoundType.PlayerWalk, .5f);

                Animate(leftDown);
            }
            else
                Animate(idle);
        }

        protected override void Move()
        {
            rb2D.velocity = Time.fixedDeltaTime * moveSpeed * new Vector2(0f, verticalRaw).normalized;
        }

        private void Animate(string animationClip)
        {
            _animator.Play(animationClip);
        }
    }
}
