using UnityEngine;

public class InteractableStatChange : MonoBehaviour, IInteractable
{
    public enum StatType { Intelligence, Strength, Agility }

    [SerializeField] private StatType _statType;
    [SerializeField] private int _Amount = 3;

    private PlayerStats _playerStats;
    public void Interact()
    {
        if (_playerStats == null)
        {
            _playerStats = FindAnyObjectByType<PlayerStats>();
        }

        switch (_statType)
        {
            case StatType.Intelligence:
                _playerStats.AddIntelligence(_Amount);
                break;
            case StatType.Strength:
                _playerStats.AddStrength(_Amount);
                break;
            case StatType.Agility:
                _playerStats.AddAgility(_Amount);
                break;
        }
    }

    public void onTouchingPlayer() { }
    public void notTouchingPlayer() { }
}
