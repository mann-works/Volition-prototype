using UnityEngine;

public class SleepInteraction : MonoBehaviour
{
    
    private void OnMouseDown()
    {
        Sleep();
    }

    public void Sleep()
    {
        Debug.Log("Sleeping...");

        if (CalendarManager.Instance != null)
        {
            CalendarManager.Instance.AdvanceTime();
            Debug.Log("Time Advanced!");
        }
        else
        {
            Debug.LogError("CalendarManager tidak ditemukan!");
        }
    }
}


