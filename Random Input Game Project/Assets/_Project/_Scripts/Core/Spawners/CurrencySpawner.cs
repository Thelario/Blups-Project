using UnityEngine;
using Game.Entities;
using System.Collections;
using Game.Managers;

namespace Game.Spawnners
{
    public class CurrencySpawner : MonoBehaviour
    {
        [Header("Fields")]
        [SerializeField] private float timeBetweenObjects;

        [Header("Waypoints")]
        [SerializeField] private Waypoints[] waypoints;
        [SerializeField] private bool spawnOnlySides = false;
        [SerializeField] private bool spawnVertically = false;

        [Header("Prefabs")]
        [SerializeField] private GameObject coinPrefab;

        private Transform _thisTransform;

        private Vector3 _previousPos;
        private int _prevChild = 0;

        private void Awake()
        {
            _thisTransform = transform;
            _previousPos = Vector3.zero;

            DifficultyManager.OnDifficultyChange += ChangeDifficulty;
        }

        private void OnDestroy()
        {
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
                    timeBetweenObjects = 2f;
                    break;
                case Difficulty.Medium:
                    timeBetweenObjects = 2f;
                    break;
                case Difficulty.Hard:
                    timeBetweenObjects = 1.5f;
                    break;
            }
        }

        private void Start()
        {
            StartSpawning();
        }

        private void StartSpawning()
        {
            StartCoroutine(nameof(Spawn));
        }

        private void StopSpawning()
        {
            StopCoroutine(nameof(Spawn));
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                Direction randomDir;
                if (spawnOnlySides)
                {
                    if (spawnVertically)
                        randomDir = (Direction)Random.Range(2, 4);
                    else
                        randomDir = (Direction)Random.Range(0, 2);
                }
                else
                    randomDir = (Direction)Random.Range(0, 4);

                GameObject g = Instantiate(coinPrefab, GetRandomSpawnpoint(randomDir), Quaternion.identity, _thisTransform);
                if (g.TryGetComponent(out IDirectable id))
                    id.SetDirection(randomDir);

                yield return new WaitForSecondsRealtime(timeBetweenObjects);
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
