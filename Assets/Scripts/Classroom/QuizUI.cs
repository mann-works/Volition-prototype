using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    public static QuizUI Instance { get; private set; }

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private Button[] answerButtons;
    [SerializeField] private TMP_Text[] answerTexts;
    [SerializeField] private GameObject disableHud1;
    [SerializeField] private GameObject disableHud2;

    private QuizData currentQuiz;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        panel.SetActive(false);
    }

    public void PlayQuiz(QuizData quiz)
    {
        currentQuiz = quiz;

        if (disableHud1 != null) disableHud1.SetActive(false);
        if (disableHud2 != null) disableHud2.SetActive(false);

        panel.SetActive(true);

        GameManager.Instance.SetState(GameState.Quiz);
        questionText.text = quiz.question;

        for (int i = 0; i < answerTexts.Length; i++)
        {
            answerTexts[i].text = quiz.choices[i];
            int index = i;

            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => Submit(index));
        }
    }

    private void Submit(int answer)
    {
        bool correct = answer == currentQuiz.correctAnswer;
        string feedbackMessage;

        if (correct)
        {
            PlayerStats.Instance.AddXP(StatType.Knowledge, currentQuiz.rewardCorrect);
            feedbackMessage = currentQuiz.correctDialogue;
            Debug.Log("Correct!");
        }
        else
        {
            feedbackMessage = currentQuiz.wrongDialogue;
            Debug.Log("Wrong!");
        }

        panel.SetActive(false);

        DialogUI.Instance.Show(
            "System",
            feedbackMessage,
            () => FinishQuizSequence()
        );
    }

    private void FinishQuizSequence()
    {
        GameManager.Instance.SetState(GameState.Gameplay);
        CalendarManager.Instance.AdvanceTime();
    }
}