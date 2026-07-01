using UnityEngine;

public class ClassroomManager : MonoBehaviour
{
    public static ClassroomManager Instance { get; private set; }

    [SerializeField] private ClassroomDatabase database;

    private void Awake()
    {
        Instance = this;
    }

    public void StartLecture()
    {
        LessonData lesson =
            database.GetLesson(CalendarManager.Instance.CurrentDate);

        if (lesson == null)
        {
            Debug.Log("No lesson today");
            return;
        }
        Debug.Log("Start Lecture");
        ClassroomUI.Instance.PlayLecture(lesson);
    }
}
