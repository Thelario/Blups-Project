using UnityEngine;

namespace Game.Animations
{
    public class Popup : MonoBehaviour
    {
        [Header("Scales")]
        [SerializeField] private Vector3 maxScale;
        [SerializeField] private Vector3 defaultScale;

        [Header("Times")]
        [SerializeField] private float timeZeroMax;
        [SerializeField] private float timeMaxDefault;

        [Header("Delay")]
        [SerializeField] private bool delay;
        [SerializeField] private float delayTime;

        private GameObject _gameObject;
        private Transform _transform;

        private void Awake()
        {
            _gameObject = gameObject;
            _transform = transform;
        }

        private void OnEnable()
        {
            AnimateZeroMax();
        }

        public void AnimateZeroMax()
        {
            _transform.localScale = Vector3.zero;

            if (delay)
                LeanTween.scale(_gameObject, maxScale, timeZeroMax).setOnComplete(AnimateMaxDefault).setIgnoreTimeScale(true).setDelay(delayTime);
            else
                LeanTween.scale(_gameObject, maxScale, timeZeroMax).setOnComplete(AnimateMaxDefault).setIgnoreTimeScale(true);
        }

        public void AnimateMaxDefault()
        {
            LeanTween.scale(_gameObject, defaultScale, timeMaxDefault).setIgnoreTimeScale(true);
        }

        public void AnimateDefaultMax()
        {
            LeanTween.scale(_gameObject, maxScale, timeMaxDefault).setOnComplete(AnimateMaxZero).setIgnoreTimeScale(true);
        }

        public void AnimateMaxZero()
        {
            LeanTween.scale(_gameObject, Vector3.zero, timeZeroMax).setIgnoreTimeScale(true);
        }
    }
}
