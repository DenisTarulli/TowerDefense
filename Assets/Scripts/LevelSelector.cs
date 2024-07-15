using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;

    public void SelectLevel(string levelName)
    {
        sceneFader.FadeTo(levelName);
    }
}
