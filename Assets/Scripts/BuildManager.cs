using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    [SerializeField] private GameObject buildEffect;
    [SerializeField] private float buildEffectDuration;

    private TurretScriptableObject turretToBuild;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
    }

    public bool CanBuild { get => turretToBuild != null; }
    public bool HasEnoughMoney { get => PlayerStats.Money >= turretToBuild.cost; }

    public void SelectTurretToBuild(TurretScriptableObject turret)
    {
        turretToBuild = turret;
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("You don't have enough money");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;

        Vector3 buildPosition = node.GetBuildPosition();

        GameObject turret = Instantiate(turretToBuild.prefab, buildPosition, Quaternion.identity);
        node.turret = turret;

        GameObject buildParticles = Instantiate(buildEffect, buildPosition, Quaternion.identity);
        Destroy(buildParticles, buildEffectDuration);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
