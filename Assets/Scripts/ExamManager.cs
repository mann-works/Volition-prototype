using System;
using UnityEngine;

public class ExamManager : MonoBehaviour
{
    public static ExamManager Instance { get; private set; }

    [SerializeField] private int requiredKnowledge = 100;
    [SerializeField] private int daysUntilExam = 7;
    private bool examTaken;

    private DateTime examDate;
    public DateTime ExamDate => examDate;

    private void Start()
    {
        examDate = CalendarManager.Instance.CurrentDate.AddDays(daysUntilExam);
        CalendarManager.Instance.OnCalendarChanged += CheckExamDate;
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
            DialogUI.Instance.Show(
                "System",
                "You failed the exam. Game Over!",
                () => GameManager.Instance.RestartGame()
            );
        }
    }
}