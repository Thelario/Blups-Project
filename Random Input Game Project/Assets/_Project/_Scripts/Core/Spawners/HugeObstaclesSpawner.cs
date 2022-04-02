using Game.Entities;
using Game.Managers;
using System.Collections;
using UnityEngine;

namespace Game.Spawnners
{
    public class HugeObstaclesSpawner : MonoBehaviour
    {
        [Header("Fields")]
        [SerializeField] private float timeBetweenObstaclesEasy;
        [SerializeField] private float timeBetweenObstaclesMedium;
        [SerializeField] private float timeBetweenObstaclesHard;

        [Header("References")]
        [SerializeField] private GameObject[] obstaclesPrefabs;
        [SerializeField] private Waypoints[] waypoints;
        [SerializeField] private ExclamationManager exclamationManager;

        private int _prevChild = 0;
        private float _timeBetweenObstacles;
        private bool _spawnDouble = false;

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
                    _timeBetweenObstacles = timeBetweenObstaclesEasy;
                    _spawnDouble = false;
                    break;
                case Difficulty.Medium:
                    _timeBetweenObstacles = timeBetweenObstaclesMedium;
                    _spawnDouble = false;
                    break;
                case Difficulty.Hard:
                    _timeBetweenObstacles = timeBetweenObstaclesHard;
                    _spawnDouble = true;
                    break;
            }
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                if (_spawnDouble)
                {
                    Direction randomDir1 = (Direction)Random.Range(0, 4);
                    Vector3 spawnPoint1 = GetRandomSpawnpoint(randomDir1);
                    exclamationManager.CreateExclamation(spawnPoint1, randomDir1);
                    SoundManager.Instance.PlaySound(SoundType.Danger, Random.Range(0.10f, 0.2f));

                    Direction randomDir2 = (Direction)Random.Range(0, 4);
                    Vector3 spawnPoint2 = GetRandomSpawnpoint(randomDir2);
                    exclamationManager.CreateExclamation(spawnPoint2, randomDir2);
                    SoundManager.Instance.PlaySound(SoundType.Danger, Random.Range(0.10f, 0.2f));

                    yield return new WaitForSeconds(1f);

                    GameObject g1 = Instantiate(obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)], spawnPoint1, Quaternion.identity);
                    GameObject g2 = Instantiate(obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)], spawnPoint2, Quaternion.identity);

                    if (g1.TryGetComponent(out IDirectable id1))
                        id1.SetDirection(randomDir1);

                    if (g2.TryGetComponent(out IDirectable id2))
                        id2.SetDirection(randomDir2);

                    yield return new WaitForSeconds(_timeBetweenObstacles);
                }
                else
                {
                    Direction randomDir = (Direction)Random.Range(0, 4);
                    Vector3 spawnPoint = GetRandomSpawnpoint(randomDir);
                    exclamationManager.CreateExclamation(spawnPoint, randomDir);
                    SoundManager.Instance.PlaySound(SoundType.Danger, Random.Range(0.10f, 0.2f));

                    yield return new WaitForSeconds(1f);

                    GameObject g = Instantiate(obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)], spawnPoint, Quaternion.identity);

                    if (g.TryGetComponent(out IDirectable id))
                        id.SetDirection(randomDir);

                    yield return new WaitForSeconds(_timeBetweenObstacles);
                }
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
