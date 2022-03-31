using Game.Managers;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class BombSpawner : MonoBehaviour
    {
        [SerializeField] private float timeBetweenBombs;
        [SerializeField] private GameObject bombIndicatorPrefab;

        private Transform _thisTransform;

        private Vector3 _previousPos;

        private void Awake()
        {
            _thisTransform = transform;
            _previousPos = Vector3.zero;

            LevelManager.OnLevelStart += StartSpawningBombs;
            LevelManager.OnLevelEnd += StopSpawningBombs;
            LevelManager.OnLevelLost += StopSpawningBombs;
            LevelManager.OnLevelPause += StopSpawningBombs;
        }

        private void OnDestroy()
        {
            LevelManager.OnLevelStart -= StartSpawningBombs;
            LevelManager.OnLevelEnd -= StopSpawningBombs;
            LevelManager.OnLevelLost -= StopSpawningBombs;
            LevelManager.OnLevelPause -= StopSpawningBombs;
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
                yield return new WaitForSeconds(timeBetweenBombs);
            }
        }

        private void SpawnBomb()
        {
            Instantiate(bombIndicatorPrefab, Utils.GetRandomPos(ref _previousPos), Quaternion.identity, _thisTransform);
        }
    }
}
