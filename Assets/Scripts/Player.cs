using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed;
    private Vector2 _targetPosition;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
<<<<<<< HEAD
=======


    private Vector2 _lastDirection = Vector2.down;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _targetPosition = transform.position;
    }

    public void MoveTo(Vector2 worldPosition)
    {
        _targetPosition = worldPosition;
    }

    private void FixedUpdate()
    {
        Vector2 currentPosition = _rigidbody.position;
        Vector2 direction = (_targetPosition - currentPosition).normalized;

        Vector2 newPosition = Vector2.MoveTowards(
            currentPosition,
            _targetPosition,
            _speed * Time.fixedDeltaTime
        );

        _rigidbody.MovePosition(newPosition);

        bool isWalking = Vector2.Distance(currentPosition, _targetPosition) > 0.01f;
        _animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            _animator.SetFloat("InputX", direction.x);
            _animator.SetFloat("InputY", direction.y);
            _lastDirection = direction;
        }

        _animator.SetFloat("LastInputX", _lastDirection.x);
        _animator.SetFloat("LastInputY", _lastDirection.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _targetPosition = _rigidbody.position;
    }
}