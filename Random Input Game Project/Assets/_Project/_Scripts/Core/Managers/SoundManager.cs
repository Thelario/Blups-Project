using UnityEngine;
using System.Collections.Generic;

namespace Game.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private SoundAudioClip[] soundAudioClipArray;
        private Dictionary<SoundType, AudioClip> audioClipDictionary;

        [SerializeField] private float volume; // Volume of SFX
        [SerializeField] private float pitch; // Pitch of SFX

        private AudioSource source; // Private reference of the audioSource where we are going to play our SFX

        private Dictionary<SoundType, float> soundTimerDictionary;

        protected override void Awake()
        {
            base.Awake();

            source = GetComponent<AudioSource>();

            Initialize();
        }

        private void Initialize()
        {
            soundTimerDictionary = new Dictionary<SoundType, float>
            {
                [SoundType.PlayerWalk] = 0f
            };

            audioClipDictionary = new Dictionary<SoundType, AudioClip>();
            foreach (SoundAudioClip sac in soundAudioClipArray)
                audioClipDictionary.Add(sac.sound, sac.audioClip);
        }

        public void PlaySound(SoundType st)
        {
            if (CanPlaySound(st))
            {
                float v = Random.Range(volume * volume - 0.1f, volume * volume + 0.1f);
                float p = Random.Range(pitch - 0.1f, pitch + 0.1f);
                source.pitch = p;
                source.PlayOneShot(SearchSound(st), v);
            }
        }

        public void PlaySound(SoundType st, float newVolume)
        {
            if (CanPlaySound(st))
            {
                float v = Random.Range(volume * newVolume - 0.1f, volume * newVolume + 0.1f);
                float p = Random.Range(pitch - 0.1f, pitch + 0.1f);
                source.pitch = p;
                source.PlayOneShot(SearchSound(st), v);
            }
        }

        private bool CanPlaySound(SoundType sound)
        {
            switch (sound)
            {
                default:
                    return true;
                case SoundType.PlayerWalk:
                    if (soundTimerDictionary.ContainsKey(sound))
                    {
                        float lastTimePlayed = soundTimerDictionary[sound];
                        float playerMoveTimerMax = .3f;
                        if (lastTimePlayed + playerMoveTimerMax < Time.time)
                        {
                            soundTimerDictionary[sound] = Time.time;
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                        return true;
            }
        }

        private AudioClip SearchSound(SoundType st)
        {
            audioClipDictionary.TryGetValue(st, out AudioClip outP);
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
        PlyerObstacleHit,
        PlayerWalk,
        MouseOverButton,
        ButtonClick,
        Coin,
        PowerUp,
        PlayerDeath,
        Bomb,
        Laser,
        Slash,
        Danger,
        PlayerSuccess,
        PlayerMistake
    }
}
