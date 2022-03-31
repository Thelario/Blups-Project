using UnityEngine;
using Game.Managers;
using System.Collections;

namespace Game.Entities
{
    public abstract class Player : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float timePassedWhenHit = 2f;

        protected float _horizontal;
        protected float _vertical;

        protected float _horizontalRaw;
        protected float _verticalRaw;

        protected bool _invencible;

        protected Rigidbody2D _rb2D;
        protected Transform _transform;

        protected void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _transform = transform;
        }

        protected void Update()
        {
            GetMoveInput();
        }

        protected void FixedUpdate()
        {
            Move();
        }

        protected abstract void GetMoveInput();

        protected abstract void Move();

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Obstacle"))
            {
                if (_invencible)
                    return;

                TimeManager.Instance.SlowTime(timePassedWhenHit);
                StartCoroutine(nameof(MakeInvencible));
                DifficultyManager.Instance.PlayerMistake(4);
            }
            else if (collision.CompareTag("Coin"))
            {
                SoundManager.Instance.PlaySound(SoundType.Coin);
                ParticlesManager.Instance.CreateParticle(ParticleType.CoinObtained, collision.transform.position);
                LevelManager.OnCoinObtained?.Invoke();
                CurrencyManager.Instance.IncreaseCurrency(1);
                DifficultyManager.Instance.PlayerSuccess(2);
                Destroy(collision.gameObject);
            }
        }

        protected IEnumerator MakeInvencible()
        {
            _invencible = true;
            yield return new WaitForSecondsRealtime(timePassedWhenHit);
            _invencible = false;
        }
    }
}
