using System.Collections;
using UnityEngine;

namespace Game.Managers
{
    public enum Difficulty { Easy, Medium, Hard }

    public class DifficultyManager : Singleton<DifficultyManager>
    {
        public delegate void DifficultyChange();
        public static event DifficultyChange OnDifficultyChange;
        
        public Difficulty currentDifficulty;
        [SerializeField] private GameObject canvas;
        [SerializeField] private Animator animator;
        [SerializeField] private string easyToMedium;
        [SerializeField] private string mediumToEasy;
        [SerializeField] private string mediumToHard;
        [SerializeField] private string hardToMedium;

        private int _succesess = 0;
        private int _mistakes = 0;

        public void PlayerMistake()
        {
            _mistakes--;
            _succesess = 0;

            if (currentDifficulty == Difficulty.Easy)
                return;

            if (currentDifficulty == Difficulty.Hard)
            {
                if (_mistakes == 0)
                {
                    ChangeDifficulty(Difficulty.Medium);
                    _mistakes = 3;
                }
            }
            else if (currentDifficulty == Difficulty.Medium)
            {
                if (_mistakes == 0)
                    ChangeDifficulty(Difficulty.Easy);
            }
        }

        public void PlayerSuccess()
        {
            _succesess++;
            _mistakes = Mathf.Clamp(++_mistakes, 0, 3);

            if (currentDifficulty == Difficulty.Hard)
                return;

            if (currentDifficulty == Difficulty.Easy)
            {
                if (_succesess == 3)
                {
                    ChangeDifficulty(Difficulty.Medium);
                    _succesess = 0;
                    _mistakes = 3;
                }
            }
            else if (currentDifficulty == Difficulty.Medium)
            {
                if (_succesess == 5)
                {
                    ChangeDifficulty(Difficulty.Hard);
                    _mistakes = 3;
                }
            }
        }

        private void ChangeDifficulty(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    StartCoroutine(nameof(ControlCanvasActivation));
                    animator.Play(mediumToEasy);
                    SoundManager.Instance.PlaySound(SoundType.PlayerMistake);
                    break;
                case Difficulty.Medium:
                    if (currentDifficulty == Difficulty.Easy)
                    {
                        StartCoroutine(nameof(ControlCanvasActivation));
                        animator.Play(easyToMedium);
                        TimeManager.Instance.SlowTime(2f);
                        SoundManager.Instance.PlaySound(SoundType.PlayerSuccess);
                    }
                    else
                    {
                        StartCoroutine(nameof(ControlCanvasActivation));
                        SoundManager.Instance.PlaySound(SoundType.PlayerMistake);
                        animator.Play(hardToMedium);
                    }
                    break;
                case Difficulty.Hard:
                    StartCoroutine(nameof(ControlCanvasActivation));
                    animator.Play(mediumToHard);
                    TimeManager.Instance.SlowTime(2f);
                    SoundManager.Instance.PlaySound(SoundType.PlayerSuccess);
                    break;
            }

            currentDifficulty = difficulty;
            OnDifficultyChange?.Invoke();
        }

        private IEnumerator ControlCanvasActivation()
        {
            canvas.SetActive(true);
            yield return new WaitForSecondsRealtime(2f);
            canvas.SetActive(false);
        }
    }
}
