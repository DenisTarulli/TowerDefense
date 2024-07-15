using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Camera cam;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        transform.rotation = cam.transform.rotation;
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
}
