using UnityEngine;

public interface IInteractable
{
    Transform InteractionPoint { get; }

    void Interact();
}