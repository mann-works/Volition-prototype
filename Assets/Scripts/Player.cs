using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float _speed;
    private Vector2 _targetPosition;
    private Rigidbody2D _rigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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

        Vector2 newPosition = Vector2.MoveTowards(
            currentPosition,
            _targetPosition,
            _speed * Time.fixedDeltaTime
        );

        _rigidbody.MovePosition(newPosition);
    }
}
