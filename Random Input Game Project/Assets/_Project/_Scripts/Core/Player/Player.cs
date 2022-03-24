using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

namespace Game.Player
{
    public enum MoveState { Dir4_Precise, Dir4_Imprecise, UpDownAuto, LeftRightAuto, UpDownGravityOnPositive, UpDownGravityOnNegative, LeftRightInclined_1, LeftRightInclined_2 }

    public class Player : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        private MoveState _moveState = MoveState.Dir4_Precise;
        private int _moveStateCount;

        private float _horizontal;
        private float _vertical;       
        
        private float _horizontalRaw;
        private float _verticalRaw;

        private Rigidbody2D _rb2D;

        private void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();

            _moveStateCount = Enum.GetValues(typeof(MoveState)).Length;
        }

        private void Update()
        {
            GetChangeStateInput();

            GetMoveInput();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void GetChangeStateInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ChangeStateRandomly();
        }

        private void GetMoveInput()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");

            _horizontalRaw = Input.GetAxisRaw("Horizontal");
            _verticalRaw = Input.GetAxisRaw("Vertical");
        }

        private void Move()
        {
            switch(_moveState)
            {
                case MoveState.Dir4_Precise:
                    Move_Dir4_Precise();
                    break;
                case MoveState.Dir4_Imprecise:
                    Move_Dir4_Imprecise();
                        break;
                case MoveState.UpDownAuto:
                    Move_UpDownAuto();
                    break;
                case MoveState.LeftRightAuto:
                    Move_LeftRightAuto();
                    break;
                case MoveState.UpDownGravityOnPositive:
                case MoveState.UpDownGravityOnNegative:
                case MoveState.LeftRightInclined_1:
                case MoveState.LeftRightInclined_2:
                    break;
            }
        }

        private void Move_Dir4_Precise()
        {
            _rb2D.velocity = Time.fixedDeltaTime * moveSpeed * new Vector2(_horizontalRaw, _verticalRaw);
        }

        private void Move_Dir4_Imprecise()
        {
            _rb2D.velocity = Time.fixedDeltaTime * moveSpeed * new Vector2(_horizontal * .75f, _vertical * .75f);
        }

        private void Move_UpDownAuto()
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            _rb2D.velocity = Time.fixedDeltaTime * moveSpeed * new Vector2(_horizontalRaw, 0f);
        }

        private void Move_LeftRightAuto()
        {
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
            _rb2D.velocity = Time.fixedDeltaTime * moveSpeed * new Vector2(0f, _verticalRaw);
        }

        public void ChangeState(MoveState newState) { _moveState = newState; }

        private void ChangeStateRandomly() 
        {
            int prev = (int)_moveState;

            int n;
            do {
                n = Random.Range(0, 4);
            } while (n == prev);

            ChangeState((MoveState)n);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Obstacle"))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
