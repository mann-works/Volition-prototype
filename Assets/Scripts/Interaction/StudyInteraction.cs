using UnityEngine;

public class StudyInteraction : BaseInteraction
{
    protected override void ConfirmInteraction()
    {
        ApplyStatModifiers();

        StudyManager.Instance.StartStudy();
    }
}
