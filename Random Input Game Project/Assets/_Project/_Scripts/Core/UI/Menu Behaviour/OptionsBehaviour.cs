using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class OptionsBehaviour : MonoBehaviour
    {
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        
        private void Start()
        {
            SetSettings();
        }

        private void OnEnable()
        {
            SetValuesFromOptions();
        }

        private void SetSettings()
        {
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
            sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);
        }

        private void SetValuesFromOptions()
        {
            masterVolumeSlider.value = OptionsManager.Instance.MasterVolume;
            musicVolumeSlider.value = OptionsManager.Instance.MusicVolume;
            sfxVolumeSlider.value = OptionsManager.Instance.SfxVolume;
        }
        
        private void SetMasterVolume(float vol) { OptionsManager.Instance.MasterVolume = vol; }
        private void SetMusicVolume(float vol) { OptionsManager.Instance.MusicVolume = vol; }
        private void SetSfxVolume(float vol) { OptionsManager.Instance.SfxVolume = vol; }
    }
}