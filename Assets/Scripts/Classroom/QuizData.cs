using UnityEngine;

[CreateAssetMenu(menuName = "Classroom/Quiz")]
public class QuizData : ScriptableObject
{
    public string question;

    public string[] choices;

    public int correctAnswer;

    public int rewardCorrect = 10;
    public int rewardWrong = 3;
}