using Game.Managers;
using System.Collections;
using UnityEngine;

namespace Game.Entities
{
    public abstract class ObjectIndicator : DestroyableEntity
    {
        [SerializeField] protected GameObject objectPrefab;

        protected Color _color;

        protected Transform _transform;
        protected SpriteRenderer _renderer;

        protected override void Awake()
        {
            base.Awake();

            _transform = transform;
            _renderer = GetComponent<SpriteRenderer>();

            _color = ColorPalettesManager.Instance.GetRandomColor();
            _renderer.color = new Color(_color.r, _color.g, _color.b, 0.05f);
        }

        protected void Start()
        {
            StartCoroutine(nameof(SpawnObject));
        }

        protected abstract IEnumerator SpawnObject();
    }
}