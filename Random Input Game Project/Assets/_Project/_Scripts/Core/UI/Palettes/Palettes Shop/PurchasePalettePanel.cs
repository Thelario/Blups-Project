using UnityEngine;
using TMPro;
using Game.Managers;
using UnityEngine.UI;

namespace Game.UI
{
    public class PurchasePalettePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image colorImage;
        [SerializeField] private Image frame;
        [SerializeField] private PalettesShop palettesShop;
        [SerializeField] private GameObject parentPanel;
        [SerializeField] private GameObject panelBackground;

        private ColorPalette _currentPalette;

        private void Awake()
        {
            panelBackground.transform.localScale = Vector3.zero;
            DeactivatePanel();
        }

        private void SetupPanel(ColorPalette colorPalette)
        {
            _currentPalette = colorPalette;
            text.text = colorPalette.price.ToString();
            colorImage.color = colorPalette.color;
            frame.color = colorPalette.color;
        }

        public void OpenPanel(ColorPalette colorPalette)
        {
            SetupPanel(colorPalette);
            AnimatePanelOpening();
        }

        public void Buy()
        {
            if (CurrencyManager.Instance.CurrencyAmount < _currentPalette.price)
                return;

            _currentPalette.Buy();
            CurrencyManager.Instance.DecreaseCurrency(_currentPalette.price);
            ColorPalettesManager.Instance.SaveColors();
            palettesShop.DestroyItems();
            palettesShop.CreateItems();
            Cancel();
        }

        public void Cancel()
        {
            _currentPalette = null;
            StartPanelClosing();
        }

        private void DeactivatePanel()
        {
            parentPanel.SetActive(false);
        }

        /* ANIMATIONS */

        private void AnimatePanelOpening()
        {
            parentPanel.SetActive(true);
            LeanTween.scale(panelBackground, new Vector3(1.15f, 1.15f, 1.15f), .5f).setIgnoreTimeScale(true).setOnComplete(FinalPanelOpening);
        }

        private void FinalPanelOpening()
        {
            LeanTween.scale(panelBackground, new Vector3(1f, 1f, 1f), .1f).setIgnoreTimeScale(true);
        }

        private void AnimatePanelClosing()
        {
            LeanTween.scale(panelBackground, Vector3.zero, .5f).setIgnoreTimeScale(true).setOnComplete(DeactivatePanel);
        }

        private void StartPanelClosing()
        {
            LeanTween.scale(panelBackground, new Vector3(1.15f, 1.15f, 1.15f), .1f).setIgnoreTimeScale(true).setOnComplete(AnimatePanelClosing);
        }
    }
}
