using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConfirmationUI : MonoBehaviour
{
    public static ConfirmationUI Instance { get; private set; }

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    public bool IsOpen => panel.activeSelf;
    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void Show(string message, Action onYes, Action onNo)
    {
        panel.SetActive(true);
        messageText.text = message;

        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        yesButton.onClick.AddListener(() =>
        {
            panel.SetActive(false);
            onYes?.Invoke();
        });

        noButton.onClick.AddListener(() =>
        {
            panel.SetActive(false);
            onNo?.Invoke();
        });
    }
}
