using System.Collections;
using UnityEngine;

namespace Game.Managers
{
    public class TimeManager : Singleton<TimeManager>
    {
        public delegate void TimeChange();
        public static event TimeChange OnGamePause;
        public static event TimeChange OnGameResume;

        public void Pause()
        {
            Time.timeScale = 0f;
            OnGamePause?.Invoke();
        }

        public void Resume()
        {
            Time.timeScale = 1f;
            OnGameResume?.Invoke();
        }

        public void SlowTime(float slowTime)
        {
            StartCoroutine(nameof(Co_SlowTime), slowTime);
        }

        private IEnumerator Co_SlowTime(float slowTime)
        {
            Time.timeScale = 0.25f;
            yield return new WaitForSecondsRealtime(slowTime);
            Time.timeScale = 1f;
        }
    }
}
