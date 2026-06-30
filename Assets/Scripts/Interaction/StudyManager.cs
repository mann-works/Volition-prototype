using UnityEngine;

public class StudyManager : MonoBehaviour
{
    public static StudyManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void StartStudy()
    {

        CalendarManager.Instance.AdvanceTime();
    }
}
