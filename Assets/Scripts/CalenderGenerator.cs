using System;
using TMPro;
using UnityEngine;

public class CalenderGenerator : MonoBehaviour
{
    [SerializeField] private GameObject dayPrefab;
    [SerializeField] private Transform gridParent;

    private void Start()
    {
        GenerateCalendar();

        if (CalendarManager.Instance != null)
        {
            CalendarManager.Instance.OnCalendarChanged += OnCalendarChanged;
        }
    }

    private void OnDestroy()
    {
        if (CalendarManager.Instance != null)
        {
            CalendarManager.Instance.OnCalendarChanged -= OnCalendarChanged;
        }
    }

    private void OnCalendarChanged(DateTime date, CalendarManager.TimePeriod period)
    {
        GenerateCalendar();
    }

    private void GenerateCalendar()
    {
        // Clear previous calendar
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        DateTime current = CalendarManager.Instance.CurrentDate;

        int year = current.Year;
        int month = current.Month;
        int currentDay = current.Day;

        int daysInMonth = DateTime.DaysInMonth(year, month);

        for (int i = 1; i <= daysInMonth; i++)
        {
            GameObject cell = Instantiate(dayPrefab, gridParent);

            TMP_Text text = cell.GetComponentInChildren<TMP_Text>();
            text.text = i.ToString();

            // Highlight today's date (optional)
            if (i == currentDay)
            {
                text.color = Color.yellow;
                text.fontStyle = FontStyles.Bold;
            }
        }
    }
}
