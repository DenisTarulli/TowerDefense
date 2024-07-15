using System.Collections;
using TMPro;
using UnityEngine;

public class WavesSurvived : MonoBehaviour
{
    private TextMeshProUGUI wavesText;
    [SerializeField, Range(0.2f, 0.7f)] private float animationDelay;
    [SerializeField, Range(0.05f, 0.15f)] private float countUpTimeInterval;

    private void Awake()
    {
        wavesText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        wavesText.text = "0";
        int wave = 0;

        yield return new WaitForSeconds(animationDelay);

        while (wave < PlayerStats.Waves)
        {
            wave++;
            wavesText.text = wave.ToString();

            yield return new WaitForSeconds(countUpTimeInterval);
        }
    }
}
