using System.Collections;
using UnityEngine;

namespace Game.Entities
{
    public class ObjectIndicator : DestroyableEntity
    {
        [SerializeField] private GameObject objectPrefab;

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
            StartCoroutine(nameof(SpawnObject));
            Destroy(gameObject, 3.1f);
        }

        private IEnumerator SpawnObject()
        {
            yield return new WaitForSeconds(3f);

            GameObject g = Instantiate(objectPrefab, _transform.position, _transform.rotation);
            g.GetComponent<Bomb>().SetColor(_color);
        }
    }
}