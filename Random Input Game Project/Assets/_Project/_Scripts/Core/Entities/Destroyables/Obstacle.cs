using Game.Managers;
using UnityEngine;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos

namespace Game.Entities
{
    public class Obstacle : DestroyableEntity, IDirectable
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private ParticleSystem trailParticles;
        [SerializeField] private bool changeSpeedAccordingToDifficulty = false;

        private Direction _direction;
        private Vector2 _moveDirection;

        private Rigidbody2D _rb2D;
        private Transform _transform;
        private SpriteRenderer _renderer;
        private Animator _animator;

        protected override void Awake()
        {
            base.Awake();

            _rb2D = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
            _renderer = _transform.GetChild(0).GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            _renderer.color = ColorPalettesManager.Instance.GetRandomColor();
            trailParticles.startColor = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 0.25f); // Código obsoleto pero que me da pereza buscar hacerlo bien :)
        }

        private void Start()
        {
            SoundManager.Instance.PlaySound(SoundType.Slash, Random.Range(0.05f, 0.15f));
            
            if (changeSpeedAccordingToDifficulty)
                ChangeDifficulty();

            DestroyYourself(3f);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnLevelWasLoaded(int level)
        {
            if (level == 1)
                moveSpeed = 350f;
        }

        public void ChangeDifficulty()
        {
            switch (DifficultyManager.Instance.currentDifficulty)
            {
                case Difficulty.Easy:
                    moveSpeed = 350f;
                    break;
                case Difficulty.Medium:
                    moveSpeed = 450f;
                    break;
                case Difficulty.Hard:
                    moveSpeed = 550f;
                    break;
            }
        }

        public void SetDirection(Direction direction)
        {
            _direction = direction;
            _moveDirection = Utils.GetInverseMoveDirection(_direction);
            Rotate();
        }

        private void Rotate()
        {
            Vector3 direction = (_transform.position + new Vector3(_moveDirection.x, _moveDirection.y)) - _transform.position;
            _transform.up = direction.normalized;
        }

        public override void DestroyYourself()
        {
            _animator.enabled = false;
            LeanTween.scale(gameObject, Vector3.zero, 0.5f);
            DestroyYourself(0.5f);
        }

        private void Move()
        {
            _rb2D.velocity = Time.fixedDeltaTime * moveSpeed * _moveDirection;
        }
    }
}
