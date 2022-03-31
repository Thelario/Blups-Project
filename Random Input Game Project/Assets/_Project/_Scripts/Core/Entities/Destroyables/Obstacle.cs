using UnityEngine;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos

namespace Game.Entities
{
    public class Obstacle : DestroyableEntity, IDirectable
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private ParticleSystem trailParticles;

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

            //_direction = GameManager.Instance.obstaclesCurrentDirection;
            //_moveDirection = Utils.GetInverseMoveDirection(_direction);
            //Rotate();

            _transform.localScale = GetRandomSize();
            _renderer.color = Utils.GetRandomColor();
            trailParticles.startColor = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 0.25f); // Código obsoleto pero que me da pereza buscar hacerlo bien :)
        }

        private void Start()
        {
            DestroyYourself(7.5f);
        }

        private void FixedUpdate()
        {
            Move();
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

        private Vector3 GetRandomSize()
        {
            float bound = Random.Range(0.5f, 2f);
            return new Vector3(bound, bound, 1f);

            /*
            return new Vector3(Random.Range(_transform.localScale.x - 0.5f, _transform.localScale.x + 0.5f), 
                               Random.Range(_transform.localScale.y - 0.5f, _transform.localScale.y + 0.5f), 0f);
            */
        }
    }
}
