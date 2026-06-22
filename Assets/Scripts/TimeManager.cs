using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private GlobalLightController _lightController;

    public enum TimeOfDay { Morning, Afternoon, Evening, Night }

    public static TimeManager Instance { get; private set; }

    public int Year { get; private set; } = 2025;
    public int Month { get; private set; } = 4;
    public int Day { get; private set; } = 1;

    public TimeOfDay CurrentTime { get; private set; } = TimeOfDay.Morning;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AdvanceTime()
    {
        if (CurrentTime == TimeOfDay.Night)
        {
            Debug.Log("Malam hari, tidak bisa beraktivitas!");
            return;
        }

        CurrentTime++;
        Debug.Log($"{Day}/{Month}/{Year} - {CurrentTime}");
        _lightController.UpdateLight();
    }

    public void Sleep()
    {
        if (CurrentTime != TimeOfDay.Night)
        {
            Debug.Log("Belum malam, tidak bisa tidur!");
            return;
        }

        AdvanceDay(); 
        CurrentTime = TimeOfDay.Morning;
        Debug.Log($"=== {Day}/{Month}/{Year} - Selamat Pagi! ===");
        _lightController.UpdateLight();
    }

  
    private void AdvanceDay()
    {
        Day++;
        int daysInMonth = System.DateTime.DaysInMonth(Year, Month);
        if (Day > daysInMonth)
        {
            Day = 1;
            Month++;
            if (Month > 12)
            {
                Month = 1;
                Year++;
            }
        }
    }

    // ✅ Getter untuk UI
    public string GetCurrentDate() => $"{Day}/{Month}/{Year}";
    public string GetCurrentTime() => CurrentTime.ToString();
}