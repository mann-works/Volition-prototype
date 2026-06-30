using UnityEngine;
using static StatType;

public class SleepInteraction : MonoBehaviour
{
    
    private void OnMouseDown()
    {
        TestKnowledge();
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
    public void TestKnowledge()
    {
        PlayerStats.Instance.AddXP(StatType.Knowledge, 5);
        CalendarManager.Instance.AdvanceTime();

    }
}


