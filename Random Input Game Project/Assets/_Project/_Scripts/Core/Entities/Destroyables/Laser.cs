using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    #pragma warning disable CS0618 // El tipo o el miembro están obsoletos

    public class Laser : DestroyableEntity
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private SpriteRenderer spriteRendererLeft;
        [SerializeField] private SpriteRenderer spriteRendererRight;
        [SerializeField] private ParticleSystem particlesLeft;
        [SerializeField] private ParticleSystem particlesRight;

        private Color _color;

        private void Start()
        {
            SoundManager.Instance.PlaySound(SoundType.Laser, Random.Range(0.1f, 0.2f));
            DestroyYourself(2f / 3f);
        }

        public void SetColor(Color color)
        {
            _color = color;
            spriteRenderer.color = _color;
            spriteRendererLeft.color = new Color(Mathf.Clamp01(_color.r - 0.3f), Mathf.Clamp01(_color.g - 0.3f), Mathf.Clamp01(_color.b - 0.3f));
            spriteRendererRight.color = new Color(Mathf.Clamp01(_color.r - 0.3f), Mathf.Clamp01(_color.g - 0.3f), Mathf.Clamp01(_color.b - 0.3f));
            particlesLeft.startColor = _color;
            particlesRight.startColor = _color;
        }
    }
}
