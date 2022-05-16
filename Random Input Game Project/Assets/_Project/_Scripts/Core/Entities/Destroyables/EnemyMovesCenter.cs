using Game.Entities.Helpers;
using Game.Managers;
using UnityEngine;

#pragma warning disable CS0618

namespace Game.Entities
{
    public class EnemyMovesCenter : DestroyableEntity, IDamageable
    {
        [SerializeField] private float moveSpeed;

        [SerializeField] private Rigidbody2D rb2D;
        [SerializeField] private Transform thisTransform;
        [SerializeField] private SpriteRenderer spRenderer;
        [SerializeField] private ParticleSystem trailParticles;
        [SerializeField] private ParticleSystem deathParticles;

        protected override void Awake()
        {
            base.Awake();

            Setup();
        }

        private void Start()
        {
            Rotate();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Setup()
        {
            spRenderer.color = ColorPalettesManager.Instance.GetRandomColor();
            trailParticles.startColor = new Color(spRenderer.color.r, spRenderer.color.g, spRenderer.color.b, 0.25f);
        }

        private void Rotate()
        {
            Vector2 direction = Vector3.zero - thisTransform.position;
            thisTransform.up = direction.normalized;
        }

        private void Move()
        {
            Vector2 direction = Vector3.zero - thisTransform.position;
            rb2D.velocity = moveSpeed * Time.fixedDeltaTime * direction.normalized;
        }

        public void TakeDamage()
        {
            SoundManager.Instance.PlaySound(SoundType.PlayerObstacleHit);
                
            deathParticles.startColor = spRenderer.color;
            Instantiate(deathParticles, transform.position, Quaternion.identity);
                
            DestroyYourself(0f);
        }
    }
}
