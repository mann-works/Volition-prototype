using UnityEngine;

public class StudyInteraction : BaseInteraction
{
    public override void Interact()
    {
        if (ClassroomManager.Instance.LectureRequired)
        {
            ClassroomManager.Instance.TryStartLecture();
            return;
        }

        base.Interact();
    }

    protected override void ConfirmInteraction()
    {
        ApplyStatModifiers();
        CalendarManager.Instance.AdvanceTime();

    }
}
