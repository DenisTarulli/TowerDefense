using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wavesText;
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private string menuSceneName;

    [SerializeField, Range(0.2f, 0.7f)] private float animationDelay; 
    [SerializeField, Range(0.05f, 0.15f)] private float countUpTimeInterval;


    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    public void PlayAgain()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
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
