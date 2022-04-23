using System;
using UnityEngine;
using Game.Managers;

namespace Game.Entities.Helpers
{
    [Serializable]
    public class Speed
    {
        [HideInInspector] public float moveSpeed;

        [Header("Speed")]
        public float easyMoveSpeed;
        public float mediumMoveSpeed;
        public float hardMoveSpeed;
        public bool changeSpeedAccordingToDifficulty;

        public void ChangeDifficulty()
        {
            switch (DifficultyManager.Instance.currentDifficulty)
            {
                case Difficulty.Easy:
                    moveSpeed = easyMoveSpeed;
                    break;
                case Difficulty.Medium:
                    moveSpeed = mediumMoveSpeed;
                    break;
                case Difficulty.Hard:
                    moveSpeed = hardMoveSpeed;
                    break;
            }
        }
    }
}
