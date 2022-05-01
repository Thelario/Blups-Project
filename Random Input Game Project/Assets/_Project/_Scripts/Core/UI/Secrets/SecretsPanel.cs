using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class SecretsPanel : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private GameObject parentPanel;
        [SerializeField] private GameObject panelBackground;
        
        private void Awake()
        {
            panelBackground.transform.localScale = Vector3.zero;
            DeactivatePanel();
        }
        
        public void OpenPanel(string ruleText)
        {
            SetupPanel(ruleText);
            AnimatePanelOpening();
        }
        
        public void Cancel()
        {
            StartPanelClosing();
        }

        private void SetupPanel(string ruleText)
        {
            text.text = ruleText;
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