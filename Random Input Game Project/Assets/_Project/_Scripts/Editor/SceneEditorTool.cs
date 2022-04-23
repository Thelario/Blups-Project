#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneEditorTool : Editor
{
    [MenuItem("Tools/Change Scene/Main Menu")]
    public static void MainMenu()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Starting_Scene.unity", OpenSceneMode.Single);
    }

    [MenuItem("Tools/Change Scene/Lasers")]
    public static void Lasers()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Laser_Minigame.unity", OpenSceneMode.Single);
    }

    [MenuItem("Tools/Change Scene/Bombs")]
    public static void Bombs()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Bomb_Minigame.unity", OpenSceneMode.Single);
    }

    [MenuItem("Tools/Change Scene/Huge Obstacles")]
    public static void HugeObstacles()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Huge_Obstacles_Minigame.unity", OpenSceneMode.Single);
    }

    [MenuItem("Tools/Change Scene/1Dir Obstacles")]
    public static void Dir1Obstacles()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/1DirObstacles_Minigame.unity", OpenSceneMode.Single);
    }

    [MenuItem("Tools/Change Scene/Semicircle Lever")]
    public static void Semicircle()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Semicircle_Lever_Minigame.unity", OpenSceneMode.Single);
    }

    [MenuItem("Tools/Change Scene/Size Testing Scene")]
    public static void SizeTesting()
    {
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/Size_Testing_Scene.unity", OpenSceneMode.Single);
    }
}

#endif