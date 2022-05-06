using Game.Managers;
using UnityEngine;
using Game.Entities.Helpers;

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos

namespace Game.Entities
{
    public class Enemy : DestroyableEntity, IDamageable
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float timeToWaitBeforeMoving;

        [SerializeField] private Rigidbody2D rb2D;
        [SerializeField] private SpriteRenderer spRenderer;
        [SerializeField] private ParticleSystem trailParticles;
        [SerializeField] private ParticleSystem deathParticles;
        [SerializeField] private GameObject[] spawnables;

        private float _smallMovementAdjustment;

        protected override void Awake()
        {
            base.Awake();

            SetupEnemy();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void SetupEnemy()
        {
            spRenderer.color = ColorPalettesManager.Instance.GetRandomColor();
            trailParticles.startColor = new Color(spRenderer.color.r, spRenderer.color.g, spRenderer.color.b, 0.25f);
            DestroyYourself(3f);

            _smallMovementAdjustment = Random.Range(-0.1f, 0.1f);
        }

        private void Move()
        {
            timeToWaitBeforeMoving -= Time.fixedDeltaTime;
            if (timeToWaitBeforeMoving > 0f)
                return;

            rb2D.velocity = moveSpeed * Time.fixedDeltaTime * new Vector2(-1f, _smallMovementAdjustment);
        }

        public void TakeDamage()
        {
            Vector3 pos = transform.position;
            deathParticles.startColor = spRenderer.color;
            Instantiate(deathParticles, pos, Quaternion.identity);
            
            GameObject s = Instantiate(GetRandomSpawnable(), pos, Quaternion.identity);
            s.GetComponent<IDirectable>().SetDirection(Direction.Right);
            
            SoundManager.Instance.PlaySound(SoundType.Bomb, 1f);
            DestroyYourself(0f);
        }

        private GameObject GetRandomSpawnable()
        {
            return Random.Range(0, 100) < 99 ? spawnables[0] : spawnables[1];
        }
    }
}
