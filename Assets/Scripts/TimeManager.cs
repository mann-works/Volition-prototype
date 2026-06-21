using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private GlobalLightController _lightController;

    public enum TimeOfDay { Morning, Afternoon, Evening, Night }

    public static TimeManager Instance { get; private set; }

    public int CurrentDay { get; private set; } = 1;
    public TimeOfDay CurrentTime { get; private set; } = TimeOfDay.Morning;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AdvanceTime()
    {
        _lightController.UpdateLight();

        if (CurrentTime == TimeOfDay.Night)
        {
            return;
        }

        CurrentTime++;
        Debug.Log($"Hari {CurrentDay} - {CurrentTime}");

        _lightController.UpdateLight();
            
    }

    public void Sleep()
    {
        if (CurrentTime != TimeOfDay.Night)
        {
            return;
        }

        CurrentDay++;
        CurrentTime = TimeOfDay.Morning;
        _lightController.UpdateLight();
    }
}