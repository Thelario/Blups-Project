using UnityEngine;
using Game.Managers;
using System.Collections;
using Game.UI;

namespace Game.Entities
{
    public abstract class Player : MonoBehaviour
    {
        [Header("Fields")]
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float timePassedWhenHit = 2f;

        protected float _horizontal;
        protected float _vertical;

        protected float _horizontalRaw;
        protected float _verticalRaw;

        protected bool _invencible;

        protected Rigidbody2D _rb2D;
        protected Transform _transform;

        protected virtual void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _transform = transform;
        }

        protected void Update()
        {
            GetPauseInput();

            GetMoveInput();
        }

        protected void FixedUpdate()
        {
            Move();
        }

        protected void GetPauseInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CanvasManager.Instance.SwitchCanvas(CanvasType.GamePause);
                TimeManager.Instance.Pause();
            }
        }

        protected abstract void GetMoveInput();

        protected abstract void Move();

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Obstacle"))
            {
                if (_invencible)
                    return;

                HandleTriggerObstacle(collision);
            }
            else if (collision.CompareTag("Coin"))
            {
                HandleTriggerCoin(collision);
            }
        }

        protected virtual void HandleTriggerObstacle(Collider2D collision)
        {
            SoundManager.Instance.PlaySound(SoundType.PlyerObstacleHit);
            TimeManager.Instance.SlowTime(timePassedWhenHit);
            StartCoroutine(nameof(MakeInvencible));
            DifficultyManager.Instance.PlayerMistake();
        }

        protected virtual void HandleTriggerCoin(Collider2D collision)
        {
            SoundManager.Instance.PlaySound(SoundType.Coin);
            ParticlesManager.Instance.CreateParticle(ParticleType.CoinObtained, collision.transform.position);
            LevelManager.OnCoinObtained?.Invoke();
            CurrencyManager.Instance.IncreaseCurrency(1);
            DifficultyManager.Instance.PlayerSuccess();
            Destroy(collision.gameObject);
        }

        protected IEnumerator MakeInvencible()
        {
            _invencible = true;
            yield return new WaitForSecondsRealtime(timePassedWhenHit);
            _invencible = false;
        }
    }
}
