using System;
using Game.Managers;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Spawnners
{
    public class SniperSpawner : MonoBehaviour
    {
        [Header("Difficulty Settings")]
        [SerializeField] private float timeBetweenObjectsEasy;
        [SerializeField] private float timeBetweenObjectsMedium;
        [SerializeField] private float timeBetweenObjectsHard;

        [Header("Waypoints")]
        [SerializeField] private Vector3[] waypoints;

        [Header("Enemies")]
        [SerializeField] private GameObject enemy;

        private float _timeBetweenEnemies;
        private Vector3 _previousSpawnpoint;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _previousSpawnpoint = Vector3.zero;
            
            GameManager.OnPlayerDeath += SetSpawnFalse;
            GameManager.OnPlayerRevive += SetSpawnTrue;
        }

        private void OnDestroy()
        {
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
            while(true)
            {
                _timeBetweenEnemies = ChangeDifficulty();

                SpawnEnemy();

                yield return new WaitForSeconds(_timeBetweenEnemies);
            }
        }

        private void SpawnEnemy()
        {
            Instantiate(enemy, GetRandomSpawnpoint(), Quaternion.identity, _transform);
        }
            

        private Vector3 GetRandomSpawnpoint()
        {
            Vector3 aux;

            do
            {
                aux = waypoints[Random.Range(0, waypoints.Length)];
            } while (aux == _previousSpawnpoint);

            _previousSpawnpoint = aux;
            return aux;
        }

        private float ChangeDifficulty()
        {
            switch (DifficultyManager.Instance.currentDifficulty)
            {
                case Difficulty.Easy:
                    return timeBetweenObjectsEasy;
                case Difficulty.Medium:
                    return timeBetweenObjectsMedium;
                case Difficulty.Hard:
                    return timeBetweenObjectsHard;
            }

            return 1f;
        }
    }
}
