using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Managers
{
    public enum Scenes { Starting, Bombs, Dir1Obstacles, Lasers, SizeMatters }

    public class SceneGameManager : Singleton<SceneGameManager>
    {
        public int currentScene;

        public float transitionTime;
        [SerializeField] private Animator transitionAnimator;

        private void OnLevelWasLoaded(int level)
        {
            if (level != 0)
                TimeManager.Instance.Resume();
        }

        public void LoadScene(Scenes scene)
        {
            int sc = (int)scene;
            currentScene = sc;
            StartCoroutine(SceneTransition(sc));
        }

        public void LoadRandomGameScene()
        {
            int sc;
            while (true)
            {
                sc = Random.Range(1, System.Enum.GetValues(typeof(Scenes)).Length);

                if (sc == currentScene)
                    continue;

                if (MinigamesManager.Instance.CheckMinigamePurchased(sc))
                    break;
            }

            currentScene = sc;
            StartCoroutine(SceneTransition(sc));
        }

        private IEnumerator SceneTransition(int scene)
        {
            transitionAnimator.Play("SceneEnd");

            yield return new WaitForSecondsRealtime(transitionTime);

            SceneManager.LoadScene(scene);
        }
    }
}
