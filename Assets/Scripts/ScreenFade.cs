using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance;

    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeDuration = 0.5f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void FadeTransition(System.Action onMidPoint)
    {
        StartCoroutine(DoFade(onMidPoint));
    }

    private IEnumerator DoFade(System.Action onMidPoint)
    {
        // Fade gelap
        yield return StartCoroutine(Fade(0f, 1f));

        // Panggil advance time di tengah (saat layar gelap)
        onMidPoint?.Invoke();

        // Fade terang
        yield return StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        Color color = _fadeImage.color;

        while (elapsed < _fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(from, to, elapsed / _fadeDuration);
            _fadeImage.color = color;
            yield return null;
        }

        color.a = to;
        _fadeImage.color = color;
    }
}