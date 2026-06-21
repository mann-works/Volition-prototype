using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightController : MonoBehaviour
{
    [SerializeField] private Light2D _globalLight;

    [Header("Light Settings")]
    [SerializeField] private Color _morningColor = new Color(1f, 0.95f, 0.8f);
    [SerializeField] private Color _afternoonColor = new Color(1f, 1f, 1f);
    [SerializeField] private Color _eveningColor = new Color(1f, 0.6f, 0.3f);
    [SerializeField] private Color _nightColor = new Color(0.1f, 0.1f, 0.3f);

    [SerializeField] private float _morningIntensity = 0.8f;
    [SerializeField] private float _afternoonIntensity = 1f;
    [SerializeField] private float _eveningIntensity = 0.6f;
    [SerializeField] private float _nightIntensity = 0.2f;

    void Start()
    {
        UpdateLight();
    }

    public void UpdateLight()
    {
        switch (TimeManager.Instance.CurrentTime)
        {
            case TimeManager.TimeOfDay.Morning:
                SetLight(_morningColor, _morningIntensity);
                break;
            case TimeManager.TimeOfDay.Afternoon:
                SetLight(_afternoonColor, _afternoonIntensity);
                break;
            case TimeManager.TimeOfDay.Evening:
                SetLight(_eveningColor, _eveningIntensity);
                break;
            case TimeManager.TimeOfDay.Night:
                SetLight(_nightColor, _nightIntensity);
                break;
        }
    }

    private void SetLight(Color color, float intensity)
    {
        _globalLight.color = color;
        _globalLight.intensity = intensity;
    }
}