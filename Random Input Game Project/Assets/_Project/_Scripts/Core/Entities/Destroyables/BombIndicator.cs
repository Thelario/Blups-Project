using System.Collections;
using UnityEngine;

namespace Game.Entities
{
    public class BombIndicator : ObjectIndicator
    {
        protected override IEnumerator SpawnObject()
        {
            yield return new WaitForSeconds(3f);

            GameObject g = Instantiate(objectPrefab, _transform.position, _transform.rotation);
            g.GetComponent<Bomb>().SetColor(_color);
        }
    }
}
