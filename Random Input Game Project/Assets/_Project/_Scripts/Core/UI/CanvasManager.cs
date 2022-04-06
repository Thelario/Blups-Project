using Game.Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.UI
{
    public enum CanvasType
    {
        MainMenu,
        MainGamesSelector,
        MainShop,
        MainOptions,
        MainAbout,
        MainAchievements,
        GameMenu,
        GamePause,
        GameOptions,
        ColorPalettesShop,
        MinigamesShop,
        SkinsShop,
        VaultMenu,
        VaultPalettes,
        VaultSkins,
        VaultMinigames,
        ScretsMenu
    }

    /// <summary>
    /// This class will control the child objects and allow us to open and close each child.
    /// </summary>
    public class CanvasManager : Singleton<CanvasManager>
    {
        List<CanvasController> canvasControllerList;
        public CanvasController lastActiveCanvas;

        protected override void Awake()
        {
            base.Awake();

            canvasControllerList = GetComponentsInChildren<CanvasController>(true).ToList();

            canvasControllerList.ForEach(x => x.gameObject.SetActive(false));

            SwitchCanvas(CanvasType.MainMenu);

            TimeManager.Instance.Pause();
        }

        public void SwitchCanvas(CanvasType _type)
        {
            if (lastActiveCanvas != null)
            {
                lastActiveCanvas.gameObject.SetActive(false);
            }

            CanvasController desiredCanvas = canvasControllerList.Find(x => x.canvasType == _type);
            if (desiredCanvas != null)
            {
                desiredCanvas.gameObject.SetActive(true);
                lastActiveCanvas = desiredCanvas;
            }
            else { Debug.LogWarning("The desired menu canvas was not found!"); }
        }
    }
}
