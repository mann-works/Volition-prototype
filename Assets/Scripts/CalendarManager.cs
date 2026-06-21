using System;
using UnityEngine;

public class CalendarManager : MonoBehaviour
{
    public static CalendarManager Instance { get; private set; }

    public enum TimePeriod
    {
        Morning,
        Afternoon,
        Evening,
        Night
    }

    public DateTime CurrentDate { get; private set; }
    public TimePeriod CurrentPeriod { get; private set; }

    public event Action<DateTime, TimePeriod> OnCalendarChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        CurrentDate = new DateTime(2020, 06, 11);
        CurrentPeriod = TimePeriod.Morning;
    }

    private void Start()
    {
        NotifyCalendarChanged();
    }

    public void AdvanceTime()
    {
        switch (CurrentPeriod)
        {
            case TimePeriod.Morning:
                CurrentPeriod = TimePeriod.Afternoon;
                break;

            case TimePeriod.Afternoon:
                CurrentPeriod = TimePeriod.Evening;
                break;

            case TimePeriod.Evening:
                CurrentPeriod = TimePeriod.Night;
                break;

            case TimePeriod.Night:
                CurrentPeriod = TimePeriod.Morning;
                CurrentDate = CurrentDate.AddDays(1);
                break;
        }

        NotifyCalendarChanged();
    }

    private void NotifyCalendarChanged()
    {
        OnCalendarChanged?.Invoke(CurrentDate, CurrentPeriod);
    }

}
