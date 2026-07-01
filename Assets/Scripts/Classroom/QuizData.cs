using UnityEngine;

[CreateAssetMenu(menuName = "Classroom/Quiz")]
public class QuizData : ScriptableObject
{
    [TextArea]
    public string question;

    public string[] choices = new string[3];

    [Range(0, 2)]
    public int correctAnswer;

    [Header("Feedback")]
    [TextArea]
    public string correctDialogue;

    [TextArea]
    public string wrongDialogue;

    public int rewardCorrect = 3;
}