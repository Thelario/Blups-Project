using System.Collections;
using UnityEngine;
using Game.Managers;
using Game.Entities;

public enum Direction { Left, Right, Up, Down }

namespace Game.Spawnners
{
    [System.Serializable]
    public struct Waypoints
    {
        public Vector3[] spawnPoints;
    }

    public class Dir4ObjectsSpawner : MonoBehaviour
    { 
        [Header("Fields")]
        [SerializeField] private float timeBetweenObstaclesEasy;
        [SerializeField] private float timeBetweenObstaclesMedium;
        [SerializeField] private float timeBetweenObstaclesHard;

        [Header("References")]
        [SerializeField] private GameObject[] obstaclesPrefabs;
        [SerializeField] private Waypoints[] waypoints;

        private int _prevChild = 0;

        private float _timeBetweenObstacles;

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
                    _timeBetweenObstacles = timeBetweenObstaclesHard;
                    break;
            }
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                Direction randomDir = (Direction)Random.Range(0, 4);
                GameObject g = Instantiate(obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)], GetRandomSpawnpoint(randomDir), Quaternion.identity);
               
                if (g.TryGetComponent(out IDirectable id))
                    id.SetDirection(randomDir);

                yield return new WaitForSeconds(_timeBetweenObstacles);
            }
        }

        private void SetSpawnTrue()
        {
            DangerAnimationManager.Instance.SetActiveAllDangerParticles(false);
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
            do {
                n = Random.Range(0, waypoints[index].spawnPoints.Length);
            } while (n == _prevChild);
            _prevChild = n;
            return n;
        }
    }
}
