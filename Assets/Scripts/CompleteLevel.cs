using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private string menuSceneName;

    [SerializeField] private string nextLevel;
    [SerializeField] private int levelToUnlock;

    private void OnEnable()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
    }

    public void NextLevel()
    {
        sceneFader.FadeTo(nextLevel);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
