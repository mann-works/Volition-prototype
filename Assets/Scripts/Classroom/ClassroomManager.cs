using UnityEngine;

public class ClassroomManager : MonoBehaviour
{
    public static ClassroomManager Instance { get; private set; }

    [SerializeField] private ClassroomDatabase database;

    public bool LectureRequired { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RequireLecture()
    {
        LectureRequired = true;
        Debug.Log("Lecture is required today.");
    }


    public bool TryStartLecture()
    {
        if (!LectureRequired)
            return false;

        LessonData lesson =
            database.GetLesson(CalendarManager.Instance.CurrentDate);

        if (lesson == null)
        {
            Debug.Log("No lesson today.");

            // No lesson today, allow normal study.
            LectureRequired = false;
            return false;
        }

        LectureRequired = false;

        Debug.Log("Starting Lecture");
        ClassroomUI.Instance.PlayLecture(lesson);

        return true;
    }
}