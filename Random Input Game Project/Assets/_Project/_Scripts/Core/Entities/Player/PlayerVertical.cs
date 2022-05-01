using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    public class PlayerVertical : Player
    {
        [Header("Animation's Names")]
        [SerializeField] private string idle;
        [SerializeField] private string leftDown;
        [SerializeField] private string rightUP;
        [SerializeField] private bool animateWithThisScript = true;

        private Animator _animator;

        protected override void Awake()
        {
            base.Awake();

            _animator = GetComponent<Animator>();
        }

        protected override void GetMoveInput()
        {
            _verticalRaw = InputManager.Instance.GetVerticalInput();

            if (_verticalRaw > 0.1f) // Move up
            {
                SoundManager.Instance.PlaySound(SoundType.PlayerWalk, .5f);

                Animate(rightUP);
            }
            else if (_verticalRaw < -0.1f) // Move down
            {
                SoundManager.Instance.PlaySound(SoundType.PlayerWalk, .5f);

                Animate(leftDown);
            }
            else
                Animate(idle);
        }

        protected override void Move()
        {
            _rb2D.velocity = Time.fixedDeltaTime * moveSpeed * new Vector2(0f, _verticalRaw).normalized;
        }

        private void Animate(string animation)
        {
            if (animateWithThisScript)
                _animator.Play(animation);
        }
    }
}
