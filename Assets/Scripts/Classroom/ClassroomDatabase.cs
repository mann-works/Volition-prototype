using UnityEngine;

public class ClassroomDatabase : MonoBehaviour
{
    [SerializeField] private LessonData[] lessons;

    public LessonData GetLesson(System.DateTime date)
    {

        foreach (var lesson in lessons)
        {

            if (lesson.date.Equals (date))
            {
                Debug.Log("Lesson Found!");
                return lesson;
            }
        }

        return null;
    }
}
