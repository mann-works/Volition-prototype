using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public int year = 2025;
    public int month = 4;
    public int day = 11;

    public DayPeriod currentPeriod = DayPeriod.Morning;

    public enum DayPeriod
    {
        Morning,
        Afternoon,
        Evening,
        Night
    }

    public void AdvanceTime()
    {
        switch (currentPeriod)
        {
            case DayPeriod.Morning:
                currentPeriod = DayPeriod.Afternoon;
                break;

            case DayPeriod.Afternoon:
                currentPeriod = DayPeriod.Evening;
                break;

            case DayPeriod.Evening:
                currentPeriod = DayPeriod.Night;
                break;

            case DayPeriod.Night:
                currentPeriod = DayPeriod.Morning;
                AdvanceDay();
                break;
        }
    }


    private void AdvanceDay()
    {
        day++;

        int daysInMonth = System.DateTime.DaysInMonth(year, month);

        if (day > daysInMonth)
        {
            day = 1;
            month++;

            if (month > 12)
            {
                month = 1;
                year++;
            }
        }
    }
    public void Study()
    {

        AdvanceTime();
    }
}
