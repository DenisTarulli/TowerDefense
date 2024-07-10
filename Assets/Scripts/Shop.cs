using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private TurretScriptableObject standardTurret;
    [SerializeField] private TurretScriptableObject missileLauncherTurret;
    [SerializeField] private TurretScriptableObject laserTurret;

    private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.Instance;
    }

    public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncherTurret()
    {
        buildManager.SelectTurretToBuild(missileLauncherTurret);
    }

    public void SelectLaserTurret()
    {
        buildManager.SelectTurretToBuild(laserTurret);
    }
}
