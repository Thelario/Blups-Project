using UnityEngine;
using Game.Managers;

namespace Game.UI
{
    public class PalettesShop : MonoBehaviour
    {
        [SerializeField] private GameObject palettePrefabUI;
        [SerializeField] private GameObject purchasedPrefabUI;
        [SerializeField] private Transform palettesParent;
        [SerializeField] private PurchasePalettePanel palettePurchasePanel;

        private void OnEnable()
        {
            CreateItems();
        }

        private void OnDisable()
        {
            DestroyItems();
        }

        public void CreateItems()
        {
            foreach (ColorPalette cp in ColorPalettesManager.Instance.colorPalettes)
            {
                if (cp.purchased)
                {
                    Instantiate(purchasedPrefabUI, palettesParent);
                    continue;
                }

                GameObject palette = Instantiate(palettePrefabUI, palettesParent);
                PaletteItemUI piui = palette.GetComponentInChildren<PaletteItemUI>();
                for (int i = 0; i < 4; i++)
                {
                    piui.colors[i].color = cp.colors.colors[i];
                    piui.colorPalette = cp;
                    piui.palettesShop = this;
                }
            }
        }

        public void DestroyItems()
        {
            foreach (Transform t in palettesParent)
            {
                Destroy(t.gameObject);
            }
        }

        public void StartPurchase(ColorPalette colorPalette)
        {
            palettePurchasePanel.OpenPanel(colorPalette);
        }
    }
}