using Game.Managers;
using System.Collections;
using UnityEngine;
using Time = Game.Spawnners.Helpers.Time;

namespace Game.Spawnners
{
    public class CircularEnemySpawner : MonoBehaviour
    {
        [Header("Fields")]
        [SerializeField] private float distanceFromCenterToSpawn;
        [SerializeField] private float pcTimeModifier;
        [SerializeField] private Time time;
        [SerializeField] private GameObject[] prefabs;

        private void Awake()
        {
            DifficultyManager.OnDifficultyChange += ChangeDifficulty;
            GameManager.OnPlayerDeath += SetSpawnFalse;
            GameManager.OnPlayerRevive += SetSpawnTrue;
        }

        private void OnDestroy()
        {
            DifficultyManager.OnDifficultyChange -= ChangeDifficulty;
            GameManager.OnPlayerDeath -= SetSpawnFalse;
            GameManager.OnPlayerRevive -= SetSpawnTrue;
        }

        private void Start()
        {
            SetSpawnTrue();
        }

        private void SetSpawnTrue()
        {
            StartCoroutine(nameof(Spawn));
        }

        private void SetSpawnFalse()
        {
            StopCoroutine(nameof(Spawn));
        }

        private IEnumerator Spawn()
        {
            ChangeDifficulty();

            while (true)
            {
                SpawnSingleObject();

                yield return new WaitForSeconds(time.timeBetweenObjects);
            }
        }

        private void SpawnSingleObject()
        {
            Instantiate(GetRandomPrefab(), GetRandomSpawnpoint(), Quaternion.identity);
        }

        private GameObject GetRandomPrefab()
        {
            int n = Random.Range(0, 100);

            if (n == 99)
                return prefabs[2]; // Secret
            
            if (n > 80 && n < 99)
                return prefabs[1]; // Coin
            
            return prefabs[0]; // Normal Obstacle
        }

        private Vector3 GetRandomSpawnpoint()
        {
            float x, y;

            do
            {
                x = Random.Range(-1f, 1f);
                y = Random.Range(-1f, 1f);
            } 
            while (x == 0f && y == 0f);

            return distanceFromCenterToSpawn *  new Vector3(x, y).normalized;
        }

        private void ChangeDifficulty()
        {
            time.ChangeDifficulty();
            
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
            time.timeBetweenObjects -= pcTimeModifier;
#endif

        }
    }
}
