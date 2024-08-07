using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color cantBuildColor;
    private Color startingColor;

    [SerializeField] private float yPositionOffset;
    private Vector3 positionOffset;
    
    [HideInInspector] public GameObject turret;
    [HideInInspector] public TurretScriptableObject currentTurret;
    [HideInInspector] public bool isUpgraded;

    private MeshRenderer meshRend;
    private BuildManager buildManager;

    private void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
        startingColor = meshRend.material.color;

        positionOffset = new(0f, yPositionOffset, 0f);

        buildManager = BuildManager.Instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild) return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    private void BuildTurret(TurretScriptableObject turretToBuild)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("You don't have enough money");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;

        Vector3 buildPosition = GetBuildPosition();

        GameObject newTurret = Instantiate(turretToBuild.prefab, buildPosition, Quaternion.identity);
        turret = newTurret;

        currentTurret = turretToBuild;

        GameObject buildParticles = Instantiate(turretToBuild.buildEffect, buildPosition, Quaternion.identity);
        Destroy(buildParticles, turretToBuild.buildEffectDuration);

        buildManager.DeselectTurret();
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < currentTurret.upgradeCost)
        {
            Debug.Log("You don't have enough money to upgrade");
            return;
        }

        PlayerStats.Money -= currentTurret.upgradeCost;

        Vector3 buildPosition = GetBuildPosition();
        Quaternion lastLookRotation = turret.GetComponent<Turret>().PartToRotate.rotation;

        Destroy(turret);

        GameObject newTurret = Instantiate(currentTurret.upgradedPrefab, buildPosition, Quaternion.identity);
        turret = newTurret;

        turret.GetComponent<Turret>().PartToRotate.rotation = lastLookRotation;

        GameObject upgradeParticles = Instantiate(currentTurret.upgradeEffect, buildPosition, Quaternion.identity);
        Destroy(upgradeParticles, currentTurret.upgradeEffectDuration);

        isUpgraded = true;
    }

    public void SellTurret()
    {
        int sellRevenue = GetSellRevenue();

        PlayerStats.Money += sellRevenue;

        GameObject sellParticles = Instantiate(currentTurret.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(sellParticles, currentTurret.sellEffectDuration);

        Destroy(turret);
        
        ResetValues();
    }

    private void ResetValues()
    {
        currentTurret = null;
        isUpgraded = false;
    }

    public int GetSellRevenue()
    {
        int sellRevenue = Mathf.CeilToInt(currentTurret.cost * currentTurret.sellPercentage);

        if (isUpgraded)
            sellRevenue += Mathf.CeilToInt(currentTurret.upgradeCost * currentTurret.sellPercentage);

        return sellRevenue;
    }

    private void OnMouseEnter()
    {
        if (!buildManager.CanBuild || EventSystem.current.IsPointerOverGameObject()) return;

        if (buildManager.HasEnoughMoney)
        {
            if (turret != null)
                meshRend.material.color = cantBuildColor;
            else
                meshRend.material.color = hoverColor;
        }            
        else
            meshRend.material.color = cantBuildColor;

    }

    private void OnMouseExit()
    {
        meshRend.material.color = startingColor;
    }
}
