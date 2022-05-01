#if UNITY_EDITOR

using Game.Managers;
using UnityEditor;
using UnityEngine;

public class DeveloperMode 
{
    [MenuItem("Developer/Clear Saves")]
    public static void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
        
        Debug.Log("<color=Color.blue>Developer: </color>" + "Clearing all saves from player prefs in Developer Mode.");
    }
    
    [MenuItem("Developer/Unlock Random Secret")]
    public static void UnlockRandomSecret()
    {
        SecretsManager.Instance.UnlockRandomSecret();
        
        Debug.Log("<color=Color.blue>Developer: </color>" + "Unlocking Random Secret in Developer Mode.");
    }
    
    [MenuItem("Developer/Give 500 coins")]
    public static void GiveCoins500()
    {
        CurrencyManager.Instance.IncreaseCurrency(500);
        
        Debug.Log("<color=Color.blue>Developer: </color>" + "Giving 500 coins in Developer Mode.");
    }
}

#endif