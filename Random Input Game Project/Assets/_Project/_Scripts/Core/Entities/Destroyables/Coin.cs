using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    public class Coin : DestroyableEntity, IDirectable
    {
        [SerializeField] private float moveSpeed;

        private Direction _direction;
        private Vector2 _moveDirection;

        private Rigidbody2D _rb2D;

        protected override void Awake()
        {
            base.Awake();

            _rb2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            DestroyYourself(3f);
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void SetDirection(Direction direction)
        {
            _direction = direction;
            _moveDirection = Utils.GetInverseMoveDirection(_direction);
        }

        private void Move()
        {
            _rb2D.velocity = Time.fixedDeltaTime * moveSpeed * _moveDirection;
        }
    }
}
