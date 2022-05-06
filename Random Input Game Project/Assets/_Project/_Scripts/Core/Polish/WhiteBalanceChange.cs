using System.Collections;
using Game.Managers;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;

namespace Game.Polish
{
    public class WhiteBalanceChange : MonoBehaviour
    {
        [Header("Fields")]
        [SerializeField] private float timeBetweenChanges;
        [SerializeField] private bool useWhiteBalanceChange;
        
        [SerializeField] private Vector2[] whiteBalanceValues;

        [Header("References")]
        [SerializeField] private Volume volume;

        private Vector2 _previousValues = Vector2.zero;
        
        private WhiteBalance _whiteBalance;

        private IEnumerator Start()
        {
            if (!useWhiteBalanceChange)
                yield break;
            
            volume.profile.TryGet(out _whiteBalance);
            yield return StartChangingValues();
        }

        private IEnumerator StartChangingValues()
        {
            Vector2 values;
            
            while (true)
            {
                values = GetRandomValue();
                _whiteBalance.temperature.value = values.x;
                _whiteBalance.tint.value = values.y;
                SoundManager.Instance.PlaySound(SoundType.Danger);
                
                yield return new WaitForSeconds(timeBetweenChanges);
            }
        }

        private Vector2 GetRandomValue()
        {
            int n;
            do {
                n = Random.Range(0, whiteBalanceValues.Length);
            } while (_previousValues == whiteBalanceValues[n]);

            _previousValues = whiteBalanceValues[n];
            return whiteBalanceValues[n];
        }
    }
}
