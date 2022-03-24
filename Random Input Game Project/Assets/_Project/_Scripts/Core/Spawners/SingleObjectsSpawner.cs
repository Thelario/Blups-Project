using System.Collections;
using UnityEngine;

namespace Game.Spawnners
{
    public enum Direction { Left, Right, Up, Down }

    [System.Serializable]
    public struct Waypoints
    {
        public Vector3[] spawnPoints;
    }

    public class SingleObjectsSpawner : MonoBehaviour
    { 
        [Header("Fields")]
        [SerializeField] private float timeBetweenObstacles;

        [Header("References")]
        [SerializeField] private GameObject[] obstaclesPrefabs;
        [SerializeField] private Waypoints[] waypoints;

        private int _prevChild = 0;

        private void Start()
        {
            StartCoroutine(nameof(Spawn));
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                Instantiate(obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)], GetRandomSpawnpoint(Direction.Up), Quaternion.identity);
                yield return new WaitForSeconds(timeBetweenObstacles);
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
            do {
                n = Random.Range(0, waypoints[index].spawnPoints.Length);
            } while (n == _prevChild);
            _prevChild = n;
            return n;
        }
    }
}
