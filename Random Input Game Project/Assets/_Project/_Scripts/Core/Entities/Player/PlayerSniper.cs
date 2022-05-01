using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    public class PlayerSniper : MonoBehaviour
    {
        [Header("Fields")]
        [SerializeField] private float timeBetweenShotsEasy;
        [SerializeField] private float timeBetweenShotsMedium;
        [SerializeField] private float timeBetweenShotsHard;

        [Header("Animations")]
        [SerializeField] private string shoot;

        [Header("References")]
        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private Animator animator;

        private float _timeBetweenShotsCounter;

        private void Awake()
        {
            _timeBetweenShotsCounter = timeBetweenShotsHard;
        }

        private void Update()
        {
            Shoot();
        }

        private void Shoot()
        {
            _timeBetweenShotsCounter -= Time.deltaTime;
            if (!CanShoot())
                return;

            SoundManager.Instance.PlaySound(SoundType.Laser, 0.5f);
            Animate(shoot);
            Instantiate(bullet, shootPoint);
        }

        private bool CanShoot()
        {
            if (_timeBetweenShotsCounter <= 0f)
            {
                _timeBetweenShotsCounter = GetTimeBetweenShotsAccordingToDifficulty();
                return true;
            }

            return false;
        }

        private float GetTimeBetweenShotsAccordingToDifficulty()
        {
            switch(DifficultyManager.Instance.currentDifficulty)
            {
                case Difficulty.Easy:
                    return timeBetweenShotsEasy;
                case Difficulty.Medium:
                    return timeBetweenShotsMedium;
                case Difficulty.Hard:
                    return timeBetweenShotsHard;
            }

            return 1f;
        }

        private void Animate(string animation)
        {
            animator.Play(animation);
        }
    }
}
