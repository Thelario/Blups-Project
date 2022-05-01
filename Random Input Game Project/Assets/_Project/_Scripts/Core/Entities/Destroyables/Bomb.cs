using Game.Managers;
using System.Collections;
using UnityEngine;

namespace Game.Entities
{
    #pragma warning disable CS0618 // El tipo o el miembro estan obsoletos

    public class Bomb : DestroyableEntity
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private Transform bombTransform;

        private Color _color;
        private Vector3 _scale;

        private readonly float _time = 2f / 3f;

        public void SetupBomb(Color color, Vector3 scale)
        {
            _color = color;
            spriteRenderer.color = _color;
            particles.startColor = _color;
            _scale = scale;
            transform.localScale = Vector3.zero;

            StartCoroutine(nameof(AnimateBomb));
        }

        private IEnumerator AnimateBomb()
        {
            LeanTween.scale(gameObject, _scale, _time);

            yield return new WaitForSeconds(_time);

            SoundManager.Instance.PlaySound(SoundType.Bomb, Random.Range(0.15f, 0.25f));
            LeanTween.scale(gameObject, Vector3.zero, 0.15f);

            yield return new WaitForSeconds(0.15f);

            Destroy(gameObject);
        }
    }
}
