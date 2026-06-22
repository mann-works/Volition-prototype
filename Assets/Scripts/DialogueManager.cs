using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> _lines;
    private System.Action _onDialogueEnd;
    private bool _isTyping = false;

    public bool isDialogueActive = false;
    public float typingSpeed = 0.02f;

    public Animator animator;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _lines = new Queue<DialogueLine>();
    }

    void Start()
    {
        animator.Play("Hide");
    }

    public void StartDialogue(Dialogue dialogue, System.Action onEnd = null)
    {
        _onDialogueEnd = onEnd;
        isDialogueActive = true;

        animator.Play("DialogueIn");
        _lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
            _lines.Enqueue(dialogueLine);

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (_isTyping)
        {
            StopAllCoroutines();
            dialogueArea.text = _currentLine;
            _isTyping = false;
            return;
        }

        if (_lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = _lines.Dequeue();

        characterName.text = currentLine.character.name;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    private string _currentLine = "";

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        _isTyping = true;
        _currentLine = dialogueLine.line;
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        _isTyping = false;
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        animator.Play("Hide");
        _onDialogueEnd?.Invoke();
        _onDialogueEnd = null;
    }
}