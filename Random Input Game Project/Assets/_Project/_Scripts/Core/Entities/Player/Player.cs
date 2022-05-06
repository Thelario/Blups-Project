using UnityEngine;
using Game.Managers;
using System.Collections;
using Game.UI;

namespace Game.Entities
{
    public abstract class Player : MonoBehaviour
    {
        [Header("Fields")]
        [SerializeField] protected float timePassedWhenHit = 2f;

        protected float horizontalRaw;
        protected float verticalRaw;

        protected Rigidbody2D rb2D;
        protected Transform thisTransform;
        
        private bool _invincible;

        protected virtual void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            thisTransform = transform;
        }

        protected virtual void Update()
        {
#if UNITY_STANDALONE || UNITY_EDITOR 
            GetPauseInput();
#endif
            
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
                if (_invincible)
                    return;

                HandleTriggerObstacle();
            }
            else if (collision.CompareTag("Coin"))
            {
                HandleTriggerCoin(collision);
            }
        }
      
#if UNITY_STANDALONE || UNITY_EDITOR 
        protected void GetPauseInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CanvasManager.Instance.SwitchCanvas(CanvasType.GamePause);
                TimeManager.Instance.Pause();
            }
        }
#endif

        protected void HandleTriggerObstacle()
        {
            SoundManager.Instance.PlaySound(SoundType.PlayerObstacleHit);
            TimeManager.Instance.SlowTime(timePassedWhenHit);
            StartCoroutine(nameof(MakeInvencible));
            DifficultyManager.Instance.PlayerMistake();
        }

        protected void HandleTriggerCoin(Collider2D collision)
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
            _invincible = true;
            yield return new WaitForSecondsRealtime(timePassedWhenHit);
            _invincible = false;
        }
    }
}
