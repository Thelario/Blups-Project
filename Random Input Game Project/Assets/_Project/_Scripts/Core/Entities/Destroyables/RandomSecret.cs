using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    public class RandomSecret : DestroyableEntity, IDirectable
    {
        [Header("Fields")]
        [SerializeField] private float moveSpeed;

        [Header("References")]
        [SerializeField] private Rigidbody2D rb2D;
        
        private Direction _direction;
        private Vector2 _moveDirection;
        
        private void Start()
        {
            DestroyYourself(4f);
        }

        private void FixedUpdate()
        {
            Move();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                SoundManager.Instance.PlaySound(SoundType.PlayerSuccess);
                SecretsManager.Instance.UnlockRandomSecret();
                DestroyYourself(0f);
            }
        }

        public void SetDirection(Direction direction)
        {
            _direction = direction;
            _moveDirection = Utils.GetInverseMoveDirection(_direction);
        }

        private void Move()
        {
            rb2D.velocity = Time.fixedDeltaTime * moveSpeed * _moveDirection;
        }
    }
}