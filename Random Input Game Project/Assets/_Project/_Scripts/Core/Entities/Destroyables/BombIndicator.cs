using System.Collections;
using UnityEngine;

namespace Game.Entities
{
    public class BombIndicator : DestroyableEntity
    {
        [SerializeField] private GameObject bombPrefab;

        private Color _color;

        private Transform _transform;
        private SpriteRenderer _renderer;

        protected override void Awake()
        {
            base.Awake();

            _transform = transform;
            _renderer = GetComponent<SpriteRenderer>();

            _color = Utils.GetRandomColor();
            _renderer.color = new Color(_color.r, _color.g, _color.b, 0.05f);
        }

        private void Start()
        {
            StartCoroutine(nameof(SpawnBomb));
            Destroy(gameObject, 3.1f);
        }

        private IEnumerator SpawnBomb()
        {
            yield return new WaitForSeconds(3f);

            GameObject g = Instantiate(bombPrefab, _transform.position, _transform.rotation);
            g.GetComponent<Bomb>().SetColor(_color);
        }
    }
}