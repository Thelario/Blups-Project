using System.Collections.Generic;
using UnityEngine;

namespace Game.Spawnners
{
    public class ControlledObstaclesSpawner : MonoBehaviour
    {
        [SerializeField] private List<Vector2> evenSpawnPositions;
        [SerializeField] private List<Vector2> oddSpawnPositions;
        [SerializeField] private GameObject[] prefabs;
    }
}
