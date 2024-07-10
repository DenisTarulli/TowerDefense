using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    public GameObject standardTurretPrefab;
    public GameObject missileLauncherTurretPrefab;
    public GameObject laserTurretPrefab;

    private GameObject turretToBuild;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
