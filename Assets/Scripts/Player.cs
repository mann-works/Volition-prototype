using Unity.ProjectAuditor.Editor.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float _speed;
    private Vector2 _targetPosition;
    private Rigidbody2D _rigidbody;
    private Animator _animator;


    private Vector2 _lastDirection = Vector2.down;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

            _targetPosition = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
        }
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
}
