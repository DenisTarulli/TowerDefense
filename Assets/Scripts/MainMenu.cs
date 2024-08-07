using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string levelToLoad;
    [SerializeField] private SceneFader sceneFader;

    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            PlayerPrefs.SetInt("levelReached", 1);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }
}
