using UnityEngine;

public abstract class BaseInteraction : MonoBehaviour, IInteractable
{
    [Header("Interaction")]
    [SerializeField] private Transform interactionPoint;

    [Header("Confirmation")]
    [SerializeField] private string confirmationMessage = "Interact?";

    [Header("Stat Rewards")]
    [SerializeField] private StatModifier[] statModifiers;

    public Transform InteractionPoint => interactionPoint;

    protected virtual void Awake()
    {
        if (interactionPoint == null)
        {
            interactionPoint = transform.Find("InteractionPoint");

            if (interactionPoint == null)
            {
                Debug.LogWarning($"{name}: No InteractionPoint found, using object position.");
                interactionPoint = transform;
            }
        }
    }

    public void Interact()
    {
        ConfirmationUI.Instance.Show(
            confirmationMessage,
            ConfirmInteraction,
            CancelInteraction);
    }

    protected virtual void CancelInteraction()
    {
    }

    protected abstract void ConfirmInteraction();
    protected void ApplyStatModifiers()
    {
        foreach (var modifier in statModifiers)
        {
            PlayerStats.Instance.AddXP(modifier.statType, modifier.amount);
        }
    }
}
