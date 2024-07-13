using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color notEnoughMoneyHoverColor;
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

        GameObject buildParticles = Instantiate(currentTurret.upgradeEffect, buildPosition, Quaternion.identity);
        Destroy(buildParticles, currentTurret.upgradeEffectDuration);

        isUpgraded = true;

        Debug.Log("Turret upgraded!");
    }

    private void OnMouseEnter()
    {
        if (!buildManager.CanBuild || EventSystem.current.IsPointerOverGameObject()) return;

        if (buildManager.HasEnoughMoney)
            meshRend.material.color = hoverColor;
        else
            meshRend.material.color = notEnoughMoneyHoverColor;

    }

    private void OnMouseExit()
    {
        meshRend.material.color = startingColor;
    }
}
