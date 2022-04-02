using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Spawnners
{
    public class LasersSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject laserIndicatorPrefab;

        [Header("Fields")]
        [SerializeField] private float timeBetweenLasersEasy;
        [SerializeField] private float timeBetweenLasersMedium;
        [SerializeField] private float timeBetweenLasersHard;

        [SerializeField] private Waypoints[] waypoints;

        private int _prevChild = 0;
        private float _timeBetweenLasers;
    }
}
