using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Button))]
    public class PaletteItemUI : MonoBehaviour
    {
        public Image color;
        public ColorPalette colorPalette;
        public PalettesShop palettesShop;

        public void OnButtonClicked()
        {
            palettesShop.StartPurchase(colorPalette);
        }
    }
}
