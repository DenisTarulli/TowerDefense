using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string levelToLoad = "MainLevel";
    [SerializeField] private SceneFader sceneFader;

    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }
}
