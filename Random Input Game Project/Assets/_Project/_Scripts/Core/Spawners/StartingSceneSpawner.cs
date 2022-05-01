using Game.Entities;
using System.Collections;
using UnityEngine;

namespace Game.Spawnners
{
    public class StartingSceneSpawner : MonoBehaviour
    {
        [Header("Fields")]
        [SerializeField] private float timeBetweenObjects;

        [Header("Waypoints")]
        [SerializeField] private Spawnpoints[] waypoints;

        [Header("Prefabs")]
        [SerializeField] private GameObject[] obstaclesPrefabs;

        private int _prevChild = 0;
        private Vector3 _previousPos;

        private Transform _thisTransform;

        private void Awake()
        {
            _thisTransform = transform;

            _previousPos = Vector3.zero;
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
                // Spawn obstacle
                Direction randomDir = (Direction)Random.Range(0, 4);
                GameObject g = Instantiate(GetRandomPrefab(), GetRandomSpawnpoint(randomDir), Quaternion.identity, _thisTransform);
                if (g.TryGetComponent(out IDirectable id))
                    id.SetDirection(randomDir);

                yield return new WaitForSecondsRealtime(timeBetweenObjects);
            }
        }

        private GameObject GetRandomPrefab()
        {
            return obstaclesPrefabs[Random.Range(0, 2)];
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

        private Quaternion GetRandomRotation(Vector3 spawnPoint)
        {
            if (spawnPoint.x > 0f)
                return Quaternion.Euler(0f, 0f, Random.Range(-45f, 0f));
            else
                return Quaternion.Euler(0f, 0f, Random.Range(0f, 45f));
        }
    }
}
