using Game.Managers;
using System;
using UnityEngine;

namespace Game.Spawnners.Helpers
{
    [Serializable]
    public class Time
    {
        [HideInInspector] public float timeBetweenObjects;

        [Header("Speed")]
        public float timeBetweenObjectsEasy;
        public float timeBetweenObjectsMedium;
        public float timeBetweenObjectsHard;
        public bool changeTimeBetweenObjectsWithDifficulty;

        public void ChangeDifficulty()
        {
            switch (DifficultyManager.Instance.currentDifficulty)
            {
                case Difficulty.Easy:
                    timeBetweenObjects = timeBetweenObjectsEasy;
                    break;
                case Difficulty.Medium:
                    timeBetweenObjects = timeBetweenObjectsMedium;
                    break;
                case Difficulty.Hard:
                    timeBetweenObjects = timeBetweenObjectsHard;
                    break;
            }
        }
    }
}
