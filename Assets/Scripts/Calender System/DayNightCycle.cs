using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    [Header("Global Light")]
    [SerializeField] private Light2D globalLight;

    [Header("Lighting")]
    [SerializeField] private Color morningColor = new Color(1f, 0.95f, 0.85f);
    [SerializeField] private Color afternoonColor = Color.white;
    [SerializeField] private Color eveningColor = new Color(1f, 0.65f, 0.45f);
    [SerializeField] private Color nightColor = new Color(0.2f, 0.25f, 0.5f);

    [SerializeField] private float morningIntensity = 0.8f;
    [SerializeField] private float afternoonIntensity = 1.0f;
    [SerializeField] private float eveningIntensity = 0.6f;
    [SerializeField] private float nightIntensity = 0.25f;

    private void Start()
    {
        CalendarManager.Instance.OnCalendarChanged += UpdateLighting;

        UpdateLighting(
            CalendarManager.Instance.CurrentDate,
            CalendarManager.Instance.CurrentPeriod);
    }

    private void OnDestroy()
    {
        if (CalendarManager.Instance != null)
            CalendarManager.Instance.OnCalendarChanged -= UpdateLighting;
    }

    private void UpdateLighting(System.DateTime date, CalendarManager.TimePeriod period)
    {
        switch (period)
        {
            case CalendarManager.TimePeriod.Morning:
                ApplyLighting(morningColor, morningIntensity);
                break;

            case CalendarManager.TimePeriod.Afternoon:
                ApplyLighting(afternoonColor, afternoonIntensity);
                break;

            case CalendarManager.TimePeriod.Evening:
                ApplyLighting(eveningColor, eveningIntensity);
                break;

            case CalendarManager.TimePeriod.Night:
                ApplyLighting(nightColor, nightIntensity);
                break;
        }
    }

    private void ApplyLighting(Color color, float intensity)
    {
        globalLight.color = color;
        globalLight.intensity = intensity;
    }
}