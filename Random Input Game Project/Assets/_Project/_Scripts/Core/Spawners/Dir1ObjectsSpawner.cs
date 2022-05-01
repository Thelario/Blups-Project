using Game.Entities;
using Game.Managers;
using System.Collections;
using UnityEngine;

namespace Game.Spawnners
{
    public class Dir1ObjectsSpawner : MonoBehaviour
    {
        public delegate void SpawnEvent();
        public static event SpawnEvent OnSpawnFalse;

        [Header("Fields")]
        [SerializeField] private float timeBetweenObstaclesEasy;
        [SerializeField] private float timeBetweenObstaclesMedium;
        [SerializeField] private float timeBetweenObstaclesHard;

        [Header("References")]
        [SerializeField] private GameObject[] obstaclesPrefabs;
        [SerializeField] private Spawnpoints[] waypoints;

        private int _prevChild = 0;
        private float _timeBetweenObstacles;
        private Direction _direction;

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
            GameManager.Instance.SetRandomDirection();
            OnSpawnFalse?.Invoke();

            ChangeDifficulty();
        }

        public void ChangeDifficulty()
        {
            switch (DifficultyManager.Instance.currentDifficulty)
            {
                case Difficulty.Easy:
                    _timeBetweenObstacles = timeBetweenObstaclesEasy;
                    break;
                case Difficulty.Medium:
                    _timeBetweenObstacles = timeBetweenObstaclesMedium;
                    break;
                case Difficulty.Hard:
                    if (_direction == Direction.Down || _direction == Direction.Up)
                        _timeBetweenObstacles = timeBetweenObstaclesHard - 0.1f;
                    else
                        _timeBetweenObstacles = timeBetweenObstaclesHard;
                    break;
            }
        }

        private IEnumerator Spawn()
        {
            _direction = GameManager.Instance.obstaclesCurrentDirection;

            while (true)
            {
                GameObject g = Instantiate(GetRandomPrefab(), GetRandomSpawnpoint(_direction), Quaternion.identity);

                if (g.TryGetComponent(out IDirectable id))
                    id.SetDirection(_direction);

                yield return new WaitForSeconds(_timeBetweenObstacles);
            }
        }

        private GameObject GetRandomPrefab()
        {
            int n = Random.Range(0, 100);

            if (n == 99)
                return obstaclesPrefabs[2]; // Secret
            
            if (n > 80 && n < 99)
                return obstaclesPrefabs[1]; // Coin
            
            return obstaclesPrefabs[0]; // Normal Obstacle
        }

        private void SetSpawnTrue()
        {
            DangerAnimationManager.Instance.SetActiveAllDangerParticles(false);
            StartCoroutine(nameof(Spawn));
        }

        private void SetSpawnFalse()
        {
            StopCoroutine(nameof(Spawn));
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
