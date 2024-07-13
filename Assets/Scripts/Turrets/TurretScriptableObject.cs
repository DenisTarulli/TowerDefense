using UnityEngine;

[CreateAssetMenu(fileName = "New turret", menuName = "Turret")]
public class TurretScriptableObject : ScriptableObject
{
    public GameObject prefab;
    public GameObject buildEffect;
    public float buildEffectDuration;
    public int cost;
}
