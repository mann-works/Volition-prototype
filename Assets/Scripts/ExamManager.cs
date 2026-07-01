using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExamManager : MonoBehaviour
{
    public static ExamManager Instance { get; private set; }

    [SerializeField] private int requiredKnowledge = 100;
    [SerializeField] private int daysUntilExam = 7;
    private bool examTaken;

    private DateTime examDate;
    public DateTime ExamDate => examDate;
    public int DaysUntilExam => daysUntilExam;
    public int RequiredKnowledge => requiredKnowledge;

    private void Start()
    {
        examDate = CalendarManager.Instance.CurrentDate.AddDays(daysUntilExam);

        GameManager.Instance.SetState(GameState.Lecture);

        string welcomeMessage = $"Welcome to the semester! Your final exam is scheduled in {daysUntilExam} days. You must build up at least {requiredKnowledge} Knowledge to pass it. Be careful: if your Stress level reaches {PlayerStats.Instance.MaxStressLimit} or higher, you will collapse and fail immediately!";

        DialogUI.Instance.Show(
            "System",
            welcomeMessage,
            () =>
            {
                GameManager.Instance.SetState(GameState.Gameplay);
                CalendarManager.Instance.OnCalendarChanged += CheckExamDate;
                ScheduleManager.Instance.EnableScheduleTracking();
            }
        );
    }

    private void OnDestroy()
    {
        if (CalendarManager.Instance != null)
            CalendarManager.Instance.OnCalendarChanged -= CheckExamDate;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void CheckExamDate(DateTime date, CalendarManager.TimePeriod period)
    {
        if (examTaken)
            return;

        if (date.Date == examDate.Date && period == CalendarManager.TimePeriod.Morning)
        {
            StartExam();
        }
    }

    private void StartExam()
    {
        examTaken = true;

        int knowledge = PlayerStats.Instance.GetStat(StatType.Knowledge).XP;

        if (knowledge >= requiredKnowledge)
        {
            DialogUI.Instance.Show(
                "System",
                "Congratulations! You passed the exam.",
                () => GameManager.Instance.SetState(GameState.Gameplay)
            );
        }
        else
        {
            ConfirmationUI.Instance.Show(
                "You failed the exam! Restart game?",
                () => GameManager.Instance.RestartGame(),
                () => SceneManager.LoadScene(0)
            );
        }
    }
}