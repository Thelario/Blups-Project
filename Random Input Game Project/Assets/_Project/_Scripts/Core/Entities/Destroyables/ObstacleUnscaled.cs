using Game.Managers;
using System.Collections;
using UnityEngine;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos

namespace Game.Entities
{
    public class ObstacleUnscaled : DestroyableEntity, IDirectable
    {
        [SerializeField] private ParticleSystem trailParticles;
        [SerializeField] private Vector2 _moveDirection;

        private Direction _direction;
        private float _moveSpeed;

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

#if UNITY_STANDALONE

            _moveSpeed = 0.02f;

#endif

#if UNITY_ANDROID

            _moveSpeed = 1f;

#endif

        }

        private void Start()
        {
            StartCoroutine(nameof(DestroyYou));
        }

        private void Update()
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

        private IEnumerator DestroyYou()
        {
            yield return new WaitForSecondsRealtime(4f);
            Destroy(gameObject);
        }

        private void Move()
        {
            _transform.Translate(_moveDirection * _moveSpeed, Space.World);
        }
    }
}
