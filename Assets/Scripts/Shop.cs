using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private TurretScriptableObject standardTurret;
    [SerializeField] private TurretScriptableObject missileLauncherTurret;
    [SerializeField] private TurretScriptableObject laserTurret;

    [SerializeField] private TextMeshProUGUI standardTurretText;
    [SerializeField] private TextMeshProUGUI missileLauncherTurretText;
    [SerializeField] private TextMeshProUGUI laserTurretText;

    private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.Instance;
        standardTurretText.text = $"${standardTurret.cost}";
        missileLauncherTurretText.text = $"${missileLauncherTurret.cost}";
        laserTurretText.text = $"${laserTurret.cost}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectStandardTurret();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectMissileLauncherTurret();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectLaserTurret();
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
