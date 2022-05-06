using Game.Entities.Helpers;
using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    public class CoinMovesCenter : DestroyableEntity
    {
        [SerializeField] private Speed speedStats;
        [SerializeField] private Rigidbody2D rb2D;
        [SerializeField] private Transform thisTransform;
        
        private Vector2 _moveDirection;
        
        protected override void Awake()
        {
            base.Awake();

            DifficultyManager.OnDifficultyChange += speedStats.ChangeDifficulty;
        }
        
        private void Start()
        {
            speedStats.ChangeDifficulty();
            DestroyYourself(4f);
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

        private void Move()
        {
            _moveDirection = Vector3.zero - thisTransform.position;
            rb2D.velocity = speedStats.moveSpeed * Time.fixedDeltaTime * _moveDirection.normalized;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                SoundManager.Instance.PlaySound(SoundType.PlayerSuccess);
                ParticlesManager.Instance.CreateParticle(ParticleType.CoinObtained, other.transform.position);
                LevelManager.OnCoinObtained?.Invoke();
                CurrencyManager.Instance.IncreaseCurrency(1);
                DifficultyManager.Instance.PlayerSuccess();
                DestroyYourself(0f);
            }
            else if (other.CompareTag("PlayerShield"))
            {
                SoundManager.Instance.PlaySound(SoundType.PlayerMistake);
                DestroyYourself(0f);
            }
        }
    }
}
