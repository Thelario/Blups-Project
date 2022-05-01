using Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Spawnners
{
    public class LasersSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject laserIndicatorPrefab;

        [Header("Fields")]
        [SerializeField] private float timeBetweenLasersEasy;
        [SerializeField] private float timeBetweenLasersMedium;
        [SerializeField] private float timeBetweenLasersHard;

        [SerializeField] private Spawnpoints[] waypoints;

        private int _prevChild = 0;
        private float _timeBetweenLasers;

        private void Awake()
        {
            LevelManager.OnLevelStart += SetSpawnTrue;
            LevelManager.OnLevelUnPause += SetSpawnTrue;
            LevelManager.OnLevelEnd += SetSpawnFalse;
            LevelManager.OnLevelLost += SetSpawnFalse;
            LevelManager.OnLevelPause += SetSpawnFalse;

            DifficultyManager.OnDifficultyChange += ChangeDifficulty;
        }

        private void OnDestroy()
        {
            LevelManager.OnLevelStart -= SetSpawnTrue;
            LevelManager.OnLevelUnPause -= SetSpawnTrue;
            LevelManager.OnLevelEnd -= SetSpawnFalse;
            LevelManager.OnLevelLost -= SetSpawnFalse;
            LevelManager.OnLevelPause -= SetSpawnFalse;

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
                    _timeBetweenLasers = timeBetweenLasersEasy;
                    break;
                case Difficulty.Medium:
                    _timeBetweenLasers = timeBetweenLasersMedium;
                    break;
                case Difficulty.Hard:
                    _timeBetweenLasers = timeBetweenLasersHard;
                    break;
            }
        }

        private void SetSpawnTrue()
        {
            StartCoroutine(nameof(Spawn));
        }

        private void SetSpawnFalse()
        {
            StopCoroutine(nameof(Spawn));
            GameManager.Instance.SetRandomDirection();
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                Vector3 spawnPoint = GetRandomSpawnpoint(Direction.Up);
                Instantiate(laserIndicatorPrefab, spawnPoint, GetRandomRotation());
                yield return new WaitForSeconds(_timeBetweenLasers);
            }
        }

        private Vector3 GetRandomSpawnpoint(Direction dir)
        {
            switch (dir)
            {
                case Direction.Left:
                    return waypoints[0].spawnPoints[GetRandomChild(0)];
                case Direction.Right:
                    return waypoints[1].spawnPoints[GetRandomChild(1)];
                case Direction.Down:
                    return waypoints[2].spawnPoints[GetRandomChild(2)];
                case Direction.Up:
                    return waypoints[3].spawnPoints[GetRandomChild(3)];
                default:
                    Debug.LogError("Not correct Direction when spawning.");
                    return waypoints[0].spawnPoints[GetRandomChild(0)];
            }
        }

        private Quaternion GetRandomRotation(Vector3 spawnPoint)
        {
            if (spawnPoint.x > 0f)
                return Quaternion.Euler(0f, 0f, Random.Range(-45f, 0f));
            else
                return Quaternion.Euler(0f, 0f, Random.Range(0f, 45f));
        }

        private Quaternion GetRandomRotation()
        {
            return Quaternion.Euler(0f, 0f, Random.Range(-5f, 5f));
        }

        private int GetRandomChild(int index)
        {
            int n;
            do
            {
                n = Random.Range(0, waypoints[index].spawnPoints.Length);
            } while (n == _prevChild);
            _prevChild = n;
            return n;
        }
    }
}
