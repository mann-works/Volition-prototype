using UnityEngine;

public class ClassroomManager : MonoBehaviour
{
    public static ClassroomManager Instance { get; private set; }

    [SerializeField] private ClassroomDatabase database;

    public bool LectureRequired { get; private set; }

    private System.DateTime _lastCompletedDate;

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
        System.DateTime currentDate = CalendarManager.Instance.CurrentDate.Date;

        if (_lastCompletedDate == currentDate)
        {
            ResetLectureFlags();
            return;
        }

        LessonData lesson = database.GetLesson(CalendarManager.Instance.CurrentDate); //[cite: 2]

        if (lesson == null)
        {
            ResetLectureFlags();
            return;
        }

        LectureRequired = true; //[cite: 2]
        GameManager.Instance.MustAttendLecture = true; //[cite: 2]

        Debug.Log("Lecture is required today.");
    }

    public bool TryStartLecture()
    {
        if (!LectureRequired)
            return false;

        LessonData lesson = database.GetLesson(CalendarManager.Instance.CurrentDate); //[cite: 2]

        if (lesson == null)
        {
            Debug.Log("No lesson today."); //[cite: 2]
            ResetLectureFlags();
            return false;
        }

        _lastCompletedDate = CalendarManager.Instance.CurrentDate.Date;
        ResetLectureFlags();

        Debug.Log("Starting Lecture via DialogUI");

        // Pass the data cleanly over to DialogUI instead of ClassroomUI[cite: 3]
        DialogUI.Instance.ShowPages(
            lesson.teacherName,
            lesson.pages,
            () => HandleLectureFinished(lesson)
        );

        return true;
    }

    private void HandleLectureFinished(LessonData lesson)
    {
        // Awards and cleanup logic transferred seamlessly from ClassroomUI[cite: 3]
        PlayerStats.Instance.AddXP(StatType.Knowledge, lesson.knowledgeReward); //[cite: 3]
        GameManager.Instance.SetState(GameState.Gameplay); //[cite: 3]

        if (lesson.quiz != null) //[cite: 3]
        {
            QuizUI.Instance.PlayQuiz(lesson.quiz); //[cite: 3]
        }
        else
        {
            CalendarManager.Instance.AdvanceTime(); //[cite: 3]
        }
    }

    private void ResetLectureFlags()
    {
        LectureRequired = false;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.MustAttendLecture = false; //[cite: 2]
        }
    }
}