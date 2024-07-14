using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node target;

    [SerializeField] private GameObject ui;
    [SerializeField] private TextMeshProUGUI upgradeCost;
    [SerializeField] private TextMeshProUGUI sellRevenue;
    [SerializeField] private Button upgradeButton;

    public void SetTarget(Node targetToSet)
    {
        target = targetToSet;

        transform.position = target.GetBuildPosition();

        SetButtonValues();
    }

    private void SetButtonValues()
    {
        if (!target.isUpgraded)
        {
            upgradeCost.text = $"${target.currentTurret.upgradeCost}";
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.interactable = false;
            upgradeCost.text = "MAX";
        }

        sellRevenue.text = $"${target.GetSellRevenue()}";

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.Instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.Instance.DeselectNode();
    }
}
