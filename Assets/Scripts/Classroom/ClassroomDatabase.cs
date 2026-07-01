using UnityEngine;

public class ClassroomDatabase : MonoBehaviour
{
    [SerializeField] private LessonData[] lessons;

    public LessonData GetLesson(System.DateTime date)
    {
        Debug.Log("Current Date : " + date);

        foreach (var lesson in lessons)
        {
            Debug.Log("Lesson Date : " + lesson.date);

            if (lesson.date.Equals (date))
            {
                Debug.Log("Lesson Found!");
                return lesson;
            }
        }

        Debug.Log("Lesson NOT Found");
        return null;
    }
}
