using Game.Managers;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class BombSpawner : MonoBehaviour
    {
        [Header("Fields")]
        [SerializeField] private float timeBetweenBombsEasy;
        [SerializeField] private float timeBetweenBombsMedium;
        [SerializeField] private float timeBetweenBombsHard;

        [SerializeField] private GameObject bombIndicatorPrefab;

        private Transform _thisTransform;

        private Vector3 _previousPos;
        private float _timeBetweenBombs;

        private void Awake()
        {
            _thisTransform = transform;
            _previousPos = Vector3.zero;

            LevelManager.OnLevelStart += StartSpawningBombs;
            LevelManager.OnLevelEnd += StopSpawningBombs;
            LevelManager.OnLevelLost += StopSpawningBombs;
            LevelManager.OnLevelPause += StopSpawningBombs;

            DifficultyManager.OnDifficultyChange += ChangeDifficulty;
        }

        private void OnDestroy()
        {
            LevelManager.OnLevelStart -= StartSpawningBombs;
            LevelManager.OnLevelEnd -= StopSpawningBombs;
            LevelManager.OnLevelLost -= StopSpawningBombs;
            LevelManager.OnLevelPause -= StopSpawningBombs;

            DifficultyManager.OnDifficultyChange -= ChangeDifficulty;
        }

        private void OnLevelWasLoaded()
        {
            ChangeDifficulty();
        }

        public void ChangeDifficulty()
        {
            switch (DifficultyManager.Instance.currentDifficulty)
            {
                case Difficulty.Easy:
                    _timeBetweenBombs = timeBetweenBombsEasy;
                    break;
                case Difficulty.Medium:
                    _timeBetweenBombs = timeBetweenBombsMedium;
                    break;
                case Difficulty.Hard:
                    _timeBetweenBombs = timeBetweenBombsHard;
                    break;
            }
        }

        private void StartSpawningBombs()
        {
            StartCoroutine(nameof(LoopSpawnBombs));
        }

        private void StopSpawningBombs()
        {
            StopCoroutine(nameof(LoopSpawnBombs));
        }

        private IEnumerator LoopSpawnBombs()
        {
            while (true)
            {
                SpawnBomb();
                yield return new WaitForSeconds(_timeBetweenBombs);
            }
        }

        private void SpawnBomb()
        {
            Instantiate(bombIndicatorPrefab, Utils.GetRandomPos(ref _previousPos), Quaternion.identity, _thisTransform);
        }
    }
}
