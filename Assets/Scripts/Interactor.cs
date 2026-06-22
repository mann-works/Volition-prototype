using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    void Interact();
    void onTouchingPlayer();
    void notTouchingPlayer();
}

public class Interactor : MonoBehaviour
{
    private Player _player;
    private IInteractable _currentInteractable;
    private IInteractable _pendingInteractable;

    void Start()
    {
        _player = GetComponent<Player>();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (DialogueManager.Instance != null && DialogueManager.Instance.isDialogueActive)
            {
                DialogueManager.Instance.DisplayNextDialogueLine();
                return;
            }

            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(
                Mouse.current.position.ReadValue()
            );

            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);

            if (hit != null)
            {
                IInteractable interactable = hit.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    _pendingInteractable = interactable;
                    _player.MoveTo(hit.transform.position);
                    return;
                }
            }

            _pendingInteractable = null;
            _player.MoveTo(mouseWorldPos);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null)
        {
            _currentInteractable = interactable;
            _currentInteractable.onTouchingPlayer();

            if (interactable == _pendingInteractable)
            {
                _currentInteractable.Interact();
                _pendingInteractable = null;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null && interactable == _currentInteractable)
        {
            _currentInteractable.notTouchingPlayer();
            _currentInteractable = null;
        }
    }
}