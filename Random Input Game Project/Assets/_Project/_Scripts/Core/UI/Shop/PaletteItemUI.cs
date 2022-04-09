using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Button))]
    public class PaletteItemUI : MonoBehaviour
    {
        public Image[] colors;
        public ColorPalette colorPalette;
        public PalettesShop palettesShop;
        /*
        private Button _menuButton;

        private void Start()
        {
            _menuButton = GetComponent<Button>();
            _menuButton.onClick.AddListener(OnButtonClicked);
        }
        */

        public void OnButtonClicked()
        {
            palettesShop.StartPurchase(colorPalette);
        }
    }
}
