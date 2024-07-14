using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;

    [SerializeField] private GameObject ui;

    private const string IS_MENU = "MainMenu";

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
        SceneManager.LoadScene(IS_MENU);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }
}
