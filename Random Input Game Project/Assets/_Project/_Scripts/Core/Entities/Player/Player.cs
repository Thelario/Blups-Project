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

        [Header("References")]
        [SerializeField] protected SpriteRenderer spRenderer;
        [SerializeField] protected ParticleSystem particles;
        
        protected float horizontalRaw;
        protected float verticalRaw;

        protected Rigidbody2D rb2D;
        protected Transform thisTransform;
        
        protected bool invincible;

        protected virtual void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            thisTransform = transform;
        }

        protected virtual void Update()
        {
#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL
            GetPauseInput();
#endif

            if (invincible)
                return;
                
            GetMoveInput();
        }

        protected void FixedUpdate()
        {
            if (invincible)
                return;
            
            Move();
        }
        
        protected abstract void GetMoveInput();

        protected abstract void Move();

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Obstacle"))
            {
                if (invincible)
                    return;

                HandleTriggerObstacle();
            }
            else if (collision.CompareTag("Coin"))
            {
                HandleTriggerCoin(collision);
            }
        }
      
#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL
        private void GetPauseInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CanvasManager.Instance.SwitchCanvas(CanvasType.GamePause);
                TimeManager.Instance.Pause();
            }
        }
#endif

        public void HandleTriggerObstacle()
        {
            SoundManager.Instance.PlaySound(SoundType.PlayerObstacleHit);
            ParticlesManager.Instance.CreateParticle(ParticleType.PlayerDeath, thisTransform.position);
            StartCoroutine(nameof(PlayerDies));
            DifficultyManager.Instance.PlayerMistake();
        }

        private void HandleTriggerCoin(Collider2D collision)
        {
            SoundManager.Instance.PlaySound(SoundType.Coin);
            ParticlesManager.Instance.CreateParticle(ParticleType.CoinObtained, collision.transform.position);
            LevelManager.OnCoinObtained?.Invoke();
            CurrencyManager.Instance.IncreaseCurrency(1);
            DifficultyManager.Instance.PlayerSuccess();
            Destroy(collision.gameObject);
        }

        protected virtual IEnumerator PlayerDies()
        {
            GameManager.Instance.PlayerDies();
            invincible = true;
            particles.Stop();
            spRenderer.enabled = false;
            rb2D.velocity = Vector2.zero;

            yield return new WaitForSecondsRealtime(timePassedWhenHit);

            particles.Play();
            spRenderer.enabled = true;
            invincible = false;
            GameManager.Instance.PlayerRevive();
        }
    }
}
