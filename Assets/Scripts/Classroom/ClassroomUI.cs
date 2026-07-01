using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class ClassroomUI : MonoBehaviour
{
    public static ClassroomUI Instance { get; private set; }

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text teacherText;
    [SerializeField] private TMP_Text lectureText;
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject disableHud1;
    [SerializeField] private GameObject disableHud2;
    private LessonData currentLesson;
    private int pageIndex;

    private void Awake()
    {
        Instance = this;

        panel.SetActive(false);

        nextButton.onClick.AddListener(NextPage);
    }

    public void PlayLecture(LessonData lesson)
    {
        GameManager.Instance.SetState(GameState.Lecture);
        disableHud1.SetActive(false);
        disableHud2.SetActive(false);
        panel.SetActive(true);
        currentLesson = lesson;
        pageIndex = 0;

        teacherText.text = lesson.teacherName;
        lectureText.text = lesson.pages[0];

        panel.SetActive(true);
    }

    private void NextPage()
    {
        pageIndex++;

        if (pageIndex < currentLesson.pages.Length)
        {
            lectureText.text = currentLesson.pages[pageIndex];
            return;
        }

        FinishLecture();
    }

    private void FinishLecture()
    {
        panel.SetActive(false);
        disableHud1.SetActive(true);
        disableHud2.SetActive(true);

        PlayerStats.Instance.AddXP(
            StatType.Knowledge,
            currentLesson.knowledgeReward);

        GameManager.Instance.SetState(GameState.Gameplay);
        if (currentLesson.quiz != null)
        {
            QuizUI.Instance.PlayQuiz(currentLesson.quiz);
        }
        else
        {
            CalendarManager.Instance.AdvanceTime();
        }
    }
}