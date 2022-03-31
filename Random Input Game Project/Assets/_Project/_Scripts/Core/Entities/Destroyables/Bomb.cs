using UnityEngine;

namespace Game.Entities
{
    #pragma warning disable CS0618 // El tipo o el miembro están obsoletos

    public class Bomb : DestroyableEntity
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ParticleSystem particles;

        private Color _color;

        private void Start()
        {
            DestroyYourself(2f / 3f);
        }

        public void SetColor(Color color)
        {
            _color = color;
            spriteRenderer.color = _color;
            particles.startColor = _color;
        }
    }
}
