using UnityEngine;
using Game.Entities.Helpers;
using Game.Managers;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos

namespace Game.Entities
{
    public class Obstacle : DestroyableEntity, IDirectable
    {
        [Header("References")]
        [SerializeField] private Speed speedStats;
        [SerializeField] private ParticleSystem trailParticles;
        [SerializeField] private Rigidbody2D rb2D;
        [SerializeField] private Transform thisTransform;
        [SerializeField] private SpriteRenderer spRenderer;
        [SerializeField] private Animator animator;

        private Direction _direction;
        private Vector2 _moveDirection;

        protected override void Awake()
        {
            base.Awake();

            SetupObstacle();
        }

        private void Start()
        {
            if (speedStats.changeSpeedAccordingToDifficulty)
                speedStats.ChangeDifficulty();

            DestroyYourself(3f);
        }

        private void FixedUpdate()
        {
            Move();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            DifficultyManager.OnDifficultyChange -= speedStats.ChangeDifficulty;
        }

        public void SetDirection(Direction direction)
        {
            _direction = direction;
            _moveDirection = Utils.GetInverseMoveDirection(_direction);

            Rotate();
        }

        private void SetupObstacle()
        {
            spRenderer.color = ColorPalettesManager.Instance.GetRandomColor();
            trailParticles.startColor = new Color(spRenderer.color.r, spRenderer.color.g, spRenderer.color.b, 0.25f);

            DifficultyManager.OnDifficultyChange += speedStats.ChangeDifficulty;
        }

        private void Rotate()
        {
            Vector3 direction = (thisTransform.position + new Vector3(_moveDirection.x, _moveDirection.y)) - thisTransform.position;
            thisTransform.up = direction.normalized;
        }

        public override void DestroyYourself()
        {
            animator.enabled = false;
            LeanTween.scale(gameObject, Vector3.zero, 0.5f);
            DestroyYourself(0.5f);
        }

        private void Move()
        {
            rb2D.velocity = Time.fixedDeltaTime * speedStats.moveSpeed * _moveDirection;
        }
    }
}
