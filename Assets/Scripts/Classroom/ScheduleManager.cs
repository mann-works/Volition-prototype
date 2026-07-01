using System;
using UnityEngine;

public class ScheduleManager : MonoBehaviour
{
    public static ScheduleManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void EnableScheduleTracking()
    {
        CalendarManager.Instance.OnCalendarChanged += OnTimeChanged;
        OnTimeChanged(CalendarManager.Instance.CurrentDate, CalendarManager.Instance.CurrentPeriod);
    }

    private void OnDestroy()
    {
        if (CalendarManager.Instance != null)
            CalendarManager.Instance.OnCalendarChanged -= OnTimeChanged;
    }

    private void OnTimeChanged(DateTime date, CalendarManager.TimePeriod period)
    {
        if (period == CalendarManager.TimePeriod.Morning)
        {
            ClassroomManager.Instance.RequireLecture();

            if (ClassroomManager.Instance.LectureRequired)
            {
                DialogUI.Instance.Show(
                    "System",
                    "A new lecture is available at the classroom today. You must attend it.",
                    () => GameManager.Instance.SetState(GameState.Gameplay)
                );
            }
        }
    }
}