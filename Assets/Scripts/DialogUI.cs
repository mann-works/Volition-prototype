using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    public static DialogUI Instance { get; private set; }

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button continueButton;

    // HUD overlays to toggle off during lectures/dialogs
    [SerializeField] private GameObject disableHud1;
    [SerializeField] private GameObject disableHud2;

    private string speakerName;
    private string[] dialoguePages;
    private int pageIndex;
    private Action onFinish;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        panel.SetActive(false);
        continueButton.onClick.AddListener(ContinueDialogue);
    }

    // Keeps backwards compatibility for single line conversations[cite: 9]
    public void Show(string speaker, string message, Action finishCallback)
    {
        ShowPages(speaker, new string[] { message }, finishCallback);
    }

    // Handles multiple pages (like an entire classroom lecture)
    public void ShowPages(string speaker, string[] pages, Action finishCallback)
    {
        GameManager.Instance.SetState(GameState.Lecture); //[cite: 3, 9]

        if (disableHud1 != null) disableHud1.SetActive(false); //
        if (disableHud2 != null) disableHud2.SetActive(false); //

        speakerName = speaker;
        dialoguePages = pages;
        pageIndex = 0;
        onFinish = finishCallback;

        speakerText.text = speakerName; //[cite: 3, 9]
        dialogueText.text = dialoguePages[0]; //[cite: 3, 9]

        panel.SetActive(true); //[cite: 3, 9]
    }

    private void ContinueDialogue()
    {
        pageIndex++; //

        // If there are more pages left, display the next one[cite: 3]
        if (dialoguePages != null && pageIndex < dialoguePages.Length)
        {
            dialogueText.text = dialoguePages[pageIndex]; //[cite: 3]
            return;
        }

        // Otherwise, close up and finish[cite: 3]
        FinishDialogue();
    }

    private void FinishDialogue()
    {
        panel.SetActive(false); //[cite: 3, 9]

        if (disableHud1 != null) disableHud1.SetActive(true); //[cite: 3]
        if (disableHud2 != null) disableHud2.SetActive(true); //[cite: 3]

        onFinish?.Invoke(); //[cite: 9]
    }
}