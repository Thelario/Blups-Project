using UnityEngine;
using Game.Managers;
using Game.Entities.Helpers;

namespace Game.Entities
{
    public class Coin : DestroyableEntity, IDirectable
    {
        [SerializeField] private Speed speedStats;

        private Direction _direction;
        private Vector2 _moveDirection;

        private Rigidbody2D _rb2D;

        protected override void Awake()
        {
            base.Awake();

            _rb2D = GetComponent<Rigidbody2D>();

            DifficultyManager.OnDifficultyChange += speedStats.ChangeDifficulty;
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
        }

        private void Move()
        {
            _rb2D.velocity = Time.fixedDeltaTime * speedStats.moveSpeed * _moveDirection;
        }
    }
}
