using UnityEngine;
using System.Collections.Generic;

namespace Game.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private SoundAudioClip[] soundAudioClipArray;
        private Dictionary<SoundType, AudioClip> _audioClipDictionary;

        [SerializeField] private float volume; // Volume of SFX
        [SerializeField] private float pitch; // Pitch of SFX

        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioSource sfxAudioSource;

        private Dictionary<SoundType, float> _soundTimerDictionary;

        protected override void Awake()
        {
            base.Awake();

            Initialize();
        }

        private void Initialize()
        {
            _soundTimerDictionary = new Dictionary<SoundType, float>
            {
                [SoundType.PlayerWalk] = 0f
            };

            _audioClipDictionary = new Dictionary<SoundType, AudioClip>();
            foreach (SoundAudioClip sac in soundAudioClipArray)
                _audioClipDictionary.Add(sac.sound, sac.audioClip);
        }
        
        public void ChangeMusicVolume()
        {
            musicAudioSource.volume = OptionsManager.Instance.MasterVolume * OptionsManager.Instance.MusicVolume * OptionsManager.Instance.MusicVolume;
            if (musicAudioSource.volume < 0.001)
                musicAudioSource.volume = 0f;
        }

        public void PlaySound(SoundType st)
        {
            if (CanPlaySound(st))
            {
                sfxAudioSource.pitch = Random.Range(pitch - 0.1f, pitch + 0.1f);
                sfxAudioSource.PlayOneShot(SearchSound(st), OptionsManager.Instance.MasterVolume * OptionsManager.Instance.SfxVolume * OptionsManager.Instance.SfxVolume);
            }
        }

        public void PlaySound(SoundType st, float newVolume)
        {
            if (CanPlaySound(st))
            {
                sfxAudioSource.pitch = Random.Range(pitch - 0.1f, pitch + 0.1f);
                sfxAudioSource.PlayOneShot(SearchSound(st), OptionsManager.Instance.MasterVolume * OptionsManager.Instance.SfxVolume * newVolume);
            }
        }

        private bool CanPlaySound(SoundType sound)
        {
            switch (sound)
            {
                default:
                    return true;
                case SoundType.PlayerWalk:
                    if (_soundTimerDictionary.ContainsKey(sound))
                    {
                        float lastTimePlayed = _soundTimerDictionary[sound];
                        float playerMoveTimerMax = .3f;
                        if (lastTimePlayed + playerMoveTimerMax < Time.time)
                        {
                            _soundTimerDictionary[sound] = Time.time;
                            return true;
                        }
                        
                        return false;
                    }
                    else
                        return true;
            }
        }

        private AudioClip SearchSound(SoundType st)
        {
            _audioClipDictionary.TryGetValue(st, out AudioClip outP);
            return outP;
        }
    }

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundType sound;
        public AudioClip audioClip;
    }

    public enum SoundType
    {
        PlayerObstacleHit,
        PlayerWalk,
        MouseOverButton,
        ButtonClick,
        Coin,
        Bomb,
        Laser,
        Danger,
        PlayerSuccess,
        PlayerMistake
    }
}
