using System.Collections;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; }

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator FadeOut()
    {
        yield return Fade(0, 1);
    }

    public IEnumerator FadeIn()
    {
        yield return Fade(1, 0);
    }

    private IEnumerator Fade(float from, float to)
    {
        float t = 0;

        canvasGroup.blocksRaycasts = true;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            canvasGroup.alpha =
                Mathf.Lerp(from, to, t / fadeDuration);

            yield return null;
        }

        canvasGroup.alpha = to;

        canvasGroup.blocksRaycasts = to > 0;
    }
}