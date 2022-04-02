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
        }

        private void Move()
        {
            _rb2D.velocity = Time.fixedDeltaTime * moveSpeed * _moveDirection;
        }
    }
}
