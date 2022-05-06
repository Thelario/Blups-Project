#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneEditorTool : Editor
{
    [MenuItem("Developer/Change Scene/Main Menu")]
    public static void MainMenu()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Main Menu.unity", OpenSceneMode.Single);
    }

    [MenuItem("Developer/Change Scene/Minigame Lasers")]
    public static void Lasers()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Minigame Lasers.unity", OpenSceneMode.Single);
    }

    [MenuItem("Developer/Change Scene/Minigame Huge Obstacles")]
    public static void HugeObstacles()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Minigame Huge Obstacles.unity", OpenSceneMode.Single);
    }

    [MenuItem("Developer/Change Scene/Minigame One Direction Obstacles")]
    public static void Dir1Obstacles()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Minigame One Direction Obstacles.unity", OpenSceneMode.Single);
    }

    [MenuItem("Developer/Change Scene/Minigame Lever")]
    public static void Semicircle()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Minigame Lever.unity", OpenSceneMode.Single);
    }

    [MenuItem("Developer/Change Scene/Minigame Sniper")]
    public static void SniperScene()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Minigame Sniper.unity", OpenSceneMode.Single);
    }

    [MenuItem("Developer/Change Scene/Minigame Sword")]
    public static void SwordScene()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Minigame Sword.unity", OpenSceneMode.Single);
    }
}

#endif