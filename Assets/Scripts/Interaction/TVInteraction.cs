using UnityEngine;

public class TVInteraction : BaseInteraction
{
    protected override void ConfirmInteraction()
    {
        ApplyStatModifiers();
        CalendarManager.Instance.AdvanceTime();
    }
}
