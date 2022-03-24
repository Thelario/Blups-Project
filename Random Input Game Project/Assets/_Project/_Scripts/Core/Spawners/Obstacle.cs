using UnityEngine;

namespace Game.Spawnners
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        private Direction _direction;
        private Vector2 _moveDirection;

        private Rigidbody2D _rb2D;
        private Transform _transform;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
            _renderer = _transform.GetChild(0).GetComponent<SpriteRenderer>();

            _direction = Direction.Down;
            _moveDirection = GetMoveDirection();

            _transform.localScale = GetRandomSize();
            _renderer.color = GetRandomColor();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _rb2D.velocity = Time.fixedDeltaTime * moveSpeed * _moveDirection;
        }

        private Vector3 GetRandomSize()
        {
            return new Vector3(Random.Range(_transform.localScale.x - 0.5f, _transform.localScale.x + 0.5f), 
                               Random.Range(_transform.localScale.y - 0.5f, _transform.localScale.y + 0.5f), 0f);
        }

        private Color GetRandomColor()
        {
            return new Color(1f, 1f, Random.Range(0f, 1f));
        }

        private Vector3 GetMoveDirection()
        {
            switch(_direction)
            {
                case Direction.Right:
                    return Vector3.right;
                case Direction.Left:
                    return Vector3.left;
                case Direction.Up:
                    return Vector3.up;
                case Direction.Down:
                    return Vector3.down;
                default:
                    Debug.LogError("Error with direction in Obstacle");
                    return Vector3.zero;
            }
        }
    }
}
