using UnityEngine;

namespace Game.Managers
{
    public class OptionsManager : Singleton<OptionsManager>
    {
        public delegate void OnOptionsChanged(float newValue);

        private void Start()
        {
            SetOptionsValues();
        }

        private void SetOptionsValues()
        {
            // TODO: here I need to add the logic for the save & load that is related to the saving of the player
            // preferences of the options, so that they don't change every time the player restarts the game.

            MasterVolume = .5f;
            MusicVolume = 0.1f;
            SfxVolume = 0.4f;
        }

        private float _masterVolume;
        public float MasterVolume
        {
            get => _masterVolume;
            set
            {
                _masterVolume = Mathf.Clamp(value, 0f, 1f);
                SoundManager.Instance.ChangeMusicVolume();
            }
        }

        private float _musicVolume;
        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = Mathf.Clamp(value, 0f, 1f);
                SoundManager.Instance.ChangeMusicVolume();
            }
        }

        private float _sfxVolume;
        public float SfxVolume
        {
            get => _sfxVolume;
            set => _sfxVolume = Mathf.Clamp(value, 0f, 1f);
        }
    }
}
