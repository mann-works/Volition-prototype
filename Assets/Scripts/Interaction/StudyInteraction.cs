using UnityEngine;

public class StudyInteraction : BaseInteraction
{
    protected override void ConfirmInteraction()
    {
        if (ClassroomManager.Instance.TryStartLecture())
            return;

        ApplyStatModifiers();
        CalendarManager.Instance.AdvanceTime();
    }
}
