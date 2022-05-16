using System.Collections;
using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    public class PlayerSniper : Player
    {
        [Header("Speed")] 
        [SerializeField] private float moveSpeed; 
        
        [Header("Fields")]
        [SerializeField] private float timeBetweenShotsEasy;
        [SerializeField] private float timeBetweenShotsMedium;
        [SerializeField] private float timeBetweenShotsHard;

        [Header("Trigger Names")]
        [SerializeField] private string idleTrigger;
        [SerializeField] private string moveTrigger;
        [SerializeField] private string shootTrigger;
        
        [Header("References")]
        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer left;
        [SerializeField] private SpriteRenderer right;
        [SerializeField] private ParticleSystem leftParticles;
        [SerializeField] private ParticleSystem rightParticles;
        
        private float _timeBetweenShotsCounter;

        protected override void Awake()
        {
            base.Awake();
            
            _timeBetweenShotsCounter = timeBetweenShotsHard;
        }

        protected override void Update()
        {
            base.Update();
            
            if (invincible)
                return;
            
            Shoot();
        }
        
        protected override void GetMoveInput()
        {
            verticalRaw = InputManager.Instance.GetVerticalInput();

            if (verticalRaw > 0.1f) // Move up
            {
                SoundManager.Instance.PlaySound(SoundType.PlayerWalk, .5f);
                animator.SetTrigger(moveTrigger);
            }
            else if (verticalRaw < -0.1f) // Move down
            {
                SoundManager.Instance.PlaySound(SoundType.PlayerWalk, .5f);

                animator.SetTrigger(moveTrigger);
            }
            else
                animator.SetTrigger(idleTrigger);
        }

        protected override void Move()
        {
            rb2D.velocity = Time.fixedDeltaTime * moveSpeed * new Vector2(0f, verticalRaw).normalized;
        }

        private void Shoot()
        {
            _timeBetweenShotsCounter -= Time.deltaTime;
            if (!CanShoot())
                return;

            SoundManager.Instance.PlaySound(SoundType.Laser, 0.5f);
            animator.SetTrigger(shootTrigger);
            Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        }

        private bool CanShoot()
        {
            if (_timeBetweenShotsCounter > 0f) 
                return false;
            
            _timeBetweenShotsCounter = GetTimeBetweenShotsAccordingToDifficulty();
            return true;
        }

        private float GetTimeBetweenShotsAccordingToDifficulty()
        {
            return DifficultyManager.Instance.currentDifficulty switch
            {
                Difficulty.Easy => timeBetweenShotsEasy,
                Difficulty.Medium => timeBetweenShotsMedium,
                Difficulty.Hard => timeBetweenShotsHard,
                _ => 1f
            };
        }
        
        protected override IEnumerator PlayerDies()
        {
            GameManager.Instance.PlayerDies();
            invincible = true;
            particles.Stop();
            spRenderer.enabled = false;
            right.enabled = false;
            left.enabled = false;
            leftParticles.Stop();
            rightParticles.Stop();
            rb2D.velocity = Vector2.zero;

            yield return new WaitForSecondsRealtime(timePassedWhenHit);

            particles.Play();
            leftParticles.Play();
            rightParticles.Play();
            spRenderer.enabled = true;
            right.enabled = true;
            left.enabled = true;
            invincible = false;
            GameManager.Instance.PlayerRevive();
        }
    }
}
