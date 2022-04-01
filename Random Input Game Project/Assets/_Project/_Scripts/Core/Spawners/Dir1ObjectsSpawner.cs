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

        [Header("Player Ref")]
        [SerializeField] private Player_1Dir player;

        [Header("Fields")]
        [SerializeField] private float timeBetweenObstacles;

        [Header("References")]
        [SerializeField] private GameObject[] obstaclesPrefabs;
        [SerializeField] private Waypoints[] waypoints;

        private int _prevChild = 0;

        private void Awake()
        {
            LevelManager.OnLevelStart += SetSpawnTrue;
            LevelManager.OnLevelUnPause += SetSpawnTrue;
            LevelManager.OnLevelEnd += SetSpawnFalse;
            LevelManager.OnLevelLost += SetSpawnFalse;
            LevelManager.OnLevelPause += SetSpawnFalse;
        }

        private void OnDestroy()
        {
            LevelManager.OnLevelStart -= SetSpawnTrue;
            LevelManager.OnLevelUnPause -= SetSpawnTrue;
            LevelManager.OnLevelEnd -= SetSpawnFalse;
            LevelManager.OnLevelLost -= SetSpawnFalse;
            LevelManager.OnLevelPause -= SetSpawnFalse;
        }

        private IEnumerator Spawn()
        {
            Direction dir = GameManager.Instance.obstaclesCurrentDirection;

            while (true)
            {
                GameObject g = Instantiate(obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)], GetRandomSpawnpoint(dir), Quaternion.identity);

                if (g.TryGetComponent(out IDirectable id))
                    id.SetDirection(dir);

                yield return new WaitForSeconds(timeBetweenObstacles);
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
            OnSpawnFalse?.Invoke();
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
