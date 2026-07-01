using UnityEngine;

public class SleepInteraction : BaseInteraction
{
    protected override void ConfirmInteraction()
    {
        ApplyStatModifiers(); 
        CalendarManager.Instance.AdvanceTime();
    }
}