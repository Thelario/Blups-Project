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

        private float _timeBetweenShotsCounter;

        protected override void Awake()
        {
            base.Awake();
            
            _timeBetweenShotsCounter = timeBetweenShotsHard;
        }

        protected override void Update()
        {
            base.Update();
            
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
    }
}
