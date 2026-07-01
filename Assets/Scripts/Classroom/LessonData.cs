using UnityEngine;

[CreateAssetMenu(menuName = "Classroom/Lesson")]
public class LessonData : ScriptableObject
{
    public GameDate date;

    public string teacherName;

    [TextArea]
    public string[] pages;

    public QuizData quiz;

    public int knowledgeReward;
}