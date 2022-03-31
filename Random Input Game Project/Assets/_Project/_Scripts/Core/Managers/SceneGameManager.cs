using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Managers
{
    public enum Scenes { Starting, Dir4Obstacles, Bombs, Dir1Obstacles }

    public class SceneGameManager : Singleton<SceneGameManager>
    {
        public int currentScene;

        public void LoadScene(Scenes scene)
        {
            int sc = (int)scene;
            currentScene = sc;
            SceneManager.LoadScene(sc);
        }

        public void LoadRandomGameScene()
        {
            int sc;
            do
            {
                sc = Random.Range(1, System.Enum.GetValues(typeof(Scenes)).Length);
            } while (sc == currentScene);

            currentScene = sc;
            SceneManager.LoadScene(sc);
        }
    }
}
