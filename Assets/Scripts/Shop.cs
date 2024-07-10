using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.Instance;
    }

    public void PurchaseStandardTurret()
    {
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }

    public void PurchaseMissileLauncherTurret()
    {
        buildManager.SetTurretToBuild(buildManager.missileLauncherTurretPrefab);
    }

    public void PurchaseLaserTurret()
    {
        buildManager.SetTurretToBuild(buildManager.laserTurretPrefab);
    }
}
