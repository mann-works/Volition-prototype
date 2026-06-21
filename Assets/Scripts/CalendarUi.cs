using System;
using TMPro;
using UnityEngine;

public class CalenderUi : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text dayNameText;
    [SerializeField] private TMP_Text dateText;
    [SerializeField] private TMP_Text periodText;

    private void OnEnable()
    {
        if (CalendarManager.Instance != null)
        {
            CalendarManager.Instance.OnCalendarChanged += UpdateCalendar;
        }
    }

    private void Start()
    {
        if (CalendarManager.Instance != null)
        {
            UpdateCalendar(
                CalendarManager.Instance.CurrentDate,
                CalendarManager.Instance.CurrentPeriod);
        }
    }

    private void OnDisable()
    {
        if (CalendarManager.Instance != null)
        {
            CalendarManager.Instance.OnCalendarChanged -= UpdateCalendar;
        }
    }

    private void UpdateCalendar(DateTime date, CalendarManager.TimePeriod period)
    {

       
        dayNameText.text = date.ToString("dddd");

        dateText.text = date.ToString("MM/dd");

        periodText.text = period.ToString().ToUpper();
    }
}