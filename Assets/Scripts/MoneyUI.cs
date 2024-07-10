using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    private TextMeshProUGUI moneyText;

    private void Start()
    {
        moneyText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        moneyText.text = $"${PlayerStats.Money}";
    }
}
