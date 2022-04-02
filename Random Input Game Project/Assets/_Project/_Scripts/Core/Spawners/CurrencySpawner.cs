using UnityEngine;
using Game.Managers;

namespace Game.Spawnners
{
    public class CurrencySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject coinPrefab;

        private Transform _thisTransform;

        private Vector3 _previousPos;

        private void Awake()
        {
            _thisTransform = transform;
            _previousPos = Vector3.zero;

            LevelManager.OnLevelStart += SpawnCoin;
            LevelManager.OnCoinObtained += SpawnCoin;
        }

        private void OnDestroy()
        {
            LevelManager.OnCoinObtained -= SpawnCoin;
            LevelManager.OnLevelStart -= SpawnCoin;
        }

        private void SpawnCoin()
        {
            Instantiate(coinPrefab, Utils.GetRandomPos(ref _previousPos), Quaternion.identity, _thisTransform);
        }
    }
}
