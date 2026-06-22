using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    public enum StatType { Intelligence, Strength, Agility, Focus }

    [SerializeField] private StatType _statType;
    [SerializeField] private int _amount = 3;
    [SerializeField] private DialogueCharacter _narrator;
    [SerializeField][TextArea(2, 5)] private string _dialogueLine;

    private PlayerStats _playerStats;

    public void Interact()
    {
        if (_statType == StatType.Focus)
        {
            if (TimeManager.Instance.CurrentTime != TimeManager.TimeOfDay.Night)
            {
                Debug.Log("Belum malam, tidak bisa tidur!");
                return;
            }

            if (_playerStats == null)
                _playerStats = FindAnyObjectByType<PlayerStats>();

            _playerStats.AddFocus(_amount);

            DialogueManager.Instance.StartDialogue(
                BuildDialogue(_dialogueLine),
                () =>
                {
                    ScreenFader.Instance.FadeTransition(() =>
                    {
                        TimeManager.Instance.Sleep();
                    });
                }
            );
            return;
        }

        if (TimeManager.Instance.CurrentTime == TimeManager.TimeOfDay.Night)
        {
            Debug.Log("Malam hari, tidak bisa beraktivitas!");
            return;
        }

        if (_playerStats == null)
            _playerStats = FindAnyObjectByType<PlayerStats>();

        switch (_statType)
        {
            case StatType.Intelligence:
                _playerStats.AddIntelligence(_amount);
                break;
            case StatType.Strength:
                _playerStats.AddStrength(_amount);
                break;
            case StatType.Agility:
                _playerStats.AddAgility(_amount);
                break;
        }

        DialogueManager.Instance.StartDialogue(
            BuildDialogue(_dialogueLine),
            () =>
            {
                ScreenFader.Instance.FadeTransition(() =>
                {
                    TimeManager.Instance.AdvanceTime();
                });
            }
        );
    }

    private Dialogue BuildDialogue(string text)
    {
        Dialogue dialogue = new Dialogue();
        dialogue.dialogueLines.Add(new DialogueLine
        {
            character = _narrator,
            line = text
        });
        return dialogue;
    }

    public void onTouchingPlayer() { }
    public void notTouchingPlayer() { }
}