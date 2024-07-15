using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;

    [SerializeField] private GameObject ui;
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private string menuSceneName;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
            Time.timeScale = 1f;
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }
}
