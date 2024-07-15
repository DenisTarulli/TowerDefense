using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private AnimationCurve animationCurve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = animationCurve.Evaluate(t);
            fadeImage.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

    private IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = animationCurve.Evaluate(t);
            fadeImage.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }
}
