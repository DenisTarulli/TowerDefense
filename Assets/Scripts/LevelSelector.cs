using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;

    [SerializeField] private GameObject buttonGrid;
    [SerializeField] private Button[] levelButtons;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        GetButtons(levelReached);
    }

    public void SelectLevel(string levelName)
    {
        sceneFader.FadeTo(levelName);
    }

    private void GetButtons(int maxLevelReached)
    {
        int buttonsAmount = buttonGrid.transform.childCount;

        levelButtons = new Button[buttonsAmount];

        for (int i = 0; i < buttonsAmount; i++)
        {
            levelButtons[i] = buttonGrid.transform.GetChild(i).GetComponent<Button>();

            if (i + 1 > maxLevelReached)
                levelButtons[i].interactable = false;
        }
    }
}
