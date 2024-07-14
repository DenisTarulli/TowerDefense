using TMPro;
using UnityEngine;

public class WavesUI : MonoBehaviour
{
    private TextMeshProUGUI wavesText;

    private void Start()
    {
        wavesText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        wavesText.text = $"Wave: {PlayerStats.Waves}";
    }
}
