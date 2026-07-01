using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance { get; private set; }

    [Header("Fade")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeTime = 0.5f;
    [SerializeField] private float holdTime = 0.8f;
    [SerializeField] private GameObject blackImage;

    [Header("Transition Text")]
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private TMP_Text dateText;
    [SerializeField] private TMP_Text periodText;
    private void Awake()
    {
        Instance = this;
        blackImage.SetActive(true);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

    }

    private void Start()
    {
        RefreshDateText();
    }

    private void RefreshDateText()
    {
        DateTime date = CalendarManager.Instance.CurrentDate;

        dayText.text = date.ToString("dddd");
        dateText.text = date.ToString("MM/dd");
        periodText.text = CalendarManager.Instance.CurrentPeriod.ToString();
    }
    public void Transition(Action action)
    {
        StartCoroutine(TransitionRoutine(action));
    }

    private IEnumerator TransitionRoutine(Action action)
    {
        canvasGroup.blocksRaycasts = true;

        yield return Fade(0, 1);

        action?.Invoke();

        RefreshDateText();

        yield return new WaitForSeconds(holdTime);

        yield return Fade(1, 0);

        canvasGroup.blocksRaycasts = false;
    }

    private IEnumerator Fade(float from, float to)
    {
        float t = 0f;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, t / fadeTime);
            yield return null;
        }

        canvasGroup.alpha = to;
    }
}