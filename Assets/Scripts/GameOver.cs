using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wavesText;
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private string menuSceneName;

    private void OnEnable()
    {
        wavesText.text = PlayerStats.Waves.ToString();
    }

    public void PlayAgain()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
