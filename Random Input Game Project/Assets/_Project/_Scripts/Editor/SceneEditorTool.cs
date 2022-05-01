#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneEditorTool : Editor
{
    [MenuItem("Developer/Change Scene/Main Menu")]
    public static void MainMenu()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Starting_Scene.unity", OpenSceneMode.Single);
    }

    [MenuItem("Developer/Change Scene/Lasers")]
    public static void Lasers()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Laser_Minigame.unity", OpenSceneMode.Single);
    }

    [MenuItem("Developer/Change Scene/Bombs")]
    public static void Bombs()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Bomb_Minigame.unity", OpenSceneMode.Single);
    }

    [MenuItem("Developer/Change Scene/Huge Obstacles")]
    public static void HugeObstacles()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Huge_Obstacles_Minigame.unity", OpenSceneMode.Single);
    }

    [MenuItem("Developer/Change Scene/1Dir Obstacles")]
    public static void Dir1Obstacles()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/1DirObstacles_Minigame.unity", OpenSceneMode.Single);
    }

    [MenuItem("Developer/Change Scene/Semicircle Lever")]
    public static void Semicircle()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Semicircle_Lever_Minigame.unity", OpenSceneMode.Single);
    }

    [MenuItem("Developer/Change Scene/Sniper Scene")]
    public static void SniperScene()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Sniper_Scene.unity", OpenSceneMode.Single);
    }

    [MenuItem("Developer/Change Scene/Shield Minigame")]
    public static void ShieldScene()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Shield_Minigame.unity", OpenSceneMode.Single);
    }
}

#endif