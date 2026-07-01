using UnityEngine;

public class QuizUI : MonoBehaviour
{
    public static QuizUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void ShowQuiz(QuizData quiz)
    {
        Debug.Log("Quiz Starts!");

        CalendarManager.Instance.AdvanceTime();
    }
}