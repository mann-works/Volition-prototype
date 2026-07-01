using System;
using UnityEngine;

public class ScheduleManager : MonoBehaviour
{
    [Serializable]
    public class DailySchedule
    {
        public DayOfWeek day;
        public LessonData lesson;
    }

    private void Start()
    {
        CalendarManager.Instance.OnCalendarChanged += OnTimeChanged;
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
                    "volition",
                    "A new lecture is available at the classroom today. You must attend it.",
                    () => GameManager.Instance.SetState(GameState.Gameplay)
                );
            }
        }
    }
}