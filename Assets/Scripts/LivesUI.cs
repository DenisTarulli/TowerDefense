using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    private TextMeshProUGUI livesText;

    private void Start()
    {
        livesText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        livesText.text = $"LIVES: {PlayerStats.Lives.ToString("D2")}";
    }
}
